using Backend.TechChallenge.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Backend.TechChallenge.Infrastructure.Persistence
{
    public class UserDbContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }

        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", "TechChallenge");
            modelBuilder.Entity<User>(entity => {
                entity.HasKey(user => new {user.Name, user.Address});
                entity.HasIndex(user => user.Email).IsUnique();
                entity.HasIndex(user => user.Phone).IsUnique();
                entity.Property(user => user.Name).HasColumnType("VARCHAR");
                entity.Property(user => user.Email).HasColumnType("VARCHAR");
                entity.Property(user => user.Address).HasColumnType("VARCHAR");
                entity.Property(user => user.Phone).HasColumnType("VARCHAR");
                entity.Property(user => user.UserType).HasColumnType("VARCHAR");
                entity.Property(user => user.Money).HasColumnType("DOUBLE");
            });
            base.OnModelCreating(modelBuilder);

        }


       

    }
}
