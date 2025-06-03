using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetAllWithSpecialty()
        {
            var doctors = await _context.Doctors.Include(d => d.Specialty).ToListAsync();
            return doctors;
        }

        public async Task<Doctor> GetByIdWithSpecialty(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Specialty).FirstOrDefaultAsync(d => d.Id == id);
            return doctor;
        }

        public async Task<Doctor> GetWithAvailabities(int id)
        {
            var doctor = await _context.Doctors.Include(d=>d.Availabilities)
                                                .FirstOrDefaultAsync(d=>d.Id == id);

            return doctor;
        }
        
    }
}
