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
        public virtual DbSet<CaringFertilizer> CaringFertilizers { get; set; }
        public virtual DbSet<CaringImage> CaringImages { get; set; }
        public virtual DbSet<CaringItem> CaringItems { get; set; }
        public virtual DbSet<CaringPesticide> CaringPesticides { get; set; }
        public virtual DbSet<CaringTask> CaringTasks { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<FarmerPermission> FarmerPermissions { get; set; }
        public virtual DbSet<Fertilizer> Fertilizers { get; set; }
        public virtual DbSet<FertilizerRange> FertilizerRanges { get; set; }
        public virtual DbSet<HarvestingImage> HarvestingImages { get; set; }
        public virtual DbSet<HarvestingItem> HarvestingItems { get; set; }
        public virtual DbSet<HarvestingTask> HarvestingTasks { get; set; }
        public virtual DbSet<InspectingForm> InspectingForms { get; set; }
        public virtual DbSet<InspectingImage> InspectingImages { get; set; }
        public virtual DbSet<InspectingItem> InspectingItems { get; set; }
        public virtual DbSet<Inspector> Inspectors { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderPlan> OrderPlans { get; set; }
        public virtual DbSet<OrderPlant> OrderPlants { get; set; }
        public virtual DbSet<Pesticide> Pesticides { get; set; }
        public virtual DbSet<PesticideRange> PesticideRanges { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Plant> Plants { get; set; } 
        public virtual DbSet<Problem> Problems { get; set; } 
        public virtual DbSet<ProblemImage> ProblemImages { get; set; } 
        public virtual DbSet<Retailer> Retailers { get; set; }
        public virtual DbSet<SampleSolution> SampleSolutions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Yield> Yields { get; set; }
        public virtual DbSet<YieldPlan> YieldPlans { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .ToTable("Account");

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

            modelBuilder.Entity<Retailer>()
                .ToTable("Retailer")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Retailers)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Inspector>()
                .ToTable("Inspector")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Inspectors)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Item>()
                .ToTable("Item");

            modelBuilder.Entity<Plant>()
                .ToTable("Plant");

            modelBuilder.Entity<Yield>()
                .ToTable("Yield");

            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasOne(o => o.Retailer)
                    .WithMany(o => o.Orders)
                    .HasForeignKey(o => o.RetailerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Transaction>()
                .ToTable("Transaction")
                .HasOne(t => t.Order)
                    .WithMany(t => t.Transactions)
                    .HasForeignKey(t => t.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");
                entity.HasOne(p => p.Plant)
                      .WithMany(p => p.Plans)
                      .HasForeignKey(p => p.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(p => p.Expert)
                      .WithMany(p => p.Plans)
                      .HasForeignKey(p => p.ExpertId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OrderPlant>(entity =>
            {
                entity.ToTable("OrderPlant");
                entity.HasKey(op => new { op.PlantId, op.OrderId });
                entity.HasOne(op => op.Plant)
                      .WithMany(op => op.OrderPlants)
                      .HasForeignKey(op => op.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(op => op.Order)
                      .WithMany(op => op.OrderPlants)
                      .HasForeignKey(op => op.OrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<OrderPlan>(entity =>
            {
                entity.ToTable("OrderPlan");
                entity.HasKey(op => new { op.PlanId, op.OrderId });
                entity.HasOne(op => op.Plan)
                      .WithMany(op => op.OrderPlans)
                      .HasForeignKey(op => op.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(op => op.Order)
                      .WithMany(op => op.OrderPlans)
                      .HasForeignKey(op => op.OrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Device>()
                .ToTable("Device")
                .HasOne(d => d.Yield)
                    .WithMany(d => d.Devices)
                    .HasForeignKey(d => d.YieldId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FarmerPermission>(entity =>
            {
                entity.ToTable("FarmerPermission");
                entity.HasKey(fp => new { fp.PlanId, fp.FarmerId });
                entity.HasOne(fp => fp.Plan)
                      .WithMany(fp => fp.FarmerPermissions)
                      .HasForeignKey(ep => ep.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(fp => fp.Farmer)
                      .WithMany(fp => fp.FarmerPermissions)
                      .HasForeignKey(fp => fp.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<YieldPlan>(entity =>
            {
                entity.ToTable("YieldPlan");
                entity.HasKey(yp => new { yp.PlanId, yp.YieldId });
                entity.HasOne(yp => yp.Plan)
                      .WithMany(yp => yp.YieldPlans)
                      .HasForeignKey(yp => yp.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(yp => yp.Yield)
                      .WithMany(yp => yp.YieldPlans)
                      .HasForeignKey(yp => yp.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CaringTask>(entity =>
            {
                entity.ToTable("CaringTask");
                entity.HasOne(pt => pt.Plan)
                      .WithMany(pt => pt.CaringTasks)
                      .HasForeignKey(pt => pt.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.Yield)
                      .WithMany(pt => pt.CaringTasks)
                      .HasForeignKey(pt => pt.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.Farmer)
                      .WithMany(pt => pt.CaringTasks)
                      .HasForeignKey(pt => pt.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.Problem)
                      .WithMany(pt => pt.CaringTasks)
                      .HasForeignKey(pt => pt.ProblemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<HarvestingTask>(entity =>
            {
                entity.ToTable("HarvestingTask");
                entity.HasOne(ht => ht.Plan)
                      .WithMany(ht => ht.HarvestingTasks)
                      .HasForeignKey(ht => ht.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(ht => ht.Yield)
                      .WithMany(ht => ht.HarvestingTasks)
                      .HasForeignKey(ht => ht.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(ht => ht.Farmer)
                      .WithMany(ht => ht.HarvestingTasks)
                      .HasForeignKey(ht => ht.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InspectingForm>(entity =>
            {
                entity.ToTable("InspectingForm");
                entity.HasOne(it => it.Plan)
                      .WithMany(it => it.InspectingForms)
                      .HasForeignKey(it => it.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(it => it.Yield)
                      .WithMany(it => it.InspectingForms)
                      .HasForeignKey(it => it.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(it => it.Inspector)
                      .WithMany(it => it.InspectingForms)
                      .HasForeignKey(it => it.InspectorId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InspectingImage>()
                .ToTable("InspectingImage")
                .HasOne(im => im.InspectingForm)
                    .WithMany(im => im.InspectingImages)
                    .HasForeignKey(im => im.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Pesticide>()
                .ToTable("Pesticide");

            modelBuilder.Entity<CaringPesticide>(entity =>
            {
                entity.ToTable("CaringPesticide");
                entity.HasKey(pp => new { pp.PesticideId, pp.TaskId });
                entity.HasOne(pp => pp.Pesticide)
                      .WithMany(pp => pp.CaringPesticides)
                      .HasForeignKey(pp => pp.PesticideId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pp => pp.CaringTask)
                      .WithMany(pp => pp.CaringPesticides)
                      .HasForeignKey(pp => pp.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<Fertilizer>()
                .ToTable("Fertilizer");

            modelBuilder.Entity<CaringFertilizer>(entity =>
            {
                entity.ToTable("CaringFertilizer");
                entity.HasKey(pf => new { pf.FertilizerId, pf.TaskId });
                entity.HasOne(pf => pf.Fertilizer)
                      .WithMany(pf => pf.CaringFertilizers)
                      .HasForeignKey(pf => pf.FertilizerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pf => pf.CaringTask)
                      .WithMany(pf => pf.CaringFertilizers)
                      .HasForeignKey(pf => pf.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CaringImage>()
                .ToTable("CaringImage")
                .HasOne(pi => pi.CaringTask)
                    .WithMany(pi => pi.CaringImages)
                    .HasForeignKey(pi => pi.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            
            modelBuilder.Entity<HarvestingImage>()
                .ToTable("HarvestingImage")
                .HasOne(hi => hi.HarvestingTask)
                    .WithMany(hi => hi.HarvestingImages)
                    .HasForeignKey(hi => hi.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CaringItem>(entity =>
            {
                entity.ToTable("CaringItem");
                entity.HasKey(pi => new { pi.ItemId, pi.TaskId });
                entity.HasOne(pi => pi.Item)
                      .WithMany(pi => pi.CaringItems)
                      .HasForeignKey(pi => pi.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pi => pi.CaringTask)
                      .WithMany(pi => pi.CaringItems)
                      .HasForeignKey(pi => pi.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<HarvestingItem>(entity =>
            {
                entity.ToTable("HarvestingItem");
                entity.HasKey(hi => new { hi.ItemId, hi.TaskId });
                entity.HasOne(hi => hi.Item)
                      .WithMany(hi => hi.HarvestingItems)
                      .HasForeignKey(hi => hi.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(hi => hi.HarvestingTask)
                      .WithMany(hi => hi.HarvestingItems)
                      .HasForeignKey(hi => hi.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<InspectingItem>(entity =>
            {
                entity.ToTable("InspectingItem");
                entity.HasKey(ii => new { ii.ItemId, ii.TaskId });
                entity.HasOne(ii => ii.Item)
                      .WithMany(ii => ii.InspectingItems)
                      .HasForeignKey(ii => ii.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(ii => ii.InspectingForm)
                      .WithMany(ii => ii.InspectingItems)
                      .HasForeignKey(ii => ii.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SampleSolution>()
                .ToTable("SampleSolution")
                .HasOne(ss => ss.Issue)
                    .WithMany(ss => ss.SampleSolutions)
                    .HasForeignKey(ss => ss.IssueId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Issue>()
                .ToTable("Issue")
                .HasOne(i => i.Problem)
                    .WithMany(i => i.Issues)
                    .HasForeignKey(i => i.ProblemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Problem>()
                .ToTable("Problem")
                .HasOne(p => p.Plan)
                    .WithMany(p => p.Problems)
                    .HasForeignKey(p => p.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ProblemImage>()
                .ToTable("ProblemImage")
                .HasOne(p => p.Problem)
                    .WithMany(p => p.ProblemImages)
                    .HasForeignKey(p => p.ProblemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FertilizerRange>(entity =>
            {
                entity.ToTable("FertilizerRange");
                entity.HasKey(fr => new { fr.PlantId, fr.FertilizerId });
                entity.HasOne(fr => fr.Plant)
                      .WithMany(fr => fr.FertilizerRanges)
                      .HasForeignKey(fr => fr.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(fr => fr.Fertilizer)
                      .WithMany(fr => fr.FertilizerRanges)
                      .HasForeignKey(fr => fr.FertilizerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<PesticideRange>(entity =>
            {
                entity.ToTable("PesticideRange");
                entity.HasKey(pr => new { pr.PlantId, pr.PesticideId });
                entity.HasOne(pr => pr.Plant)
                      .WithMany(pr => pr.PesticideRanges)
                      .HasForeignKey(pr => pr.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pr => pr.Pesticide)
                      .WithMany(pr => pr.PesticideRanges)
                      .HasForeignKey(pr => pr.PesticideId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
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
    