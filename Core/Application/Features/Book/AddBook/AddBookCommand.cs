using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Books.AddBook
{
    public class AddBookCommand: IRequest<Result<BookDTO>>
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int PublicationYear { get; set; }
    }
}
