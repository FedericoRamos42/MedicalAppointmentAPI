using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _schedules;
        public ScheduleService(IScheduleRepository schedule)
        {
            _schedules = schedule;
        }
        public async Task<Result<ScheduleDto>> Create(ScheduleCreateRequest request)
        {
            Schedule schedule = new Schedule()
            {
                DoctorId = request.DoctorId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
            };

            await _schedules.AddAsync(schedule);

            var dto = schedule.ToDto();
            return Result<ScheduleDto>.Success(dto);
            

        }
    }
}
