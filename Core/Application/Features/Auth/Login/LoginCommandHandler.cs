using LibraryManagementCleanArchitecture.Application.Contracts.Services;
using LibraryManagementCleanArchitecture.Application.DTO;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Errors;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthResultDTO>>
    {
        private readonly IUserService userService;
        private readonly IJwtTokenGenerateService jwtTokenGenerateService;

        public LoginCommandHandler(IUserService userService, IJwtTokenGenerateService jwtTokenGenerateService)
        {
            this.userService = userService;
            this.jwtTokenGenerateService = jwtTokenGenerateService;
        }

        public async Task<Result<AuthResultDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {   
            var user = await userService.CheckPasswordAsync(request.email, request.password);
            if (!user)
            {
                return Result<AuthResultDTO>.Failure(DomainErrors.Identity.InvalidCredentials);
            }
            var userDetails = await userService.GetUserDetailsFromEmail(request.email);

            string token = jwtTokenGenerateService.GenerateToken(userDetails.Name, userDetails.UserId.ToString(), userDetails.Email, userDetails.Role);
            return Result<AuthResultDTO>.Success(new AuthResultDTO
            {
                Token = token,
                Message = "User logged in successfully !"
            }); 
        }
    }
}
