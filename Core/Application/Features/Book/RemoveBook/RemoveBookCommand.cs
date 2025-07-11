// <copyright file="RemoveBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Books.RemoveBook
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using MediatR;

    public class RemoveBookCommand : IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }
    }
}
