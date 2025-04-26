using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class AvailabilityMapper
    {
        public static AvailabilityDto ToDto(this Availability availability) => new AvailabilityDto
        {
            ScheduleDay = availability.ScheduleDay,
            ScheduleId = availability.ScheduleId,
            EndTime = availability.EndTime,
            StartTime = availability.StartTime,
        };
        public static List<AvailabilityDto> ToListDto(this IEnumerable<Availability> availabilities) => availabilities.Select(x => ToDto(x)).ToList();
    }
}
