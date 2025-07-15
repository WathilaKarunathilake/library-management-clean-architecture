// <copyright file="RegisterCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Register
{
    using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
    using LibraryManagementCleanArchitecture.Application.Contracts.Services;
    using LibraryManagementCleanArchitecture.Application.DTO;
    using LibraryManagementCleanArchitecture.Application.Response;
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using LibraryManagementCleanArchitecture.Domain.Errors;
    using MediatR;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResultDTO>>
    {
        private readonly IUserService userService;
        private readonly IRepository<Member> memberRepository;
        private readonly IJwtTokenGenerateService jwtTokenGenerateService;
        private readonly IUnitOfWork unitOfWork;

        public RegisterCommandHandler(
            IUserService userService,
            IRepository<Member> memberRepository,
            IUnitOfWork unitOfWork,
            IJwtTokenGenerateService jwtTokenGenerateService)
        {
            this.userService = userService;
            this.memberRepository = memberRepository;
            this.unitOfWork = unitOfWork;
            this.jwtTokenGenerateService = jwtTokenGenerateService;
        }

        public async Task<Result<AuthResultDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await this.userService.CreateUserAsync(request.Name, request.Email, request.Password, request.MemberType.ToUpper());

            if (!result.Succeeded)
            {
                return Result<AuthResultDTO>.Failure(DomainErrors.Custom.Failure(result.Errors));
            }

            if (request.MemberType is not ("library" or "staff"))
            {
                return Result<AuthResultDTO>.Failure(DomainErrors.Identity.RoleNotFound(request.MemberType));
            }

            await this.userService.AddToRoleAsync(request.Email, request.MemberType);

            Member member = request.MemberType == "library"
                ? new LibraryMember(result.UserId, request.Name)
                : new StaffMember(result.UserId, request.Name, request.StaffType);

            await this.memberRepository.AddAsync(member);
            await this.unitOfWork.SaveChangesAsync();

            string token = this.jwtTokenGenerateService.GenerateToken(result.Name, result.UserId.ToString(), result.Email, request.MemberType.ToUpper());
            return Result<AuthResultDTO>.Success(new AuthResultDTO
            {
                Token = token,
                Message = "User regisration successful !",
            });
        }
    }
}
