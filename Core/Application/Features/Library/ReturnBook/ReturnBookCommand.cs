// <copyright file="ReturnBookCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Library.ReturnBook
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using MediatR;

    public class ReturnBookCommand : IRequest<Result<Unit>>
    {
        public Guid BookId { get; set; }

        public Guid MemberId { get; set; }
    }
}
