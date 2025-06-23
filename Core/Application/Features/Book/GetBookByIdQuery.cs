using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class GetBookByIdQuery: IRequest<BookDTO>
    {
        public Guid BookId { get; }
    }
}
