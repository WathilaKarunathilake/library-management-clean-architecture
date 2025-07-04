using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Application.DTO;
using LibraryManagementCleanArchitecture.Application.Features.Auth.Login;
using LibraryManagementCleanArchitecture.Application.Features.Auth.Register;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public class AuthEndpoints : IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var authGroup = app.MapGroup("/api/auth").WithTags("Authentication Endpoints");

            authGroup.MapPost("/login", LoginUser);
            authGroup.MapPost("/register", RegisterUser);
        }

        private static async Task<IResult> LoginUser(LoginCommand loginCommand, ISender sender)
        {
            var result = await sender.Send(loginCommand);
            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<AuthResultDTO>
                {
                    Data = result.Value,
                    Success = true
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false
            });
        }

        private static async Task<IResult> RegisterUser(RegisterCommand registerCommand, ISender sender)
        {
            var result = await sender.Send(registerCommand);
            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<AuthResultDTO>
                {
                    Data = result.Value,
                    Success = true
                });
            }

            return Results.BadRequest(new ApiResponse<string>
            {
                Data = result.Error,
                Success = false
            });
        }
    }
}