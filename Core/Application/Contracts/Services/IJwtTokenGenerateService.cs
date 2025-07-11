// <copyright file="IJwtTokenGenerateService.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Contracts.Services
{
    public interface IJwtTokenGenerateService
    {
        string GenerateToken(string name, string userId, string email, string role);
    }
}
