using Application.Interfaces;
using Application.Mappers;
using Application.Models;
using Application.Result;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IPatientRepository _repositoryPatient;
        private readonly IDoctorRepository _repositoryDoctor;
        private readonly IAppointmentRepository _repositoryAppointment;
        private readonly IMedicalHistoryRepository _repositoryMedicalHistory;
        public DashboardService(IDoctorRepository repositoryDoctor,
            IAppointmentRepository repositoryAppointment,
            IPatientRepository patientRepository,
            IMedicalHistoryRepository medicalHistoryRepository)
        {
            _repositoryDoctor = repositoryDoctor;
            _repositoryAppointment = repositoryAppointment;
            _repositoryPatient = patientRepository;
            _repositoryMedicalHistory = medicalHistoryRepository;
        }

        public async Task<Result<DashboardAdminDto>> GetAdminDashboard()
        {
            var totalAppointments = await _repositoryAppointment.CountAsync();
            var confirmedAppointments = await _repositoryAppointment.CountAsync(a => a.Status == Domain.Enums.AppointmentStatus.Confirmed);
            var canceledAppointments = await _repositoryAppointment.CountAsync(a => a.Status == Domain.Enums.AppointmentStatus.Canceled);
            var todayAppointments = await _repositoryAppointment.CountAsync(a=> a.Date.Date == DateTime.Today);
            var patient = await _repositoryPatient.CountAsync(p => p.IsAvailable == true);
            var doctor = await _repositoryDoctor.CountAsync(d => d.IsAvailable == true);

            var dto = new DashboardAdminDto()
            {
                AppointmentTotal = totalAppointments,
                ActiveDoctor = doctor,
                ActivePatient = patient,
                CanceledAppointment = canceledAppointments,
                ConfirmedAppointment = canceledAppointments,
                AppoinmentToday = todayAppointments,
            };

            return Result<DashboardAdminDto>.Success(dto);
        }
        public async Task<Result<DashboardDoctorDto>> GetDoctorDashboard(int doctorId)
        {
            
            var totalAppointments = await _repositoryAppointment.CountAsync(a=>a.DoctorId == doctorId);
            var todayAppointment = await _repositoryAppointment.CountAsync(a=>a.DoctorId == doctorId && a.Date.Date == DateTime.Today);
            var confirmedAppointments = await _repositoryAppointment.CountAsync(a => a.DoctorId == doctorId && a.Status == Domain.Enums.AppointmentStatus.Confirmed);
            var canceledAppointments = await _repositoryAppointment.CountAsync(a=> a.DoctorId == doctorId && a.Status == Domain.Enums.AppointmentStatus.Canceled);
            var dto = new DashboardDoctorDto()
            {
                AppointmentTotal = totalAppointments,
                AppoinmentToday = todayAppointment,
                AppointmentCanceled = canceledAppointments,
                AppointmentConfirmed = confirmedAppointments,
                MedicalHistoryTotal = 1
            };
            return Result<DashboardDoctorDto>.Success(dto);
        }

        public async Task<Result<DashboardPatientDto>> GetPatientDashboard(int patientId)
        {
            var confirmedAppointments = await _repositoryAppointment.CountAsync(a => a.PatientId == patientId && a.Status == Domain.Enums.AppointmentStatus.Confirmed);
            var canceledAppointments = await _repositoryAppointment.CountAsync(a => a.PatientId == patientId && a.Status == Domain.Enums.AppointmentStatus.Canceled);
            var nextAppointment = await _repositoryAppointment.GetNextAppointmentByPatientId(patientId);
            var lastMedicalHistory = await _repositoryMedicalHistory.GetLastMedicalHistoryByPatient(patientId);
            var totalMeidcalHistory = await _repositoryMedicalHistory.CountAsync(a => a.PatientId == patientId);

            var dto = new DashboardPatientDto()
            {
                AppointmentsConfirmed = confirmedAppointments,
                AppointmentsCanceled = canceledAppointments,
                NextAppointment = nextAppointment?.ToDto(),
                LastMedicalHistory = lastMedicalHistory?.ToDto(),
                MedicalHistoryTotal = totalMeidcalHistory
            };

            return Result<DashboardPatientDto>.Success(dto);

        }
    }
}
