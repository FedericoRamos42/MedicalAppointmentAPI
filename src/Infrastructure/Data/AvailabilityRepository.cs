using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AvailabilityRepository : BaseRepository<Availability>, IAvailabilityRepository
    {
        private readonly ApplicationDbContext _context;
        public AvailabilityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Availability>> GetByDoctorAndDate(int doctorId, DateTime date)
        {

            SheduleDay day = (SheduleDay)((int)date.DayOfWeek);
            var list = await _context.Availabilities.Where(a => a.Schedule.DoctorId == doctorId && a.ScheduleDay == day).ToListAsync();
            return list;
        }
    }
}
