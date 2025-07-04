namespace LibraryManagementCleanArchitecture.Domain.Entities
{
    public class Borrowings
    {
        public Guid BorrowingId { get; }
        public Guid MemberId { get; set; }
        public Guid BookId { get; set; }
    }
}
