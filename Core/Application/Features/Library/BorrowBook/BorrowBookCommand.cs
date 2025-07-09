using LibraryManagementCleanArchitecture.Application.Response;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    public class BorrowBookCommand: IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
    }
}
