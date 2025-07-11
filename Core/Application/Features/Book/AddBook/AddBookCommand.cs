// <copyright file="AddBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Books.AddBook
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using MediatR;

    public class AddBookCommand : IRequest<Result<BookDTO>>
    {
        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Category { get; set; }

        public string? Description { get; set; }

        public int PublicationYear { get; set; }
    }
}
