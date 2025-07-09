using Microsoft.AspNetCore.Identity;

namespace LibraryManagementCleanArchitecture.Infastrcuture.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
