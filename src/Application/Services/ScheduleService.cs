using System.Collections.Generic;
using System.Linq;
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
        private readonly IAvailabilityRepository _availabilities;
        private readonly IAppointmentRepository _appointments;
        public ScheduleService(IScheduleRepository schedule,IAvailabilityRepository availabilities,IAppointmentRepository appointments)
        {
            _schedules = schedule;
            _availabilities = availabilities;
            _appointments = appointments;
        }

        public async Task<Result<ScheduleDto>> AddAvailability(int id, AvailabilityCreateRequest request)
        {
            Schedule schedule = await _schedules.GetWithAvailabilities(id) ?? throw new ArgumentException("fail"); 
            Availability availability = new Availability()
            {
                ScheduleId = schedule.Id,
                ScheduleDay = request.ScheduleDay,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
            };
            await _availabilities.AddAsync(availability);
            var dto = schedule.ToDto();
            return Result<ScheduleDto>.Success(dto); 



        }

        public async Task<Result<ScheduleDto>> Create(ScheduleCreateRequest request)
        {
            Schedule shedule = new Schedule()
            {
                DoctorId = request.DoctorId,
                Availabilities = request.Availabilities.Select(a=> new Availability()
                {
                    ScheduleDay = a.ScheduleDay,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                }).ToList(),
            };

            await _schedules.AddAsync(shedule);
            var dto = shedule.ToDto();
            return Result<ScheduleDto>.Success(dto);
        }

        public async Task<Result<ScheduleDto>> Delete(int Id)
        {
            var schedule = await _schedules.GetByIdAsync(Id);
            var dto = schedule.ToDto();
            return Result<ScheduleDto>.Success(dto);
        }

        public async Task<Result<ScheduleDto>> GetByDoctor(int id)
        {
            Schedule entity = await _schedules.GetWithAvailabilities(id) ?? throw new Exception("error");
            var dto = entity.ToDto();
            return Result<ScheduleDto>.Success(dto);
            
        }

        public async Task<Result<IEnumerable<TimeSpan>>> GetByDoctorAndDate(int doctorId, DateTime date)
        {
            List<Availability> availabilities = (List<Availability>) await _availabilities.GetByDoctorAndDate(doctorId, date);
            List<Appointment> appointments =  await  _appointments.GetAppointmentsbyDateAndDoctor(date, doctorId);

            var list = new List<TimeSpan>();

            foreach (var avail in availabilities)
            {
                var timeStart = avail.StartTime;

                while (timeStart + TimeSpan.FromHours(1) <= avail.EndTime)
                {
                    bool isOccupied = appointments.Any(a =>a.Date == date.Date
                                                       && a.Time == timeStart
                                                       && a.Status == AppointmentStatus.Pending);

                    if (!isOccupied)
                    {
                        list.Add(timeStart);
                    }

                    timeStart += TimeSpan.FromHours(1);
                }
            }
            return Result<IEnumerable<TimeSpan>>.Success(list);
        }

        
    }
}
