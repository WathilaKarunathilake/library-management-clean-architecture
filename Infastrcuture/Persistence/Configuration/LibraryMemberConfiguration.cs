using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
    public class LibraryMemberConfiguration : IEntityTypeConfiguration<LibraryMember>
    {
        public void Configure(EntityTypeBuilder<LibraryMember> builder)
        {
            builder.Property(l => l.BooksBorrowed)
                   .IsRequired();
        }
    }
}
