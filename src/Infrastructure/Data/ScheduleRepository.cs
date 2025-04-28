using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ScheduleRepository : BaseRepository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _context;
        public ScheduleRepository(ApplicationDbContext context): base(context)  
        {
            _context = context;
        }

        public async Task<Schedule?> GetWithAvailabilities(int id)
        {
            var schedule = await _context.Schedules.Include(s => s.Availabilities)
                                                   .FirstOrDefaultAsync(s => s.Id == id);

            return schedule;
        }
    }
}
