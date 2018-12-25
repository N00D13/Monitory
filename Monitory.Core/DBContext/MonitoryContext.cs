using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Monitory.Core.Models;

namespace Monitory.Core.DBContext
{
    public class MonitoryContext : DbContext
    {
        public MonitoryContext()
        {
        }

        public MonitoryContext(DbContextOptions<MonitoryContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<WebCheck> WebChecks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .ToTable("Account")
                .HasIndex(u => u.Username)
                .IsUnique(true);

            modelBuilder.Entity<Task>()
                .ToTable("Task")
                .HasIndex(t => t.TaskID)
                .IsUnique(true);

            modelBuilder.Entity<WebCheck>()
                .ToTable("WebCheck")
                .HasIndex(wc => wc.WebCheckID)
                .IsUnique(true);
        }
    }
}
