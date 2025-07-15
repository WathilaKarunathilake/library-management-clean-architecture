// <copyright file="GetBorrowedBooksByIdQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.GetBorrowedBoosById
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using MediatR;

    public class GetBorrowedBooksByIdQuery : IRequest<Result<List<BookDTO>>>
    {
        public string? MemberId { get; set; }
    }
}
