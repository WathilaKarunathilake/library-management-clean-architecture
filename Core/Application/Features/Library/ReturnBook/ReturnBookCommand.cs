using LibraryManagementCleanArchitecture.Application.Response;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    public class ReturnBookCommand: IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }
        public Guid MemberId { get; set; }
    }
}
