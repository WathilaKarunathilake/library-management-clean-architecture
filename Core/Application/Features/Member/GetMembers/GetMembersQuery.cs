// <copyright file="GetMembersQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Members.GetMembers
{
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using MediatR;

    public class GetMembersQuery : IRequest<Result<List<MemberDTO>>>
    {
    }
}
