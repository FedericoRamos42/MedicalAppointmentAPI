using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using FluentValidation;

namespace Application.Validations.Admin
{
    public class UpdateAdminValidator : AbstractValidator<AdminUpdateRequest>
    {
        public UpdateAdminValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                              .MaximumLength(30).WithMessage("Name must be 30 characters max long");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lastname is required")
                                    .MaximumLength(30).WithMessage("Last name must be 30 characters max long");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                                       .MaximumLength(20);
            RuleFor(x => x.Address).NotEmpty().WithMessage("Addres is required")
                                    .MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                                 .EmailAddress().WithMessage("Invalid Format.");
        }
    }
}
