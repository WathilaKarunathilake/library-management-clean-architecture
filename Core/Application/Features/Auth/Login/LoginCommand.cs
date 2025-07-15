// <copyright file="LoginCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Login
{
    using LibraryManagementCleanArchitecture.Application.DTO;
    using LibraryManagementCleanArchitecture.Application.Response;
    using MediatR;

    public record LoginCommand(string email, string password) : IRequest<Result<AuthResultDTO>>;
}
