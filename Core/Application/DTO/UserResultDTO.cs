namespace LibraryManagementCleanArchitecture.Application.DTO
{
    public class UserResultDTO
    {
        public bool Succeeded { get; set; }
        public string Errors { get; set; }
        public Guid UserId { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
    }
}
