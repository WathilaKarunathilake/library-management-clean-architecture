using LibraryManagementCleanArchitecture.Core.Application.DTO;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books
{
    public class RemoveBookCommand : IRequest<Unit>
    {
        public Guid BookId { get; set; }
    }
}
