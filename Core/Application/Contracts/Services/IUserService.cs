using LibraryManagementCleanArchitecture.Application.DTO;

namespace LibraryManagementCleanArchitecture.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> CheckPasswordAsync(string email, string password);
        Task AddToRoleAsync(string email, string role);
        Task<UserResultDTO> GetUserDetailsFromEmail(string email);
        Task<UserResultDTO> CreateUserAsync(string username, string email, string password, string role);
    }
}
