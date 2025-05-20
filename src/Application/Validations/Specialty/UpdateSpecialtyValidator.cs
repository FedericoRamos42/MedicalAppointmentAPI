using Application.Models.Request;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Specialty
{
    public class UpdateSpecialtyValidator : AbstractValidator<SpecialtyUpdateRequest>
    {
        public UpdateSpecialtyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        }
    }
}
