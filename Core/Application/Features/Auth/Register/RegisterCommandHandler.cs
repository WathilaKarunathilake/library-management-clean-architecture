using AutoMapper;
using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Application.Contracts.Services;
using LibraryManagementCleanArchitecture.Application.DTO;
using LibraryManagementCleanArchitecture.Application.Response;
using LibraryManagementCleanArchitecture.Domain.Entities;
using LibraryManagementCleanArchitecture.Domain.Errors;
using MediatR;

namespace LibraryManagementCleanArchitecture.Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthResultDTO>>
    {
        private readonly IUserService userService;
        private readonly IRepository<Member> memberRepository;
        private readonly IJwtTokenGenerateService jwtTokenGenerateService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RegisterCommandHandler(
            IUserService userService,
            IRepository<Member> memberRepository,
            IUnitOfWork unitOfWork,
            IJwtTokenGenerateService jwtTokenGenerateService,
            IMapper mapper)
        {
            this.userService = userService;
            this.memberRepository = memberRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jwtTokenGenerateService =jwtTokenGenerateService;
        }

        public async Task<Result<AuthResultDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.CreateUserAsync(request.Name, request.Email, request.Password, request.MemberType.ToUpper());

            if (!result.Succeeded)
                return Result<AuthResultDTO>.Failure(DomainErrors.Custom.Failure(result.Errors));

            if (request.MemberType is not ("library" or "staff"))
                return Result<AuthResultDTO>.Failure(DomainErrors.Identity.RoleNotFound(request.MemberType));

            await userService.AddToRoleAsync(request.Email, request.MemberType);

            Member member = request.MemberType == "library"
                ? new LibraryMember(result.UserId, request.Name)
                : new StaffMember(result.UserId, request.Name, request.StaffType);

            await memberRepository.AddAsync(member);
            await unitOfWork.SaveChangesAsync();

            string token = jwtTokenGenerateService.GenerateToken(result.Name, result.UserId.ToString(), result.Email, request.MemberType.ToUpper());
            return Result<AuthResultDTO>.Success(new AuthResultDTO
            {
                Token = token,
                Message = "User regisration successful !"
            });
        }
    }
}
