// <copyright file="IUserService.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application.Contracts.Services
{
    using LibraryManagementCleanArchitecture.Application.DTO;

    public interface IUserService
    {
        Task<bool> CheckPasswordAsync(string email, string password);

        Task AddToRoleAsync(string email, string role);

        Task<UserResultDTO> GetUserDetailsFromEmail(string email);

        Task<string> GetEmailFromId(string id);

        Task<UserResultDTO> CreateUserAsync(string username, string email, string password, string role);
    }
}
