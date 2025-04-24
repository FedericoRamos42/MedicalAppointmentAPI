
using Application.Models;
using Domain.Entities;

namespace Application.Mappers
{
    public static class ScheduleMapper
    {
        public static ScheduleDto ToDto(this Schedule schedule) => new ScheduleDto
        {
            DoctorId = schedule.DoctorId,
            DayOfWeek = schedule.DayOfWeek.ToString(),
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime,
        };
    }
}
