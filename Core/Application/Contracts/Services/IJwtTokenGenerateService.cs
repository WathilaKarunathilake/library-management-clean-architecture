namespace LibraryManagementCleanArchitecture.Application.Contracts.Services
{
    public interface IJwtTokenGenerateService
    {
        string GenerateToken(string userId, string email, string role);
    }
}
