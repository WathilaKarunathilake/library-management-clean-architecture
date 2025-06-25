using LibraryManagementCleanArchitecture.Application.Response;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    public class RemoveBookCommand : IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }
    }
}
