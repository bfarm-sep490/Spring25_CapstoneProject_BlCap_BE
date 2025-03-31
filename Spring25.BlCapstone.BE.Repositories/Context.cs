using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Helper;

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
        public virtual DbSet<DataEnvironment> DataEnvironments { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<Farmer> Farmers { get; set; }
        public virtual DbSet<FarmerCaringTask> FarmerCaringTasks { get; set; }
        public virtual DbSet<FarmerHarvestingTask> FarmerHarvestingTasks { get; set; }
        public virtual DbSet<FarmerPackagingTask> FarmerPackagingTasks { get; set; }
        public virtual DbSet<FarmerPermission> FarmerPermissions { get; set; }
        public virtual DbSet<Fertilizer> Fertilizers { get; set; }
        public virtual DbSet<HarvestingImage> HarvestingImages { get; set; }
        public virtual DbSet<HarvestingItem> HarvestingItems { get; set; }
        public virtual DbSet<HarvestingTask> HarvestingTasks { get; set; }
        public virtual DbSet<InspectingForm> InspectingForms { get; set; }
        public virtual DbSet<InspectingImage> InspectingImages { get; set; }
        public virtual DbSet<InspectingResult> InspectingResults { get; set; }
        public virtual DbSet<Inspector> Inspectors { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<NotificationExpert> NotificationExperts { get; set; }
        public virtual DbSet<NotificationFarmer> NotificationFarmers { get; set; }
        public virtual DbSet<NotificationOwner> NotificationOwners { get; set; }
        public virtual DbSet<NotificationRetailer> NotificationRetailers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PackagingImage> PackagingImages { get; set; }
        public virtual DbSet<PackagingItem> PackagingItems { get; set; }
        public virtual DbSet<PackagingProduct> PackagingProducts { get; set; }
        public virtual DbSet<PackagingTask> PackagingTasks { get; set; }
        public virtual DbSet<PackagingType> PackagingTypes { get; set; }
        public virtual DbSet<Pesticide> Pesticides { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Plant> Plants { get; set; } 
        public virtual DbSet<PlantYield> PlantYields { get; set; }
        public virtual DbSet<Problem> Problems { get; set; } 
        public virtual DbSet<ProblemImage> ProblemImages { get; set; } 
        public virtual DbSet<Retailer> Retailers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Yield> Yields { get; set; }
        public virtual DbSet<PlanTransaction> PlanTransactions { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .ToTable("Account");

            modelBuilder.Entity<NotificationOwner>()
                .ToTable("NotificationOwner");

            modelBuilder.Entity<Farmer>()
                .ToTable("Farmer")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Farmers)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<NotificationFarmer>()
                .ToTable("NotificationFarmer")
                .HasOne(nf => nf.Farmer)
                    .WithMany(nf => nf.NotificationFarmers)
                    .HasForeignKey(nf => nf.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Expert>()
                .ToTable("Expert")
                .HasOne(i => i.Account)
                    .WithMany(f => f.Experts)
                    .HasForeignKey(i => i.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<NotificationExpert>()
                .ToTable("NotificationExpert")
                .HasOne(ne => ne.Expert)
                    .WithMany(ne => ne.NotificationExperts)
                    .HasForeignKey(ne => ne.ExpertId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Retailer>()
                .ToTable("Retailer")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Retailers)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<NotificationRetailer>()
                .ToTable("NotificationRetailer")
                .HasOne(nr => nr.Retailer)
                    .WithMany(nr => nr.NotificationRetailers)
                    .HasForeignKey(nr => nr.RetailerId)
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

            modelBuilder.Entity<DataEnvironment>()
                .ToTable("DataEnvironment")
                .HasOne(de => de.Yield)
                    .WithMany(de => de.DataEnvironments)
                    .HasForeignKey(de => de.YieldId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PlantYield>(entity =>
            {
                entity.ToTable("PlantYield");
                entity.HasKey(py => new { py.PlantId, py.YieldId });
                entity.HasOne(py => py.Plant)
                      .WithMany(py => py.PlantYields)
                      .HasForeignKey(py => py.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(py => py.Yield)
                      .WithMany(py => py.PlantYields)
                      .HasForeignKey(py => py.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasOne(o => o.Retailer)
                      .WithMany(o => o.Orders)
                      .HasForeignKey(o => o.RetailerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(o => o.Plan)
                      .WithMany(o => o.Orders)
                      .HasForeignKey(o => o.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(o => o.PackagingType)
                      .WithMany(o => o.Orders)
                      .HasForeignKey(o => o.PackagingTypeId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(o => o.Plant)
                      .WithMany(o => o.Orders)
                      .HasForeignKey(o => o.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            }); 

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
                entity.HasOne(p => p.Yield)
                      .WithMany(p => p.Plans)
                      .HasForeignKey(p => p.YieldId)
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

            modelBuilder.Entity<CaringTask>(entity =>
            {
                entity.ToTable("CaringTask");
                entity.HasOne(pt => pt.Plan)
                      .WithMany(pt => pt.CaringTasks)
                      .HasForeignKey(pt => pt.PlanId)
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
            });

            modelBuilder.Entity<InspectingForm>(entity =>
            {
                entity.ToTable("InspectingForm");
                entity.HasOne(it => it.Plan)
                      .WithMany(it => it.InspectingForms)
                      .HasForeignKey(it => it.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(it => it.Inspector)
                      .WithMany(it => it.InspectingForms)
                      .HasForeignKey(it => it.InspectorId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(it => it.InspectingResult)
                      .WithOne(it => it.InspectingForm)
                      .HasForeignKey<InspectingResult>(it => it.Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InspectingResult>()
                .ToTable("InspectingResult");

            modelBuilder.Entity<InspectingImage>()
                .ToTable("InspectingImage")
                .HasOne(ii => ii.InspectingResult)
                    .WithMany(ii => ii.InspectingImages)
                    .HasForeignKey(ii => ii.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Pesticide>()
                .ToTable("Pesticide");

            modelBuilder.Entity<CaringPesticide>(entity =>
            {
                entity.ToTable("CaringPesticide");
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

            modelBuilder.Entity<FarmerCaringTask>(entity =>
            {
                entity.ToTable("FarmerCaringTask");
                entity.HasKey(py => new { py.FarmerId, py.TaskId });
                entity.HasOne(fct => fct.Farmer)
                      .WithMany(fct => fct.FarmerCaringTasks)
                      .HasForeignKey(fct => fct.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(fct => fct.CaringTask)
                      .WithMany(fct => fct.FarmerCaringTasks)
                      .HasForeignKey(fct => fct.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<FarmerHarvestingTask>(entity =>
            {
                entity.ToTable("FarmerHarvestingTask");
                entity.HasKey(py => new { py.FarmerId, py.TaskId });
                entity.HasOne(fht => fht.Farmer)
                      .WithMany(fht => fht.FarmerHarvestingTasks)
                      .HasForeignKey(fht => fht.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(fht => fht.HarvestingTask)
                      .WithMany(fht => fht.FarmerHarvestingTasks)
                      .HasForeignKey(fht => fht.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<FarmerPackagingTask>(entity =>
            {
                entity.ToTable("FarmerPackagingTask");
                entity.HasKey(py => new { py.FarmerId, py.TaskId });
                entity.HasOne(fpt => fpt.Farmer)
                      .WithMany(fpt => fpt.FarmerPackagingTasks)
                      .HasForeignKey(fpt => fpt.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(fpt => fpt.PackagingTask)
                      .WithMany(fpt => fpt.FarmerPackagingTasks)
                      .HasForeignKey(fpt => fpt.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<HarvestingImage>()
                .ToTable("HarvestingImage")
                .HasOne(hi => hi.HarvestingTask)
                    .WithMany(hi => hi.HarvestingImages)
                    .HasForeignKey(hi => hi.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CaringItem>(entity =>
            {
                entity.ToTable("CaringItem");
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
                entity.HasOne(hi => hi.Item)
                      .WithMany(hi => hi.HarvestingItems)
                      .HasForeignKey(hi => hi.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(hi => hi.HarvestingTask)
                      .WithMany(hi => hi.HarvestingItems)
                      .HasForeignKey(hi => hi.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<PackagingTask>(entity =>
            {
                entity.ToTable("PackagingTask");
                entity.HasOne(pt => pt.Plan)
                      .WithMany(pt => pt.PackagingTasks)
                      .HasForeignKey(pt => pt.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.PackagingType)
                      .WithMany(pt => pt.PackagingTasks)
                      .HasForeignKey(pt => pt.PackagingTypeId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PackagingItem>(entity =>
            {
                entity.ToTable("PackagingItem");
                entity.HasOne(pi => pi.Item)
                      .WithMany(pi => pi.PackagingItems)
                      .HasForeignKey(pi => pi.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pi => pi.PackagingTask)
                      .WithMany(pi => pi.PackagingItems)
                      .HasForeignKey(pi => pi.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PackagingImage>()
                .ToTable("PackagingImage")
                .HasOne(pi => pi.PackagingTask)
                    .WithMany(pi => pi.PackagingImages)
                    .HasForeignKey(pi => pi.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Problem>(entity =>
            {
                entity.ToTable("Problem");
                entity.HasOne(p => p.Plan)
                      .WithMany(p => p.Problems)
                      .HasForeignKey(p => p.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(p => p.Farmer)
                      .WithMany(p => p.Problems)
                      .HasForeignKey(p => p.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
                
            modelBuilder.Entity<ProblemImage>()
                .ToTable("ProblemImage")
                .HasOne(p => p.Problem)
                    .WithMany(p => p.ProblemImages)
                    .HasForeignKey(p => p.ProblemId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PackagingProduct>(entity =>
            {
                entity.ToTable("PackagingProduct");
                entity.HasOne(pp => pp.PackagingTask)
                      .WithMany(pp => pp.PackagingProducts)
                      .HasForeignKey(pp => pp.PackagingTaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pp => pp.HarvestingTask)
                      .WithMany(pp => pp.PackagingProducts)
                      .HasForeignKey(pp => pp.HarvestingTaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull); 
            });

            modelBuilder.Entity<PackagingType>()
                .ToTable("PackagingType");

            modelBuilder.Entity<PlantYield>(entity =>
            {
                entity.ToTable("PlantYield");
                entity.HasKey(py => new { py.PlantId, py.YieldId });
                entity.HasOne(py => py.Plant)
                      .WithMany(py => py.PlantYields)
                      .HasForeignKey(py => py.PlantId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(py => py.Yield)
                      .WithMany(py => py.PlantYields)
                      .HasForeignKey(py => py.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PlanTransaction>()
                .ToTable("PlanTransaction")
                .HasOne(pt => pt.Plan)
                    .WithMany(pt => pt.PlanTransactions)
                    .HasForeignKey(pt => pt.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("OrderProduct");
                entity.HasOne(fpt => fpt.Order)
                      .WithMany(fpt => fpt.OrderProducts)
                      .HasForeignKey(fpt => fpt.OrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(fpt => fpt.PackagingProduct)
                      .WithMany(fpt => fpt.OrderProducts)
                      .HasForeignKey(fpt => fpt.ProductId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            FakeDataSeeder.Seed(modelBuilder);
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
    