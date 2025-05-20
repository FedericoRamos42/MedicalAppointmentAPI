using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Availability
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public int DoctorId { get; set; }
        public SheduleDay DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

    }
}
