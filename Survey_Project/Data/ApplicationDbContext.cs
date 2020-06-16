using System;
using System.Collections.Generic;
using System.Text;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<CustomerSurvey>()
            //.HasKey(cs => new {cs.CustomerId, cs.SurveyId });
            //builder.Entity<CustomerSurvey>()
            //    .HasOne(bc => bc.customer)
            //    .WithMany(b => b.survey)
            //    .HasForeignKey(bc => bc.CustomerId);
            //builder.Entity<CustomerSurvey>()
            //    .HasOne(bc => bc.survey)
            //    .WithMany(c => c.customer)
            //    .HasForeignKey(bc => bc.SurveyId);
            builder.Entity<CustomerSurvey>().HasKey(cs => new { cs.CustomerId, cs.SurveyId });

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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        public DbSet<CustomerSurvey> customerSurveys { get; set; }

       // public DbSet<Survey_Project.Models.Customer> Customer { get; set; }
    }
}
//We can further improve the organization of our code
//by creating sub configuration classes to handle the seed data for each table.