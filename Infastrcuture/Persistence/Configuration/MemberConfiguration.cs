using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasDiscriminator<string>("MemberType")
                   .HasValue<LibraryMember>("LibraryMember")
                   .HasValue<StaffMember>("StaffMember");

            builder.HasKey(m => m.MemberID);

            builder.Property(m => m.MemberID)
                   .HasColumnName("MemberID")
                   .IsRequired();

            builder.Property(m => m.Name)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
