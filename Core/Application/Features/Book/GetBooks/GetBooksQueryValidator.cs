using FluentValidation;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks
{
    public class GetBooksQueryValidator: AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(x => x.MemberId)
                .NotEmpty().WithMessage("Member ID is required.");
        }
    }
}
