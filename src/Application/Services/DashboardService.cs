using Application.Interfaces;
using Application.Models;
using Application.Result;
using Domain.Interfaces;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IPatientRepository _repositoryPatient;
        private readonly IDoctorRepository _repositoryDoctor;
        private readonly IAppointmentRepository _repositoryAppointment;
        public DashboardService(IDoctorRepository repositoryDoctor, IAppointmentRepository repositoryAppointment,IPatientRepository patientRepository)
        {
            _repositoryDoctor = repositoryDoctor;
            _repositoryAppointment = repositoryAppointment;
            _repositoryPatient = patientRepository;
        }

        public async Task<Result<DashboardAdminDto>> GetAdminDashboard()
        {
            var totalAppointments = await _repositoryAppointment.CountAsync();
            var confirmedAppointments = await _repositoryAppointment.CountAsync(a => a.Status == Domain.Enums.AppointmentStatus.Pending);
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
    }
}
