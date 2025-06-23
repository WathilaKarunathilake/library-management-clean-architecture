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
    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<object>>
    {
        private readonly IRepository<Member> memberRepository;

        public GetMembersQueryHandler(IRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public async Task<IEnumerable<object>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
           var members = await memberRepository.GetAllAsync();
           return members;
            
        }
    }
}
