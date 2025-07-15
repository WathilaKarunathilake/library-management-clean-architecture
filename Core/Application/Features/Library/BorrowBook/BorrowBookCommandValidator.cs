// <copyright file="BorrowBookCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    using FluentValidation;

    public class BorrowBookCommandValidator : AbstractValidator<BorrowBookCommand>
    {
        public BorrowBookCommandValidator()
        {
            this.RuleFor(x => x.BookId).NotEmpty().WithMessage("Book ID is required.");
            this.RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member ID is required.");
        }
    }
}
