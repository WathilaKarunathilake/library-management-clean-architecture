using LibraryManagementCleanArchitecture.Application.Contracts.Persistence;
using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryManagementCleanArchitecture.Infastrcuture.Identity.Requirnments
{
    public class StaffTypeHandler : AuthorizationHandler<StaffTypeRequirement>
    {
        private readonly IRepository<Member> memberRepository;

        public StaffTypeHandler(IRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, StaffTypeRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }

            var staff = await memberRepository.GetByIdAsync(Guid.Parse(userId));
            if (staff is StaffMember staffMember)
            {
                if (staffMember.StaffType == requirement.RequiredStaffType)
                {
                    context.Succeed(requirement);
                }
            }
        }

    }
}