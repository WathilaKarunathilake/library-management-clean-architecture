using System;

namespace LibraryManagementCleanArchitecture.Core.Application.DTO
{
    public class MemberDTO
    {
        public Guid memberID { get; set; }
        public string? Name { get; set; }
        public string? MemberType { get; set; }  
        public string? StaffType { get; set; }
        public int booksBorrowed { get; set; } = null;
    }
}
