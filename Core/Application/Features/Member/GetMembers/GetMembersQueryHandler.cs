﻿// <copyright file="GetMembersQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Members.GetMembers
{
    using AutoMapper;
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Contracts.Services;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Core.Application.DTO;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Domain.Errors;
    using MediatR;

    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, Result<List<MemberDTO>>>
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;
        private readonly IUserService userManager;

        public GetMembersQueryHandler(
            IMapper mapper,
            IRepository<Member> memberRepository,
            IUserService userManager)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<Result<List<MemberDTO>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await this.memberRepository.GetAllAsync();
            if (members == null || !members.Any())
            {
                return Result<List<MemberDTO>>.Failure(DomainErrors.Member.NoMembersFound());
            }

            var dtos = new List<MemberDTO>();

            foreach (var member in members)
            {
                MemberDTO? dto = member switch
                {
                    LibraryMember lm => this.mapper.Map<MemberDTO>(lm),
                    StaffMember sm => this.mapper.Map<MemberDTO>(sm),
                    _ => null
                };

                if (dto != null)
                {
                    var email = await this.userManager.GetEmailFromId(member.MemberID.ToString());
                    dto.Email = email;
                    dtos.Add(dto);
                }
            }

            return Result<List<MemberDTO>>.Success(dtos);
        }
    }
}
