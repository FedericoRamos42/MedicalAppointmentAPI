using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  IDoctorRepository doctorRepository,
                                  IPatientRepository patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            
        }

        public Task<Result<AppointmentDto>> Create(AppointmentCreateRequest request)
        {
            throw new NotImplementedException();

        }

        public Task<Result<AppointmentDto>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<AppointmentDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<AppointmentDto>> GetAppointmentAvailableByDoctor(int id, DateTime? date)
        {
            throw new NotImplementedException();
        }
    }
}
