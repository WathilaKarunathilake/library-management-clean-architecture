using LibraryManagementCleanArchitecture.Application.Features.Members;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.API.Endpoints
{
    public static class MemberEndpoints
    {
        public static void MapMemberEndpoints(this IEndpointRouteBuilder app)
        {
            var memberGroup = app.MapGroup("/api/members").WithTags("Member Endpoints");

            memberGroup.MapPost("/", AddMember);
            memberGroup.MapGet("/", GetMembers);
        }

        private static async Task<IResult> AddMember(AddMemberCommand addMember, ISender sender)
        {
            try
            {
                var member = await sender.Send(addMember);
                return Results.Created($"/api/members", new ApiResponse<MemberDTO>
                {
                    Data = member
                });
            }
            catch (ArgumentException error)
            {
                return Results.BadRequest(new ApiResponse<string>
                {
                    Data = error.Message,
                    Success = false
                });
            }
        }


        private static async Task<IResult> GetMembers(ISender sender)
        {
            try
            {
                var members =await sender.Send(new GetMembersQuery());
                return Results.Ok(new ApiResponse<IEnumerable<object>>
                {
                    Data = members
                });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new ApiResponse<string>
                {
                    Data = ex.Message,
                    Success = false
                });
            }
        }
    }
}
