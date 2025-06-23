using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Members
{
    public class AddMemberCommand: IRequest<MemberDTO>
    {
        public string? Name { get; set; }
        public string? MemberType { get; set; }
        public string? StaffType { get; set; }
    }
}
