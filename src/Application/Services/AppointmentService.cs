using System.Reflection.Metadata.Ecma335;
using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Models.Request;
using Application.Result;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IValidator<AppointmentCreateRequest> _createValidator;
        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  IDoctorRepository doctorRepository,
                                  IPatientRepository patientRepository,
                                  IAvailabilityRepository availabilityRepository,
                                  IValidator<AppointmentCreateRequest> createValidator)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _availabilityRepository = availabilityRepository;
            _createValidator = createValidator;
        }

        public async Task<Result<AppointmentDto>> Create(AppointmentCreateRequest request)
        {
            var result = _createValidator.Validate(request);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e=>e.ErrorMessage).ToList();
                return Result<AppointmentDto>.FailureModels(errors);
            }

            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                return Result<AppointmentDto>.Failure($"Patient with Id {request.PatientId} that not exist");
            }

            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                return Result<AppointmentDto>.Failure($"Patient with Id {request.DoctorId} that not exist");
            }

            Appointment appointment = new Appointment()
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                Time = request.Time,
                Date = request.Date,
                Status = AppointmentStatus.Pending,
            };


            await _appointmentRepository.AddAsync(appointment);
            var dto = appointment.ToDto();
            return Result<AppointmentDto>.Success(dto);

        }

        public async Task<Result<AppointmentDto>> Delete(int id)
        {
            Appointment appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return Result<AppointmentDto>.Failure($"Appointment with id {id} that not exist");
            }
            await _appointmentRepository.DeleteAsync(appointment);
            var dto = appointment.ToDto();
            return Result<AppointmentDto>.Success(dto);
        }
        public async Task<Result<AppointmentDto>> GetById(int id)
        {
            Appointment appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null)
            {
                return Result<AppointmentDto>.Failure($"Appointment with id {id} that not exist");
            }
            var dto = appointment.ToDto();
            return Result<AppointmentDto>.Success(dto);
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetAll()
        {
            IEnumerable<Appointment> appointments = await _appointmentRepository.GetAllAsync();
            var dtos = appointments.ToListDto();
            return Result<IEnumerable<AppointmentDto>>.Success(dtos);
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetByPatient(int patientId)
        {
            var patient = await _patientRepository.GetByIdAsync(patientId);
            if (patient == null)
            {
                return Result<IEnumerable<AppointmentDto>>.Failure($"Patient with id {patientId} that not exist");
            }
            List<Appointment> appointments = (List<Appointment>) await _appointmentRepository.Search(u => u.PatientId == patientId);
            var dtos = appointments.ToListDto();
            return Result<IEnumerable<AppointmentDto>>.Success(dtos);
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetByDoctor(int doctorId)
        {
            var doctor = await _doctorRepository.GetByIdAsync(doctorId);
            if (doctor == null)
            {
                return Result<IEnumerable<AppointmentDto>>.Failure($"Patient with id {doctorId} that not exist");
            }
            List<Appointment> appointments = (List<Appointment>) await _appointmentRepository.Search(u => u.DoctorId == doctorId);
            var dtos = appointments.ToListDto();
            return Result<IEnumerable<AppointmentDto>>.Success(dtos);
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetByStatus(int id, AppointmentStatus status)
        {
            List<Appointment> appointments = (List<Appointment>)await _appointmentRepository.Search(
                                                                      u => u.PatientId == id || u.DoctorId == id
                                                                      && u.Status == status);
            var dtos = appointments.ToListDto();
            return Result<IEnumerable<AppointmentDto>>.Success(dtos);
        }

        public async Task<Result<AppointmentDto>> Cancel(int appointmentId)
        {
            Appointment appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                return Result<AppointmentDto>.Failure($"Appointment with id {appointmentId} that not exist");
            }
            appointment.Status = AppointmentStatus.Canceled;
            await _appointmentRepository.UpdateAsync(appointment);
            var dtos = appointment.ToDto();
            return Result<AppointmentDto>.Success(dtos);  
        }

        public async Task<Result<IEnumerable<TimeSpan>>> GetAppointmentAvailabilited(int doctorId, DateTime date)
        {
            var doctor = await _doctorRepository.GetByIdAsync(doctorId);
            if(doctor == null)
            {
                return Result<IEnumerable<TimeSpan>>.Failure($"Doctor with id {doctorId} that not exist");
            }
            if (string.IsNullOrEmpty(date.ToString()))
            { 
                return Result<IEnumerable<TimeSpan>>.Failure($"Date is Required");
            }
            List<Availability> availabilities = (List<Availability>) await _availabilityRepository.GetByDoctorAndDate(doctorId, date);
            List<Appointment> appointments = await _appointmentRepository.GetAppointmentsbyDateAndDoctor(date, doctorId);

            var list = new List<TimeSpan>();

            foreach (var avail in availabilities)
            {
                var timeStart = avail.StartTime;

                while (timeStart + TimeSpan.FromHours(1) <= avail.EndTime)
                {
                    bool isOccupied = appointments.Any(a => a.Date == date.Date
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
