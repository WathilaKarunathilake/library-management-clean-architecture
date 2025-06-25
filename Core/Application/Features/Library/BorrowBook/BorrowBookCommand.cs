using LibraryManagementCleanArchitecture.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    public class BorrowBookCommand: IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }

    }
}
