using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using FluentValidation;

namespace Application.Validations.Appointment
{
    public class CreateAppointmentValidator : AbstractValidator<AppointmentCreateRequest>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(a=>a.DoctorId).NotEmpty().GreaterThan(0);
            RuleFor(a => a.PatientId).NotEmpty().GreaterThan(0);
            RuleFor(a=>a.Date).NotEmpty();
            RuleFor(a=>a.Time).NotEmpty();
        }
    }
}
