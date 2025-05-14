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
            Id = availability.Id,
            DayOfWeek = availability.DayOfWeek,
            DoctorId = availability.DoctorId,
            EndTime = availability.EndTime,
            StartTime = availability.StartTime,
        };
        public static List<AvailabilityDto> ToListDto(this IEnumerable<Availability> availabilities) => availabilities.Select(x => ToDto(x)).ToList();
    }
}
