// <copyright file="RegisterCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Register
{
    using FluentValidation;

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            this.RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

            this.RuleFor(x => x.MemberType)
                .NotEmpty().WithMessage("Member type is required.")
                .Must(mt => mt == "library" || mt == "staff")
                .WithMessage("Member type must be either 'library' or 'staff'.");

            this.When(x => x.MemberType == "staff", () =>
            {
                this.RuleFor(x => x.StaffType)
                    .NotEmpty().WithMessage("Staff type is required when MemberType is 'staff'.");
            });
        }
    }
}
