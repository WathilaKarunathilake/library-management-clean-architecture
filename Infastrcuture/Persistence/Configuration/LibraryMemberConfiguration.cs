// <copyright file="LibraryMemberConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LibraryMemberConfiguration : IEntityTypeConfiguration<LibraryMember>
    {
        public void Configure(EntityTypeBuilder<LibraryMember> builder)
        {
            builder.Property(l => l.BooksBorrowed)
                   .IsRequired();
        }
    }
}
