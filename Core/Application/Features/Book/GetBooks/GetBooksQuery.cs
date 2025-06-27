using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks
{
    public class GetBooksQuery : IRequest<Result<List<BookDTO>>>
    {
        public Guid MemberId { get; set; }
    }
}
