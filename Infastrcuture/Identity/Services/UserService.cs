using LibraryManagementCleanArchitecture.Application.Contracts.Services;
using LibraryManagementCleanArchitecture.Application.DTO;
using LibraryManagementCleanArchitecture.Infastrcuture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementCleanArchitecture.Infastrcuture.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResultDTO> CreateUserAsync(string username, string email, string password, string role)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                return new UserResultDTO
                {
                    Errors = "Email is already registered.",
                    Succeeded = false,
                    UserId = Guid.Empty
                };
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Name = username,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                string errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                return new UserResultDTO
                {
                    Errors = errorMessages,
                    Succeeded = false,
                    UserId = Guid.Empty
                };
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                string roleErrors = string.Join("; ", roleResult.Errors.Select(e => e.Description));
                return new UserResultDTO
                {
                    Errors = $"User created, but failed to assign role: {roleErrors}",
                    Succeeded = false,
                    UserId = Guid.Parse(user.Id)
                };
            }

            return new UserResultDTO
            {
                Email = email,
                Role = role,
                Succeeded = true,
                UserId = Guid.Parse(user.Id)
            };
        }

        public async Task<UserResultDTO> GetUserDetailsFromEmail(string email)
        {
            var users = await _userManager.Users.Where(u => u.Email == email).ToListAsync();
            var existingUser = users.FirstOrDefault();

            var roles = await _userManager.GetRolesAsync(existingUser); 

            return new UserResultDTO
            {
                UserId = Guid.Parse(existingUser.Id),
                Email = existingUser.Email,
                Role = roles.FirstOrDefault() 
            };
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var users = await _userManager.Users.Where(u => u.Email == email).ToListAsync();
            var user = users.FirstOrDefault();
            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task AddToRoleAsync(string email, string role)
        {
            var users = await _userManager.Users.Where(u => u.Email == email).ToListAsync();
            var user = users.FirstOrDefault();
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
