using Application.Models.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations.Specialty
{
    public class CreateSpecialtyValidator : AbstractValidator<SpecialtyCreateRequest>
    {
        public CreateSpecialtyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("Description is required");
        }
    }
}
