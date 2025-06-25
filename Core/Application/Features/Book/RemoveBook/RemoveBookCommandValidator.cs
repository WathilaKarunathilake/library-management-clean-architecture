using FluentValidation;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    public class RemoveBookCommandValidator: AbstractValidator<RemoveBookCommand>
    {
        public RemoveBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book ID is required.");
        }
    }
}
