using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        public ScheduleRepository(ApplicationDbContext context): base(context)  
        {
            _context = context;
        }

       
    }
}
