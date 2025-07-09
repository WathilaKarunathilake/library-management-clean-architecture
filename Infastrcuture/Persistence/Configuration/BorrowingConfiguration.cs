using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementCleanArchitecture.Persistence.Configuration
{
    public class BorrowingsConfiguration : IEntityTypeConfiguration<Borrowings>
    {
        public void Configure(EntityTypeBuilder<Borrowings> builder)
        {
            builder.ToTable("Borrowings");

            builder.HasKey(b => b.BorrowingId);

            builder.Property(b => b.BorrowingId)
                .IsRequired();

            builder.Property(b => b.MemberId)
                .IsRequired();

            builder.Property(b => b.BookId)
                .IsRequired();
        }
    }
}
