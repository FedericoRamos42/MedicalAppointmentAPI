using Application.Models;
using Application.Models.Request;
using Application.Models.Response;
using Application.Result;
using Domain.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<Result<AppointmentDto>> Create (AppointmentCreateRequest request);
        Task<Result<AppointmentDto>> GetById(int id);
        Task<Result<IEnumerable<AppointmentDto>>> GetAll();
        Task<Result<AppointmentDto>> Delete(int id);
        Task<Result<IEnumerable<AppointmentResponse>>> GetByDoctor(int doctorId);
        Task<Result<IEnumerable<AppointmentResponse>>> GetByPatient(int patientId);
        Task<Result<IEnumerable<AppointmentDto>>> GetByStatus(int id, AppointmentStatus status);
        Task<Result<IEnumerable<TimeSpan>>> GetAppointmentAvailabilited(int doctorId, DateTime date);
        Task<Result<AppointmentDto>> Cancel(int appointmentId);
        Task<Result<PaginatedList<AppointmentDto>>> GetPaginated(int pageIndex, int pageSize);

    }
}
