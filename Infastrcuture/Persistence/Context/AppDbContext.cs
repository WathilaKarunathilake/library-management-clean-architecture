using LibraryManagementCleanArchitecture.Domain.Entities;
using LibraryManagementCleanArchitecture.Infastrucuture.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagementCleanArchitecture.Infastrucute.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }

        // Refered to https://www.learnentityframeworkcore.com/configuration/fluent-api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new LibraryMemberConfiguration());
            modelBuilder.ApplyConfiguration(new StaffMemberConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
