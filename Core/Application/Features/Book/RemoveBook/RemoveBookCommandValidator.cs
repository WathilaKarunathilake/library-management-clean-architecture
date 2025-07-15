// <copyright file="RemoveBookCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    using FluentValidation;

    public class RemoveBookCommandValidator : AbstractValidator<RemoveBookCommand>
    {
        public RemoveBookCommandValidator()
        {
            this.RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book ID is required.");
        }
    }
}
