using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ScheduleMapper
    {
        public static ScheduleDto ToDto(this Schedule schedule) => new ScheduleDto
        {
            Id = schedule.Id,
            DoctorId = schedule.DoctorId,
            Availabilities = schedule.Availabilities.ToListDto(),

        };
        public static List<ScheduleDto> ToListDto(this IEnumerable<Schedule> schedule) => schedule.Select(x => ToDto(x)).ToList();


    }
}
