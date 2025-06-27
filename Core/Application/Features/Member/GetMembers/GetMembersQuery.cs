using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Members.GetMembers
{
    public class GetMembersQuery: IRequest<Result<List<MemberDTO>>>
    {

    }
}
