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
        public virtual DbSet<Seed> Seeds { get; set; } 
        public virtual DbSet<Yield> Yields { get; set; }
        public virtual DbSet<Retailer> Retailers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ShipmentTrip> ShipmentTrips { get; set; }
        public virtual DbSet<ShipmentImage> ShipmentImages { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PackedProduct> PackedProducts { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<ExpertPermission> ExpertPermissions { get; set; }
        public virtual DbSet<FarmerPermission> FarmerPermissions { get; set; }
        public virtual DbSet<YieldPlan> YieldPlans { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<ProductionTask> ProductionTasks { get; set; }
        public virtual DbSet<HarvestingTask> HarvestingTasks { get; set; }
        public virtual DbSet<PackagingTask> PackagingTasks { get; set; }
        public virtual DbSet<InspectingTask> InspectingTasks { get; set; }
        public virtual DbSet<ProductionImage> ProductionImages { get; set; }
        public virtual DbSet<HarvestingImage> HarvestingImages { get; set; }
        public virtual DbSet<PackagingImage> PackagingImages { get; set; }
        public virtual DbSet<InspectingImage> InspectionImages { get; set; }
        public virtual DbSet<Pesticide> Pesticides { get; set; }
        public virtual DbSet<ProductionPesticide> ProductionPesticides { get; set; }
        public virtual DbSet<Fertilizer> Fertilizers { get; set; }
        public virtual DbSet<ProductionFertilizer> ProductionFertilizers { get; set; }
        public virtual DbSet<ProductionItem> ProductionItems { get; set; }
        public virtual DbSet<HarvestingItem> HarvestingItems { get; set; }
        public virtual DbSet<PackagingItem> PackagingItems { get; set; }
        public virtual DbSet<InspectingItem> InspectingItems { get; set; }
        
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

            modelBuilder.Entity<Seed>()
                .ToTable("Seed");

            modelBuilder.Entity<Yield>()
                .ToTable("Yield");

            modelBuilder.Entity<Retailer>()
                .ToTable("Retailer")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Retailers)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Driver>()
                .ToTable("Driver")
                .HasOne(f => f.Account)
                    .WithMany(f => f.Drivers)
                    .HasForeignKey(f => f.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasOne(o => o.Retailer)
                    .WithMany(o => o.Orders)
                    .HasForeignKey(o => o.RetailerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ShipmentTrip>(entity =>
            {
                entity.ToTable("ShipmentTrip");
                entity.HasOne(st => st.Order)
                      .WithMany(st => st.ShipmentTrips)
                      .HasForeignKey(st => st.OrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(st => st.Driver)
                      .WithMany(st => st.ShipmentTrips)
                      .HasForeignKey(st => st.DriverId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ShipmentImage>()
                .ToTable("ShipmentImage")
                .HasOne(si => si.ShipmentTrip)
                    .WithMany(si => si.ShipmentImages)
                    .HasForeignKey(si => si.ShipmentTripId)
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
                entity.HasOne(p => p.Seed)
                      .WithMany(p => p.Plans)
                      .HasForeignKey(p => p.SeedId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(p => p.Yield)
                      .WithMany(p => p.Plans)
                      .HasForeignKey(p => p.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PackedProduct>()
                .ToTable("PackedProduct")
                .HasOne(pp => pp.Plan)
                    .WithMany(pp => pp.PackedProducts)
                    .HasForeignKey(pp => pp.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(od => new { od.OrderId, od.PackedProductId });
                entity.HasOne(od => od.Order)
                      .WithMany(od => od.OrderDetails)
                      .HasForeignKey(od => od.OrderId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(od => od.PackedProduct)
                      .WithMany(od => od.OrderDetails)
                      .HasForeignKey(od => od.PackedProductId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Device>()
                .ToTable("Device")
                .HasOne(d => d.Yield)
                    .WithMany(d => d.Devices)
                    .HasForeignKey(d => d.YieldId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ExpertPermission>(entity =>
            {
                entity.ToTable("ExpertPermission");
                entity.HasKey(ep => new { ep.PlanId, ep.ExpertId });
                entity.HasOne(ep => ep.Plan)
                      .WithMany(ep => ep.ExpertPermissions)
                      .HasForeignKey(ep => ep.PlanId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(ep => ep.Expert)
                      .WithMany(ep => ep.ExpertPermissions)
                      .HasForeignKey(ep => ep.ExpertId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

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

            modelBuilder.Entity<Period>()
                .ToTable("Period")
                .HasOne(p => p.Plan)
                    .WithMany(p => p.Periods)
                    .HasForeignKey(p => p.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ProductionTask>(entity =>
            {
                entity.ToTable("ProductionTask");
                entity.HasOne(pt => pt.Period)
                      .WithMany(pt => pt.ProductionTasks)
                      .HasForeignKey(pt => pt.PeriodId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.Yield)
                      .WithMany(pt => pt.ProductionTasks)
                      .HasForeignKey(pt => pt.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.Farmer)
                      .WithMany(pt => pt.ProductionTasks)
                      .HasForeignKey(pt => pt.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<HarvestingTask>(entity =>
            {
                entity.ToTable("HarvestingTask");
                entity.HasOne(ht => ht.Period)
                      .WithMany(ht => ht.HarvestingTasks)
                      .HasForeignKey(ht => ht.PeriodId)
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

            modelBuilder.Entity<PackagingTask>(entity =>
            {
                entity.ToTable("PackagingTask");
                entity.HasOne(pt => pt.Period)
                      .WithMany(pt => pt.PackagingTasks)
                      .HasForeignKey(pt => pt.PeriodId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pt => pt.Farmer)
                      .WithMany(pt => pt.PackagingTasks)
                      .HasForeignKey(pt => pt.FarmerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InspectingTask>(entity =>
            {
                entity.ToTable("InspectingTask");
                entity.HasOne(it => it.Period)
                      .WithMany(it => it.InspectingTasks)
                      .HasForeignKey(it => it.PeriodId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(it => it.Yield)
                      .WithMany(it => it.InspectingTasks)
                      .HasForeignKey(it => it.YieldId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(it => it.Expert)
                      .WithMany(it => it.InspectingTasks)
                      .HasForeignKey(it => it.ExpertId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<InspectingImage>()
                .ToTable("InspectionImage")
                .HasOne(im => im.InspectingTask)
                    .WithMany(im => im.InspectingImages)
                    .HasForeignKey(im => im.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Pesticide>()
                .ToTable("Pesticide");

            modelBuilder.Entity<ProductionPesticide>(entity =>
            {
                entity.ToTable("ProductionPesticide");
                entity.HasKey(pp => new { pp.PesticideId, pp.TaskId });
                entity.HasOne(pp => pp.Pesticide)
                      .WithMany(pp => pp.ProductionPesticides)
                      .HasForeignKey(pp => pp.PesticideId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pp => pp.ProductionTask)
                      .WithMany(pp => pp.ProductionPesticides)
                      .HasForeignKey(pp => pp.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
            
            modelBuilder.Entity<Fertilizer>()
                .ToTable("Fertilizer");

            modelBuilder.Entity<ProductionFertilizer>(entity =>
            {
                entity.ToTable("ProductionFertilizer");
                entity.HasKey(pf => new { pf.FertilizerId, pf.TaskId });
                entity.HasOne(pf => pf.Fertilizer)
                      .WithMany(pf => pf.ProductionFertilizers)
                      .HasForeignKey(pf => pf.FertilizerId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pf => pf.ProductionTask)
                      .WithMany(pf => pf.ProductionFertilizers)
                      .HasForeignKey(pf => pf.TaskId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductionImage>()
                .ToTable("ProductionImage")
                .HasOne(pi => pi.ProductionTask)
                    .WithMany(pi => pi.ProductionImages)
                    .HasForeignKey(pi => pi.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            
            modelBuilder.Entity<HarvestingImage>()
                .ToTable("HarvestingImage")
                .HasOne(hi => hi.HarvestingTask)
                    .WithMany(hi => hi.HarvestingImages)
                    .HasForeignKey(hi => hi.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            
            modelBuilder.Entity<PackagingImage>()
                .ToTable("PackagingImage")
                .HasOne(pi => pi.PackagingTask)
                    .WithMany(pi => pi.PackagingImages)
                    .HasForeignKey(pi => pi.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ProductionItem>(entity =>
            {
                entity.ToTable("ProductionItem");
                entity.HasKey(pi => new { pi.ItemId, pi.TaskId });
                entity.HasOne(pi => pi.Item)
                      .WithMany(pi => pi.ProductionItems)
                      .HasForeignKey(pi => pi.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pi => pi.ProductionTask)
                      .WithMany(pi => pi.ProductionItems)
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

            modelBuilder.Entity<PackagingItem>(entity =>
            {
                entity.ToTable("PackagingItem");
                entity.HasKey(pi => new { pi.ItemId, pi.TaskId });
                entity.HasOne(pi => pi.Item)
                      .WithMany(pi => pi.PackagingItems)
                      .HasForeignKey(pi => pi.ItemId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(pi => pi.PackagingTask)
                      .WithMany(pi => pi.PackagingItems)
                      .HasForeignKey(pi => pi.TaskId)
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
                entity.HasOne(ii => ii.InspectingTask)
                      .WithMany(ii => ii.InspectingItems)
                      .HasForeignKey(ii => ii.TaskId)
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
    