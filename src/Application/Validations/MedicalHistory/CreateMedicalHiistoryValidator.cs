using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using FluentValidation;

namespace Application.Validations.MedicalHistory
{
    public class CreateMedicalHiistoryValidator : AbstractValidator<MedicalHistoryCreateRequest>
    {
        public CreateMedicalHiistoryValidator()
        {
            RuleFor(m => m.PatientId).NotEmpty().GreaterThan(0);
            RuleFor(m => m.ReasonForVisit).NotEmpty();
            RuleFor(m => m.Diagnosis).NotEmpty();
            RuleFor(m => m.Notes).MaximumLength(200);
            RuleFor(m => m.AppointmentId).GreaterThan(0);
            RuleFor(m => m.Treatment).MaximumLength(500);
        }
    }
}
