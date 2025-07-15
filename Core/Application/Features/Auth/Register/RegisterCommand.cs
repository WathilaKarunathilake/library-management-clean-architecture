using LibraryManagementCleanArchitecture.Application.DTO;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Register
{
    public record RegisterCommand(
        string Name,
        string Email,
        string Password,
        string MemberType,
        string StaffType
    ) : IRequest<Result<AuthResultDTO>>;
}
