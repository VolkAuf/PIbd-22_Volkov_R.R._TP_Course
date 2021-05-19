using AllDeductedDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllDeductedDatabaseImplement
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=course;Username=Rafael;Password=1234");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderGroup> OrderGroups { get; set; }
        public virtual DbSet<OrderStudent> OrderStudents { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudyingStatus> StudyingStatuses { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
