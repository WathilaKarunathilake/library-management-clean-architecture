using LibraryManagementCleanArchitecture.API.Extensions;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using LibraryManagementCleanArchitecture.Application.Features.Members.GetMembers;
using MediatR;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public class MemberEndpoints: IEndpointGroup
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var memberGroup = app.MapGroup("/api/members").WithTags("Member Endpoints");
            memberGroup.RequireAuthorization();

            memberGroup.MapGet("/", GetMembers);
        }

        private static async Task<IResult> GetMembers(ISender sender)
        {
            var result = await sender.Send(new GetMembersQuery());

            if (result.IsSuccess)
            {
                return Results.Ok(new ApiResponse<List<MemberDTO>>
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
