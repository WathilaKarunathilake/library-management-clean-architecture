using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Register
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

            RuleFor(x => x.MemberType)
                .NotEmpty().WithMessage("Member type is required.")
                .Must(mt => mt == "library" || mt == "staff")
                .WithMessage("Member type must be either 'library' or 'staff'.");

            When(x => x.MemberType == "staff", () =>
            {
                RuleFor(x => x.StaffType)
                    .NotEmpty().WithMessage("Staff type is required when MemberType is 'staff'.");
            });
        }
    }
}
