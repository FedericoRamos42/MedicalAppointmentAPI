using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;
        public SheduleDay DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
