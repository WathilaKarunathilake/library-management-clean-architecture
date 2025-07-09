using LibraryManagementCleanArchitecture.Application.DTO;
using LibraryManagementCleanArchitecture.Application.Response;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<AuthResultDTO>>;
}
