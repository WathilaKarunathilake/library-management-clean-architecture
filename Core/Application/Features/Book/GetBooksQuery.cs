using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class GetBooksQuery : IRequest<List<BookDTO>>
    {
        public Guid memberId { get; set; }
    }
}
