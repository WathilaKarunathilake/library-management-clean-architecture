// <copyright file="StaffMemberConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>
namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
    using LibraryManagementCleanArchitecture.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StaffMemberConfiguration : IEntityTypeConfiguration<StaffMember>
    {
        public void Configure(EntityTypeBuilder<StaffMember> builder)
        {
            builder.Property(s => s.StaffType)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
