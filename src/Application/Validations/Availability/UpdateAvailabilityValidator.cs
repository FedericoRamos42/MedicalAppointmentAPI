using Application.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Availability
{
    public class UpdateAvailabilityValidator : AbstractValidator<AvailabilityUpdateRequest>
    {
        public UpdateAvailabilityValidator() {


            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be earlier than end time.");
        }

    }
}
