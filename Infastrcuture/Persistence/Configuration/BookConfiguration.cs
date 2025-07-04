using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.BookId);

            builder.Property(b => b.BookId)
                   .HasColumnName("BookID")
                   .IsRequired();

            builder.Property(b => b.Title)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(b => b.Author)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(b => b.Category)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(b => b.PublicationYear)
                   .IsRequired();


            builder.Property(b => b.Description)
                   .IsRequired();

            builder.Property(b => b.Available)
                   .IsRequired();
        }
    }
}
