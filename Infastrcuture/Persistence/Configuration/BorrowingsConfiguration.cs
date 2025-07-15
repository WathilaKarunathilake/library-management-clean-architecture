// <copyright file="BorrowingsConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Persistence.Configuration
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
