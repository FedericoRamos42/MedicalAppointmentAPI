using Application.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Availability
{
    public class AvailabilityCreateRequestValidator : AbstractValidator<AvailabilityCreateRequest>
    {
        public AvailabilityCreateRequestValidator()
        {
            RuleFor(x => x.DoctorId)
                .GreaterThan(0)
                .WithMessage("Doctor ID must be greater than 0.");

            RuleFor(x => x.DayOfWeek)
                .IsInEnum()
                .WithMessage("Invalid day of the week.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be earlier than end time.");

            //RuleFor(x => x.StartTime)
            //    .Must(BeWithinWorkingHours)
            //    .WithMessage("Start time must be within working hours (e.g., 08:00 - 16:00).");

            //RuleFor(x => x.EndTime)
            //    .Must(BeWithinWorkingHours)
            //    .WithMessage("End time must be within working hours (e.g., 08:00 - 17:00).");
        }

        //private bool BeWithinWorkingHours(TimeSpan time)
        //{
        //    var start = new TimeSpan(8, 0, 0);
        //    var end = new TimeSpan(17, 0, 0);
        //    return time >= start && time <= end;
        //}
    }
}
