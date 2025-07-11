// <copyright file="GetBooksQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Features.Books.GetBooks
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using MediatR;

    public class GetBooksQuery : IRequest<Result<List<BookDTO>>>
    {
    }
}
