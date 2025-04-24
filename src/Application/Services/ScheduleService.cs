using System.Collections.Generic;
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

        public async Task<Result<ScheduleDto>> Delete(int Id)
        {
            var schedule = await _schedules.GetByIdAsync(Id);
            await _schedules.DeleteAsync(schedule);
            var dto = schedule.ToDto();
            return Result<ScheduleDto>.Success(dto);
        }

        public async Task<Result<IEnumerable<ScheduleDto>>> GetByDoctor(int id)
        {
            var schedules = await _schedules.Search(s=>s.DoctorId == id);

            var dto = schedules.ToListDto();
            return Result<IEnumerable <ScheduleDto>>.Success(dto);
        }

        public async Task<Result<ScheduleDto>> Update(ScheduleUpdateRequest request)
        {
            Schedule schedule = await _schedules.GetByDoctorAndDate(request.DoctorId,request.DayOfWeek);

            schedule.StartTime = request.StartTime;
            schedule.EndTime = request.EndTime;

            await _schedules.UpdateAsync(schedule);

            var dto = schedule.ToDto();
            return Result<ScheduleDto>.Success(dto);
        }
    }
}
