using LibraryManagementCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration
{
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
