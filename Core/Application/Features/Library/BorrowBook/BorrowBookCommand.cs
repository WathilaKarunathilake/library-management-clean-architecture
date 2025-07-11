// <copyright file="BorrowBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.BorrowBook
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using MediatR;

    public class BorrowBookCommand : IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }

        public Guid MemberId { get; set; }
    }
}
