using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementCleanArchitecture.Infastrcuture.Identity.Requirnments
{
    public class StaffTypeRequirement : IAuthorizationRequirement
    {
        public string RequiredStaffType { get; }

        public StaffTypeRequirement(string requiredStaffType)
        {
            RequiredStaffType = requiredStaffType;
        }
    }
}
