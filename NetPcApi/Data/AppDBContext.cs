using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetPcApi.Models;

namespace NetPcApi.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        // tabele w sql
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasMany(c => c.Contacts)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SubCategory>()
                .HasMany(sc => sc.Contacts)
                .WithOne(c => c.SubCategory)
                .HasForeignKey(c => c.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id = "Admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Id = "User",
                    Name = "User",
                    NormalizedName = "USER"
                },
            };

            builder.Entity<IdentityRole>().HasData(roles); // system ról, można później rozbudować aplikacji o dodatkowe możliwości użytkownika
        }
    }
}