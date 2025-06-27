using FluentValidation;

namespace LibraryManagementCleanArchitecture.Application.Features.Members.AddMember
{
    public class AddMembersCommandValidator: AbstractValidator<AddMemberCommand>
    {
        public AddMembersCommandValidator()
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
