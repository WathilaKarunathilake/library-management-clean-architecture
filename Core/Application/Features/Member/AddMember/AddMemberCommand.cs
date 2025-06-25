using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Members.AddMember
{
    public class AddMemberCommand: IRequest<Result<MemberDTO>>
    {
        public string? Name { get; set; }
        public string? MemberType { get; set; }
        public string? StaffType { get; set; }
    }
}
