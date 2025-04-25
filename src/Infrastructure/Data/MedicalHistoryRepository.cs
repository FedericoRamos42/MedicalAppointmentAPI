using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class MedicalHistoryRepository : BaseRepository<MedicalHistory>, IMedicalHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicalHistoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
