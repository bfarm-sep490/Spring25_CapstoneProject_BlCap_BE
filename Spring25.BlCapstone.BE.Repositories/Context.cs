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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .ToTable("Account");

            modelBuilder.Entity<Item>()
                .ToTable("Item");

            modelBuilder.Entity<Farmer>()
                .ToTable("Farmer")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Farmers)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Expert>()
                .ToTable("Expert")
                .HasOne(i => i.Account)
                    .WithMany(f => f.Experts)
                    .HasForeignKey(i => i.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            /*modelBuilder.Entity<Pesticide>(entity =>
            {
                entity.ToTable("Pesticide");
                entity.HasOne(d => d.Owner).WithMany(p => p.Pesticides)
                .HasForeignKey(p => p.FarmOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pesticide_FarmOwner");
            });

            modelBuilder.Entity<Fertilizer>(entity =>
            {
                entity.ToTable("Fertilizer");
                entity.HasOne(d => d.Owner).WithMany(p => p.Fertilizers)
                .HasForeignKey(p => p.FarmOwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fertilizer_FarmOwner");
            });

            modelBuilder.Entity<Farmer>().ToTable("Farmer");

            modelBuilder.Entity<Seed>(entity =>
            {
                entity.ToTable("Seed");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");

                entity.HasOne(p => p.Field)
                      .WithMany(f => f.Plans)
                      .HasForeignKey(p => p.FieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(p => p.Seed)
                      .WithMany(s => s.Plans)
                      .HasForeignKey(p => p.SeedId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("Period");
                entity.HasOne(e => e.Plan)
                      .WithMany(p => p.Periods)
                      .HasForeignKey(e => e.PlanId);
            });

            modelBuilder.Entity<Models.Task>(entity =>
            {
                entity.ToTable("Task");
                entity.HasOne(e => e.Period)
                      .WithMany(p => p.Tasks)
                      .HasForeignKey(e => e.PeriodId);
                entity.HasOne(e => e.Farmer)
                      .WithMany(f => f.Tasks)
                      .HasForeignKey(e => e.FarmerId);
            });

            modelBuilder.Entity<ImageReport>(entity =>
            {
                entity.ToTable("ImageReport");
                entity.HasOne(e => e.Task)
                      .WithMany(t => t.ImageReports)
                      .HasForeignKey(e => e.TaskId);
            });

            modelBuilder.Entity<TaskFertilizer>(entity =>
            {
                entity.ToTable("TaskFertilizer");
                entity.HasKey(tf => new { tf.FertilizerId, tf.TaskId });
                entity.HasOne(tf => tf.Fertilizer)
                      .WithMany(tf => tf.TaskFertilizers)
                      .HasForeignKey(tf => tf.FertilizerId);
                entity.HasOne(tf => tf.Task)
                      .WithMany(t => t.TaskFertilizers)
                      .HasForeignKey(tf => tf.TaskId);
            });

            modelBuilder.Entity<TaskPesticide>(entity =>
            {
                entity.ToTable("TaskPesticide");
                entity.HasKey(tp => new { tp.PesticideId, tp.TaskId });
                entity.HasOne(tp => tp.Pesticide)
                      .WithMany(tf => tf.TaskPesticides)
                      .HasForeignKey(tp => tp.PesticideId);
                entity.HasOne(tp => tp.Task)
                      .WithMany(t => t.TaskPesticides)
                      .HasForeignKey(tp => tp.TaskId);
            });
            */
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
    