using Application.Models;
using Application.Models.Request;
using Application.Result;
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
        Task<Result<IEnumerable<AppointmentDto>>> GetByDoctor(int doctorId);
        Task<Result<IEnumerable<AppointmentDto>>> GetByPatient(int patientId);
        Task<Result<IEnumerable<AppointmentDto>>> GetByStatus(int id, AppointmentStatus status);
        Task<Result<AppointmentDto>> Cancel(int appointmentId);

    }
}
