using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    public class ReturnBookCommandValidator: AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookCommandValidator()
        {

            RuleFor(x => x.BookId).NotEmpty().WithMessage("Book ID is required.");
            RuleFor(x => x.MemberId).NotEmpty().WithMessage("Member ID is required.");
        }
    }
}
