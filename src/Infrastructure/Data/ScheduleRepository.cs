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

        public async Task<Schedule> GetByDoctorAndDate(int doctorId, SheduleDay day)
        {
            //var schedule = await _context.Schedules.FirstOrDefaultAsync(s=> s.DoctorId == doctorId && s.DayOfWeek == day);
            //return schedule;
            throw new NotImplementedException();

        }
    }
}
