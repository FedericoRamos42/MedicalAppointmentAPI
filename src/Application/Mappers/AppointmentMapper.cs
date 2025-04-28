using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class AppointmentMapper
    {
        public static AppointmentDto ToDto(this Appointment appointment) => new AppointmentDto
        {
            Id = appointment.Id,
            DoctorId = appointment.DoctorId,
            PatientId = appointment.PatientId,
            Date = appointment.Date,
            Time = appointment.Time,   
            Status = appointment.Status,

        };

        public static List<AppointmentDto> ToListDto(this IEnumerable<Appointment> appointments) => appointments.Select(x => ToDto(x)).ToList();
    }
}
