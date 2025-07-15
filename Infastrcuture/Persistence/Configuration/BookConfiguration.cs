// <copyright file="BookConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
