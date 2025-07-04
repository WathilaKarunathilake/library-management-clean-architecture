using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Library.GetBorrowedBoosById
{
    public class GetBorrowedBooksByIdQuery : IRequest<Result<List<BookDTO>>>
    {
        public string MemberId { get; set; }
    }
}
