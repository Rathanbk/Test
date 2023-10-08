using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{

        //}
        //public DbSet<Login> Logins { get; set; }
        //public DbSet<Register> Registers { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Expense> Expenses { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                     .Entity<Login>()
                .Property(e => e.Email)
                .HasColumnType("varchar(512)");
                    builder
                 .Entity<Login>()
                .Property(e => e.Password)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Register>()
                .Property(e => e.UserName)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Register>()
                .Property(e => e.FirstName)
                .HasColumnType("varchar(512)");
            builder
               .Entity<Register>()
               .Property(e => e.LastName)
               .HasColumnType("varchar(512)");
            builder
                .Entity<Register>()
                .Property(e => e.Email)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Register>()
                .Property(e => e.Password)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Register>()
                .Property(e => e.ConfirmPassword)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Item>()
                .Property(e => e.ItemName)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Item>()
                .Property(e => e.Borrower)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Item>()
                .Property(e => e.Lender)
                .HasColumnType("varchar(512)");

            builder
                .Entity<Expense>()
                .Property(e => e.ExpenseName)
                .HasColumnType("varchar(512)");
            builder
                .Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("Integer");
            

        }
        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder
       )
        {
            optionsBuilder.UseSqlite("Data Source=Sqlitee2.db");
        }
    }
    }
