using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Spring25.BlCapstone.BE.Repositories.Models;

namespace Spring25.BlCapstone.BE.Repositories
{
    public class Context : DbContext
    {  
        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<FarmOwner> FarmOwners { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<ImageField> ImageFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FarmOwner>()
                .ToTable("FarmOwner")
                .HasMany(f => f.Fields)
                    .WithOne(f => f.FarmOwner)
                    .HasForeignKey(f => f.FarmOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Field>()
                .ToTable("Field")
                .HasMany(f => f.ImageFields)
                    .WithOne(i => i.Field)
                    .HasForeignKey(i => i.FieldId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ImageField>()
                .ToTable("ImageField") 
                .HasOne(i => i.Field)   
                    .WithMany(f => f.ImageFields)
                    .HasForeignKey(i => i.FieldId)  
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
