using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Interfaces;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Core.Application;
using LibraryManagementCleanArchitecture.Core.Application.DTO;
using LibraryManagementCleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace LibraryManagementCleanArchitecture.Application.Features.Members.AddMember
{
    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, Result<MemberDTO>>
    {
        private readonly IRepository<Member> memberRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddMemberCommandHandler(IRepository<Member> memberRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.memberRepository = memberRepository;
        }

        public async Task<Result<MemberDTO>> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            if (request.MemberType == "library")
            {
                var libraryMember = new LibraryMember(request.Name);
                await memberRepository.AddAsync(libraryMember);
                await unitOfWork.SaveChangesAsync();

                var dto = mapper.Map<MemberDTO>(libraryMember);
                return Result<MemberDTO>.Success(dto);
            }
            else if (request.MemberType == "staff")
            {
                var staffMember = new StaffMember(request.Name, request.StaffType);
                await memberRepository.AddAsync(staffMember);
                await unitOfWork.SaveChangesAsync();

                var dto = mapper.Map<MemberDTO>(staffMember);
                return Result<MemberDTO>.Success(dto);
            }
            else
            {
                return Result<MemberDTO>.Failure("Invalid member type");
            }
        }
    }
}
