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

        public async Task<Doctor> GetWithAvailabities(int id)
        {
            var doctor = await _context.Doctors.Include(d=>d.Availabilities)
                                                .FirstOrDefaultAsync(d=>d.Id == id);

            return doctor;
        }
    }
}
