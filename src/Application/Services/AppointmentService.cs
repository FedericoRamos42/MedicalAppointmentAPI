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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository,
                                  IDoctorRepository doctorRepository,
                                  IPatientRepository patientRepository,
                                  IAvailabilityRepository availabilityRepository)
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _availabilityRepository = availabilityRepository;
        }

        public async Task<Result<AppointmentDto>> Create(AppointmentCreateRequest request)
        {
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
            await _appointmentRepository.DeleteAsync(appointment);
            var dto = appointment.ToDto();
            return Result<AppointmentDto>.Success(dto);
        }
        public async Task<Result<AppointmentDto>> GetById(int id)
        {
            Appointment appointment = await _appointmentRepository.GetByIdAsync(id);
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
            List<Appointment> appointments = (List<Appointment>) await _appointmentRepository.Search(u => u.PatientId == patientId);
            var dtos = appointments.ToListDto();
            return Result<IEnumerable<AppointmentDto>>.Success(dtos);
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetByDoctor(int doctorId)
        {
            List<Appointment> appointments = (List<Appointment>)await _appointmentRepository.Search(u => u.DoctorId == doctorId);
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
            appointment.Status = AppointmentStatus.Canceled;
            await _appointmentRepository.UpdateAsync(appointment);
            var dtos = appointment.ToDto();
            return Result<AppointmentDto>.Success(dtos);  
        }

        public async Task<Result<IEnumerable<TimeSpan>>> GetAppointmentAvailabilited(int doctorId, DateTime date)
        {
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
