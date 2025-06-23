using AutoMapper;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Core.Domain;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Features.Members
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, MemberDTO>
    {
        private readonly IRepository<Member> memberRepository;

        private readonly IMapper mapper;

        public AddMemberCommandHandler(IRepository<Member> memberRepository, IMapper mapper)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        public async Task<MemberDTO> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            Member member;
            if (request.MemberType == "library")
            {
                member = new LibraryMember(request.Name);
            } else if (request.MemberType == "staff")
            {
                member = new StaffMember(request.Name, request.StaffType);
            } else
            {
                throw new ArgumentException("Invalid member type");
            }
            
            await memberRepository.AddAsync(member);
            await memberRepository.SaveAsync();

            return mapper.Map<MemberDTO>(member);
        }
    }
}
