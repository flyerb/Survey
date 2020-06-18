using System;
using System.Collections.Generic;
using System.Text;
using com.sun.tools.doclets.formats.html.resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Survey_Project.Models;

namespace Survey_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<Options> Options { get; set; }
        public DbSet<Questions> Questions { get; set; }

        public DbSet<Responses> Responses { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
            ); 
        }

    }
}
//We can further improve the organization of our code
//by creating sub configuration classes to handle the seed data for each table.