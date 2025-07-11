// <copyright file="UserService.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Infastrcuture.Identity.Services
{
    using LibraryManagementCleanArchitecture.Application.Contracts.Services;
    using LibraryManagementCleanArchitecture.Application.DTO;
    using LibraryManagementCleanArchitecture.Infastrcuture.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserResultDTO> CreateUserAsync(string username, string email, string password, string role)
        {
            var existingUser = await this.userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                return new UserResultDTO
                {
                    Errors = "Email is already registered.",
                    Succeeded = false,
                    UserId = Guid.Empty,
                };
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Name = username,
                Email = email,
            };

            var result = await this.userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                string errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                return new UserResultDTO
                {
                    Errors = errorMessages,
                    Succeeded = false,
                    UserId = Guid.Empty,
                };
            }

            var roleResult = await this.userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                string roleErrors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                return new UserResultDTO
                {
                    Errors = $"User created, but failed to assign role: {roleErrors}",
                    Succeeded = false,
                    UserId = Guid.Parse(user.Id),
                };
            }

            return new UserResultDTO
            {
                Name = username,
                Email = email,
                Role = role,
                Succeeded = true,
                UserId = Guid.Parse(user.Id),
            };
        }

        public async Task<UserResultDTO> GetUserDetailsFromEmail(string email)
        {
            var users = await this.userManager.Users.Where(u => u.Email == email).ToListAsync();
            var existingUser = users.FirstOrDefault();
            if (existingUser == null)
            {
                return new UserResultDTO
                {
                    Errors = "User not found.",
                    Succeeded = false,
                    UserId = Guid.Empty,
                };
            }

            var roles = await this.userManager.GetRolesAsync(existingUser);

            return new UserResultDTO
            {
                Name = existingUser.Name,
                UserId = Guid.Parse(existingUser.Id),
                Email = existingUser.Email,
                Role = roles.FirstOrDefault(),
            };
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var users = await this.userManager.Users.Where(u => u.Email == email).ToListAsync();
            var user = users.FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            return await this.userManager.CheckPasswordAsync(user, password);
        }

        public async Task AddToRoleAsync(string email, string role)
        {
            var users = await this.userManager.Users.Where(u => u.Email == email).ToListAsync();
            var user = users.FirstOrDefault();
            if (user != null)
            {
                await this.userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task<string> GetEmailFromId(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            return user.Email;
        }
    }
}
