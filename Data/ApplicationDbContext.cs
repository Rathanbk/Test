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
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        //{
                
        //}
        public DbSet<Login> Logins { get; set; }
        public DbSet<Register> Registers { get; set; }
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
                .Property(e => e.Name)
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

        }
        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder
       )
        {
            optionsBuilder.UseSqlite("Data Source=Sqlitee.db");
        }
    }
    }
