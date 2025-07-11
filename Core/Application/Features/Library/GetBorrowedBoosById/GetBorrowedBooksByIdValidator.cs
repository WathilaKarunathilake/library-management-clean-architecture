// <copyright file="GetBorrowedBooksByIdValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.GetBorrowedBoosById
{
    using FluentValidation;

    public class GetBorrowedBooksByIdValidator : AbstractValidator<GetBorrowedBooksByIdQuery>
    {
        public GetBorrowedBooksByIdValidator()
        {
            this.RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member ID is required.");
        }
    }
}
