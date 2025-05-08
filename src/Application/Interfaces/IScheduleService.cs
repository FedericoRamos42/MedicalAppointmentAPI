using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Enums;

namespace Application.Interfaces
{
    public interface IScheduleService
    {
        Task<Result<ScheduleDto>> Create(ScheduleCreateRequest request);
        Task<Result<IEnumerable<TimeSpan>>> GetByDoctorAndDate(int DoctorId, DateTime date);
        Task<Result<ScheduleDto>> AddAvailability(int id, AvailabilityCreateRequest request);
        Task<Result<ScheduleDto>> Delete(int Id);
        Task<Result<ScheduleDto>> GetByDoctor(int id);

    }
}
