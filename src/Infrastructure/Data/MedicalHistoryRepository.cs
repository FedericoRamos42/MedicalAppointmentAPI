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
    public class MedicalHistoryRepository : BaseRepository<MedicalHistory>, IMedicalHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicalHistoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalHistory>> GetAll()
        {
            var entities = await _context.MedicalHistories.Include(mh => mh.Patient)
                                                           .Include(mh => mh.Doctor)
                                                            .Include(mh => mh.Appoinment)
                                                             .ToListAsync();
            return entities;
        }

        public async Task<MedicalHistory> GetById(int id)
        {
            var entity = await _context.MedicalHistories.Include(mh => mh.Patient)
                                                           .Include(mh => mh.Doctor)
                                                            .Include(mh => mh.Appoinment)
                                                             .FirstOrDefaultAsync(mh=>mh.Id == id);
            return entity;
        }
    }
}
