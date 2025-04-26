using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models
{
    public class AvailabilityDto
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public SheduleDay ScheduleDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
