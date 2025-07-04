namespace LibraryManagementCleanArchitecture.Core.Application.DTO
{
    public class BookDTO
    {
        public Guid BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public bool Available { get; set; }
    }
}
