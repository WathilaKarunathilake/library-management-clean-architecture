using System.Text.Json.Serialization;

namespace LibraryManagementCleanArchitecture.Core.Application.DTO
{
    public class MemberDTO
    {
        public Guid MemberID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MemberType { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BooksBorrowed { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StaffType { get; set; }
    }
}
