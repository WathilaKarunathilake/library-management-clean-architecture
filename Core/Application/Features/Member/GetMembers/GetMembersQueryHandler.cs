using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using LibraryManagementCleanArchitecture.Domain.Errors;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Members.GetMembers
{
    public class GetMembersQueryHandler: IRequestHandler<GetMembersQuery, Result<List<MemberDTO>>>
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public GetMembersQueryHandler(IMapper mapper, IRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<MemberDTO>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await memberRepository.GetAllAsync();
            if (members == null || !members.Any())
            {
                return Result<List<MemberDTO>>.Failure(DomainErrors.Member.NoMembersFound());
            }

            var dtos = members
                .Select(m => m switch
                {
                    LibraryMember lm => mapper.Map<MemberDTO>(lm),
                    StaffMember sm => mapper.Map<MemberDTO>(sm),
                    _ => null
                })
                .Where(dto => dto != null)
                .ToList();

            return Result<List<MemberDTO>>.Success(dtos);
        }
    }
}
