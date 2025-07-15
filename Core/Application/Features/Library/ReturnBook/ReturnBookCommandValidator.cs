// <copyright file="ReturnBookCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    using FluentValidation;

    public class ReturnBookCommandValidator : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {
            this.RuleFor(x => x.BookId).NotEmpty().WithMessage("Book ID is required.");
            this.RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member ID is required.");
        }
    }
}
