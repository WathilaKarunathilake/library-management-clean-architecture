using FluentValidation;
using LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.GetBorrowedBoosById
{
    public class GetBorrowedBooksByIdValidator : AbstractValidator<GetBorrowedBooksByIdQuery>
    {
        public GetBorrowedBooksByIdValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member ID is required.");
        }
    }
}
