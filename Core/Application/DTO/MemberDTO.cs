using System.Text.Json.Serialization;

namespace LibraryManagementCleanArchitecture.Core.Application.DTO
{
    public class MemberDTO
    {
        public Guid MemberID { get; set; }
        public string Name { get; set; }
        public string MemberType { get; set; }
        public string Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BooksBorrowed { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StaffType { get; set; }
    }
}
