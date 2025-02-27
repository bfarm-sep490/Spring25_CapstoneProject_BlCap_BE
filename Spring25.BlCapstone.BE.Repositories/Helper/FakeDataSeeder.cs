﻿using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Spring25.BlCapstone.BE.Repositories.Helper
{
    public static class FakeDataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Name = "Nguyễn Văn A", Email = "nguyenvana@gmail.com", Role = "Inspector", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 2, Name = "Trần Thị B", Email = "farmer@gmail.com", Role = "Farmer", Password = "1@", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 3, Name = "Lê Quang C", Email = "lequangc@gmail.com", Role = "Expert", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 4, Name = "Phạm Minh D", Email = "phaminhd@gmail.com", Role = "Farmer", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 5, Name = "Nguyễn Bình Phương Trâm", Email = "tramnbp@gmail.com", Role = "Farmer", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 6, Name = "Nguyễn Thiện F", Email = "nguyenthienf@gmail.com", Role = "Farmer", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 7, Name = "Trần Bích G", Email = "tranbichg@gmail.com", Role = "Farmer", Password = "1234", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 8, Name = "Lê Sơn H", Email = "inspector@gmail.com", Role = "Inspector", Password = "1@", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 9, Name = "Phạm Tuan I", Email = "phamtuani@gmail.com", Role = "Expert", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 10, Name = "Hoàng Quỳnh J", Email = "expert@gmail.com", Role = "Expert", Password = "1@", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 11, Name = "Trịnh Xuân Admin", Email = "farmowner@gmail.com", Role = "Farm Owner", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 12, Name = "Trịnh Hữu Tuấn", Email = "retailer@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 13, Name = "Vũ Hoàng Duy Khánh", Email = "khanhvhd@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 14, Name = "Lê Quốc Khánh", Email = "khanhlq@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Retailer>().HasData(
                new Retailer { Id = 1, AccountId = 12, LongxLat = "10.7769,106.7009", Address = "123 Đường Lê Lợi, Quận 1, TP.HCM", Phone = "0901234567", DOB = new DateTime(1985, 5, 10), Avatar = "avatar1.jpg", Status = "Active" },
                new Retailer { Id = 2, AccountId = 13, LongxLat = "10.7627,106.6822", Address = "456 Đường Nguyễn Huệ, Quận 1, TP.HCM", Phone = "0912345678", DOB = new DateTime(1990, 8, 20), Avatar = "avatar2.jpg", Status = "Inactive" },
                new Retailer { Id = 3, AccountId = 14, LongxLat = "10.8231,106.6297", Address = "789 Đường Phạm Văn Đồng, Quận Thủ Đức, TP.HCM", Phone = "0923456789", DOB = new DateTime(1995, 12, 15), Avatar = "avatar3.jpg", Status = "Pending" }
            );

            modelBuilder.Entity<Expert>().HasData(
                new Expert { Id = 1, AccountId = 3, DOB = new DateTime(1985, 5, 20), Phone = "0912345678", Status = "Active", Avatar = "avatar1.jpg" },
                new Expert { Id = 2, AccountId = 9, DOB = new DateTime(1990, 8, 15), Phone = "0987654321", Status = "Inactive", Avatar = "avatar2.jpg" },
                new Expert { Id = 3, AccountId = 10, DOB = new DateTime(1995, 12, 5), Phone = "0971122334", Status = "Active", Avatar = "avatar3.jpg" }
            );

            modelBuilder.Entity<Farmer>().HasData(
                new Farmer { Id = 1, AccountId = 2, DOB = new DateTime(1980, 4, 15), Phone = "0901234567", Status = "Active", Avatar = "farmer1.jpg" },
                new Farmer { Id = 2, AccountId = 4, DOB = new DateTime(1985, 7, 10), Phone = "0912345678", Status = "Inactive", Avatar = "farmer2.jpg" },
                new Farmer { Id = 3, AccountId = 5, DOB = new DateTime(1990, 9, 25), Phone = "0923456789", Status = "Active", Avatar = "farmer3.jpg" },
                new Farmer { Id = 4, AccountId = 6, DOB = new DateTime(1995, 11, 30), Phone = "0934567890", Status = "Pending", Avatar = "farmer4.jpg" },
                new Farmer { Id = 5, AccountId = 7, DOB = new DateTime(2000, 1, 5), Phone = "0945678901", Status = "Active", Avatar = "farmer5.jpg" }
            );

            modelBuilder.Entity<Inspector>().HasData(
                new Inspector
                {
                    Id = 1,
                    AccountId = 1,
                    Description = "Experienced agricultural inspector with 10 years in the field.",
                    Address = "123 Green Farm Road, Hanoi",
                    Status = "Active",
                    ImageUrl = "inspector1.jpg",
                    IsAvailable = true
                },
                new Inspector
                {
                    Id = 2,
                    AccountId = 8,
                    Description = "Expert in organic certification and food safety.",
                    Address = "456 Eco Farm Lane, Ho Chi Minh City",
                    Status = "Inactive",
                    ImageUrl = "inspector2.jpg",
                    IsAvailable = false
                }
            );

            modelBuilder.Entity<Plant>().HasData(
                new Plant
                {
                    Id = 1,
                    PlantName = "Rau muống",
                    Quantity = 100,
                    Unit = "kg",
                    Description = "Rau muống xanh tốt, dễ trồng",
                    IsAvailable = true,
                    MinTemp = 20,
                    MaxTemp = 35,
                    MinHumid = 60,
                    MaxHumid = 80,
                    MinMoisture = 30,
                    MaxMoisture = 70,
                    MinFertilizer = 1,
                    MaxFertilizer = 5,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 2,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 5,
                    MaxBrixPoint = 8,
                    GTTestKitColor = "Green", 
                    ImageUrl = "https://lh3.googleusercontent.com/Yjr46vcV8-zXGaD88d-3_VV34Lcttz5Je5kzzlP4__C6HaCVPw82CZhOsqvF9QusGFr1Gbqb9wNDYglguUJhB9jA5tE1NY5a=rw"
                },
                new Plant
                {
                    Id = 2,
                    PlantName = "Cải xanh",
                    Quantity = 80,
                    Unit = "kg",
                    Description = "Cải xanh tươi tốt, thích hợp khí hậu ôn hòa",
                    IsAvailable = true,
                    MinTemp = 18,
                    MaxTemp = 28,
                    MinHumid = 65,
                    MaxHumid = 85,
                    MinMoisture = 35,
                    MaxMoisture = 75,
                    MinFertilizer = 1.5,
                    MaxFertilizer = 4,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 1.5,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 4,
                    MaxBrixPoint = 7,
                    GTTestKitColor = "Light Green",
                    ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/390/808/products/417703-aadd29cc34ac4e77ad0a253a570c41fd-large.jpg?v=1592815671443"
                },
                new Plant
                {
                    Id = 3,
                    PlantName = "Cà chua",
                    Quantity = 150,
                    Unit = "kg",
                    Description = "Cà chua đỏ mọng, giàu dinh dưỡng",
                    IsAvailable = true,
                    MinTemp = 22,
                    MaxTemp = 30,
                    MinHumid = 70,
                    MaxHumid = 85,
                    MinMoisture = 40,
                    MaxMoisture = 75,
                    MinFertilizer = 2,
                    MaxFertilizer = 6,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0.5,
                    MaxPesticide = 2,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 6,
                    MaxBrixPoint = 10,
                    GTTestKitColor = "Red",
                    ImageUrl = "https://product.hstatic.net/200000423303/product/ca-chua-bee-cherry-huu-co_2afe5b08b1f242809cac54171701fff4_1024x1024.jpg"
                },
                new Plant
                {
                    Id = 4,
                    PlantName = "Bí đỏ",
                    Quantity = 120,
                    Unit = "kg",
                    Description = "Bí đỏ thơm ngon, giàu vitamin",
                    IsAvailable = true,
                    MinTemp = 23,
                    MaxTemp = 32,
                    MinHumid = 65,
                    MaxHumid = 80,
                    MinMoisture = 35,
                    MaxMoisture = 70,
                    MinFertilizer = 3,
                    MaxFertilizer = 7,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 1.5,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 5,
                    MaxBrixPoint = 9,
                    GTTestKitColor = "Orange",
                    ImageUrl = "https://product.hstatic.net/200000459373/product/b9edb47fb13ffa61a2f24d9de633ee32_f7dc0339ec9644ae93f7eb26644592d8_master.jpg"
                },
                new Plant
                {
                    Id = 5,
                    PlantName = "Ớt chuông",
                    Quantity = 90,
                    Unit = "kg",
                    Description = "Ớt chuông đa màu sắc, giàu vitamin C",
                    IsAvailable = true,
                    MinTemp = 21,
                    MaxTemp = 28,
                    MinHumid = 60,
                    MaxHumid = 75,
                    MinMoisture = 30,
                    MaxMoisture = 65,
                    MinFertilizer = 2,
                    MaxFertilizer = 5,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0.2,
                    MaxPesticide = 1.2,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 4,
                    MaxBrixPoint = 8,
                    GTTestKitColor = "Yellow",
                    ImageUrl = "https://bizweb.dktcdn.net/100/021/951/products/ot-chuong-do.jpg?v=1626768715773"
                },
                new Plant
                {
                    Id = 6,
                    PlantName = "Hành lá",
                    Quantity = 110,
                    Unit = "kg",
                    Description = "Hành lá thơm ngon, thích hợp ăn kèm",
                    IsAvailable = true,
                    MinTemp = 18,
                    MaxTemp = 26,
                    MinHumid = 60,
                    MaxHumid = 80,
                    MinMoisture = 30,
                    MaxMoisture = 60,
                    MinFertilizer = 1,
                    MaxFertilizer = 4,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 1,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 5,
                    MaxBrixPoint = 7,
                    GTTestKitColor = "Green",
                    ImageUrl = "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2020/9/30/840429/Hanh-La-Tot-Suc-Khoe.jpg"
                },
                new Plant
                {
                    Id = 7,
                    PlantName = "Ngò rí",
                    Quantity = 70,
                    Unit = "kg",
                    Description = "Ngò rí tươi tốt, thơm ngon",
                    IsAvailable = true,
                    MinTemp = 20,
                    MaxTemp = 28,
                    MinHumid = 60,
                    MaxHumid = 75,
                    MinMoisture = 35,
                    MaxMoisture = 65,
                    MinFertilizer = 1,
                    MaxFertilizer = 3.5,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 1.2,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 3,
                    MaxBrixPoint = 6,
                    GTTestKitColor = "Green",
                    ImageUrl = "https://images-handler.kamereo.vn/eyJidWNrZXQiOiJpbWFnZS1oYW5kbGVyLXByb2QiLCJrZXkiOiJzdXBwbGllci82NTQvUFJPRFVDVF9JTUFHRS81ZmVjZTkxMy1lMzFlLTRlYjQtOGU3ZC0wNDEzZjA1MDFmZDEuanBnXzE3MDBXeDE3MDBIIn0="
                },
                new Plant
                {
                    Id = 8,
                    PlantName = "Cải bó xôi",
                    Quantity = 85,
                    Unit = "kg",
                    Description = "Cải bó xôi giàu sắt, tốt cho sức khỏe",
                    IsAvailable = true,
                    MinTemp = 18,
                    MaxTemp = 26,
                    MinHumid = 65,
                    MaxHumid = 85,
                    MinMoisture = 40,
                    MaxMoisture = 75,
                    MinFertilizer = 1.5,
                    MaxFertilizer = 4.5,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0.3,
                    MaxPesticide = 1.5,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 5,
                    MaxBrixPoint = 9,
                    GTTestKitColor = "Dark Green",
                    ImageUrl = "https://product.hstatic.net/1000354044/product/spinach-with-root_a908f4303613407aa4212ff58f0a2301_master.jpg"
                },
                new Plant
                {
                    Id = 9,
                    PlantName = "Dưa leo",
                    Quantity = 130,
                    Unit = "kg",
                    Description = "Dưa leo giòn, tươi ngon",
                    IsAvailable = true,
                    MinTemp = 20,
                    MaxTemp = 32,
                    MinHumid = 65,
                    MaxHumid = 85,
                    MinMoisture = 45,
                    MaxMoisture = 75,
                    MinFertilizer = 2.5,
                    MaxFertilizer = 6,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 2,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 3,
                    MaxBrixPoint = 7,
                    GTTestKitColor = "Light Green",
                    ImageUrl = "https://hoayeuthuong.com/hinh-hoa-tuoi/moingay/11896_dua-leo-lon-kg.jpg"
                },
                new Plant
                {
                    Id = 10,
                    PlantName = "Củ cải trắng",
                    Quantity = 95,
                    Unit = "kg",
                    Description = "Củ cải trắng tươi ngon, giàu dinh dưỡng",
                    IsAvailable = true,
                    MinTemp = 18,
                    MaxTemp = 28,
                    MinHumid = 60,
                    MaxHumid = 80,
                    MinMoisture = 30,
                    MaxMoisture = 70,
                    MinFertilizer = 1.5,
                    MaxFertilizer = 4,
                    FertilizerUnit = "kg/ha",
                    MinPesticide = 0,
                    MaxPesticide = 1.5,
                    PesticideUnit = "ml/L",
                    MinBrixPoint = 4,
                    MaxBrixPoint = 8,
                    GTTestKitColor = "White",
                    ImageUrl = "https://cdn-ikpgcep.nitrocdn.com/ZvpbNFNEnLdyaCvAOioeTonnTVKjUsWC/assets/images/optimized/rev-62fa9b2/phanthietstore.com/wp-content/uploads/2024/03/13-1-2.jpg"
                }
            );

            modelBuilder.Entity<Yield>().HasData(
                new Yield { Id = 1, YieldName = "Đồng lúa A", AreaUnit = "hectare", Area = 2.5, Description = "Đồng lúa trồng lúa nước", Type = "Lúa", IsAvailable = true, Size = "Lớn" },
                new Yield { Id = 2, YieldName = "Vườn rau B", AreaUnit = "square meter", Area = 500, Description = "Vườn rau hữu cơ", Type = "Rau", IsAvailable = true, Size = "Nhỏ" },
                new Yield { Id = 3, YieldName = "Nông trại C", AreaUnit = "hectare", Area = 5, Description = "Nông trại đa cây", Type = "Tổng hợp", IsAvailable = true, Size = "Lớn" },
                new Yield { Id = 4, YieldName = "Trang trại D", AreaUnit = "hectare", Area = 3, Description = "Trang trại cây ăn quả", Type = "Trái cây", IsAvailable = false, Size = "Vừa" },
                new Yield { Id = 5, YieldName = "Vùng trồng E", AreaUnit = "square meter", Area = 800, Description = "Vùng trồng rau sạch", Type = "Rau", IsAvailable = true, Size = "Nhỏ" },
                new Yield { Id = 6, YieldName = "Cánh đồng F", AreaUnit = "hectare", Area = 4, Description = "Cánh đồng trồng ngô", Type = "Ngô", IsAvailable = true, Size = "Vừa" },
                new Yield { Id = 7, YieldName = "Đất trống G", AreaUnit = "hectare", Area = 6, Description = "Đất trống chờ canh tác", Type = "Khác", IsAvailable = false, Size = "Lớn" }
            );

            modelBuilder.Entity<Fertilizer>().HasData(
                new Fertilizer { Id = 1, Name = "Phân Ure", Description = "Phân đạm Ure giúp cây phát triển xanh tốt", Image = "ure.png", Unit = "kg", AvailableQuantity = 1000, TotalQuantity = 5000, Status = "Available", Type = "Đạm" },
                new Fertilizer { Id = 2, Name = "Phân Kali", Description = "Phân Kali giúp cây tăng sức đề kháng", Image = "kali.png", Unit = "kg", AvailableQuantity = 800, TotalQuantity = 4000, Status = "Available", Type = "Kali" },
                new Fertilizer { Id = 3, Name = "Phân Lân", Description = "Phân Lân kích thích rễ phát triển", Image = "lan.png", Unit = "kg", AvailableQuantity = 600, TotalQuantity = 3000, Status = "Available", Type = "Lân" },
                new Fertilizer { Id = 4, Name = "Phân Hữu Cơ", Description = "Phân hữu cơ giúp cải tạo đất", Image = "huuco.png", Unit = "kg", AvailableQuantity = 500, TotalQuantity = 2500, Status = "Available", Type = "Hữu cơ" },
                new Fertilizer { Id = 5, Name = "Phân Vi Sinh", Description = "Phân vi sinh chứa vi khuẩn có lợi", Image = "visinh.png", Unit = "kg", AvailableQuantity = 700, TotalQuantity = 3500, Status = "Available", Type = "Vi sinh" }
            );

            modelBuilder.Entity<Pesticide>().HasData(
                new Pesticide { Id = 1, Name = "Thuốc Trừ Sâu Regent", Description = "Diệt sâu hại hiệu quả", Image = "regent.png", Unit = "ml", AvailableQuantity = 500, TotalQuantity = 2000, Status = "Available", Type = "Trừ sâu" },
                new Pesticide { Id = 2, Name = "Thuốc Trừ Nấm Ridomil", Description = "Chống nấm bệnh cho cây trồng", Image = "ridomil.png", Unit = "g", AvailableQuantity = 700, TotalQuantity = 3000, Status = "Available", Type = "Trừ nấm" },
                new Pesticide { Id = 3, Name = "Thuốc Diệt Cỏ Glyphosate", Description = "Diệt cỏ hiệu quả, an toàn", Image = "glyphosate.png", Unit = "l", AvailableQuantity = 400, TotalQuantity = 1500, Status = "Available", Type = "Diệt cỏ" },
                new Pesticide { Id = 4, Name = "Thuốc Trừ Rầy Confidor", Description = "Đặc trị rầy nâu, rệp sáp", Image = "confidor.png", Unit = "ml", AvailableQuantity = 600, TotalQuantity = 2500, Status = "Available", Type = "Trừ rầy" },
                new Pesticide { Id = 5, Name = "Thuốc Trừ Bọ Xít Karate", Description = "Diệt bọ xít, nhện đỏ", Image = "karate.png", Unit = "ml", AvailableQuantity = 800, TotalQuantity = 3500, Status = "Available", Type = "Trừ bọ xít" }
            );

            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, PlantId = 1, YieldId = 1, ExpertId = 1, PlanName = "Trồng cà chua vụ đông", Description = "Kế hoạch trồng cà chua vào mùa đông", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 4, 15), Status = "Ongoing", EstimatedProduct = 500, EstimatedUnit = "kg", AvailablePackagingQuantity = 200, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 2, PlantId = 2, YieldId = 2, ExpertId = 2, PlanName = "Trồng dưa lưới", Description = "Kế hoạch trồng dưa lưới trong nhà kính", StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 6, 1), Status = "Pending", EstimatedProduct = 300, EstimatedUnit = "kg", AvailablePackagingQuantity = 150, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = false },
                new Plan { Id = 3, PlantId = 3, YieldId = 3, ExpertId = 3, PlanName = "Trồng bắp cải", Description = "Kế hoạch trồng bắp cải sạch", StartDate = new DateTime(2024, 3, 15), EndDate = new DateTime(2024, 6, 30), Status = "Ongoing", EstimatedProduct = 400, EstimatedUnit = "kg", AvailablePackagingQuantity = 180, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 4, PlantId = 4, YieldId = 4, ExpertId = 1, PlanName = "Trồng rau muống", Description = "Kế hoạch trồng rau muống ngắn ngày", StartDate = new DateTime(2024, 4, 5), EndDate = new DateTime(2024, 5, 5), Status = "Completed", EstimatedProduct = 200, EstimatedUnit = "kg", AvailablePackagingQuantity = 100, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 5, PlantId = 5, YieldId = 5, ExpertId = 2, PlanName = "Trồng cà rốt", Description = "Kế hoạch trồng cà rốt hữu cơ", StartDate = new DateTime(2024, 5, 1), EndDate = new DateTime(2024, 9, 1), Status = "Pending", EstimatedProduct = 350, EstimatedUnit = "kg", AvailablePackagingQuantity = 160, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = false },
                new Plan { Id = 6, PlantId = 6, YieldId = 6, ExpertId = 3, PlanName = "Trồng hành lá", Description = "Kế hoạch trồng hành lá sạch", StartDate = new DateTime(2024, 6, 10), EndDate = new DateTime(2024, 9, 30), Status = "Ongoing", EstimatedProduct = 250, EstimatedUnit = "kg", AvailablePackagingQuantity = 120, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true }
            );

            modelBuilder.Entity<FarmerPermission>().HasData(
                new FarmerPermission { FarmerId = 4, PlanId = 2, IsActive = true },
                new FarmerPermission { FarmerId = 2, PlanId = 1, IsActive = true },
                new FarmerPermission { FarmerId = 3, PlanId = 2, IsActive = true },
                new FarmerPermission { FarmerId = 1, PlanId = 3, IsActive = true },
                new FarmerPermission { FarmerId = 5, PlanId = 3, IsActive = true }
                );

            modelBuilder.Entity<CaringTask>().HasData(
                new CaringTask { Id = 1, PlanId = 1, ProblemId = 1, FarmerId = 1, TaskName = "Tưới nước cho cà chua", TaskType = "Watering", StartDate = new DateTime(2024, 1, 12), EndDate = new DateTime(2024, 1, 15), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 2, PlanId = 1, ProblemId = 2, FarmerId = 2, TaskName = "Bón phân hữu cơ cho cà chua", TaskType = "Fertilizing", StartDate = new DateTime(2024, 1, 18), EndDate = new DateTime(2024, 1, 20), IsCompleted = false, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 3, PlanId = 2, ProblemId = 3, FarmerId = 3, TaskName = "Kiểm tra sâu bệnh trên dưa lưới", TaskType = "Inspecting", StartDate = new DateTime(2024, 2, 10), EndDate = new DateTime(2024, 2, 12), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 4, PlanId = 2, ProblemId = 4, FarmerId = 4, TaskName = "Lắp hệ thống tưới tự động", TaskType = "Setup", StartDate = new DateTime(2024, 2, 15), EndDate = new DateTime(2024, 2, 18), IsCompleted = false, IsAvailable = true, Priority = 2, Status = "Ongoing", CreatedAt = DateTime.Now },
                new CaringTask { Id = 5, PlanId = 3, ProblemId = 5, FarmerId = 5, TaskName = "Nhổ cỏ dại quanh bắp cải", TaskType = "Weeding", StartDate = new DateTime(2024, 3, 20), EndDate = new DateTime(2024, 3, 22), IsCompleted = false, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 6, PlanId = 4, ProblemId = 6, FarmerId = 1, TaskName = "Phun thuốc phòng bệnh cho rau muống", TaskType = "Pesticide", StartDate = new DateTime(2024, 4, 7), EndDate = new DateTime(2024, 4, 10), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 7, PlanId = 4, ProblemId = 7, FarmerId = 2, TaskName = "Thu gom rác nông nghiệp", TaskType = "Cleaning", StartDate = new DateTime(2024, 4, 12), EndDate = new DateTime(2024, 4, 14), IsCompleted = true, IsAvailable = true, Priority = 3, Status = "Completed", CreatedAt = DateTime.Now },
                new CaringTask { Id = 8, PlanId = 5, ProblemId = 8, FarmerId = 3, TaskName = "Tưới nước cho cà rốt", TaskType = "Watering", StartDate = new DateTime(2024, 5, 5), EndDate = new DateTime(2024, 5, 7), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 9, PlanId = 6, ProblemId = 9, FarmerId = 4, TaskName = "Bón phân lá cho hành lá", TaskType = "Fertilizing", StartDate = new DateTime(2024, 6, 15), EndDate = new DateTime(2024, 6, 17), IsCompleted = false, IsAvailable = true, Priority = 2, Status = "Ongoing", CreatedAt = DateTime.Now },
                new CaringTask { Id = 10, PlanId = 1, ProblemId = 10, FarmerId = 5, TaskName = "Kiểm tra côn trùng gây hại trên mướp hương", TaskType = "Inspecting", StartDate = new DateTime(2024, 7, 12), EndDate = new DateTime(2024, 7, 15), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 11, PlanId = 2, ProblemId = 1, FarmerId = 1, TaskName = "Cắt tỉa cành ớt chuông", TaskType = "Pruning", StartDate = new DateTime(2024, 8, 20), EndDate = new DateTime(2024, 8, 22), IsCompleted = false, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 12, PlanId = 3, ProblemId = 2, FarmerId = 2, TaskName = "Tưới phun sương cho rau xà lách", TaskType = "Watering", StartDate = new DateTime(2024, 9, 10), EndDate = new DateTime(2024, 9, 12), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 13, PlanId = 4, ProblemId = 3, FarmerId = 3, TaskName = "Nhổ cỏ dại trong vườn xà lách", TaskType = "Weeding", StartDate = new DateTime(2024, 9, 15), EndDate = new DateTime(2024, 9, 17), IsCompleted = true, IsAvailable = true, Priority = 3, Status = "Completed", CreatedAt = DateTime.Now },
                new CaringTask { Id = 14, PlanId = 5, ProblemId = 4, FarmerId = 4, TaskName = "Bón phân NPK cho cải ngọt", TaskType = "Fertilizing", StartDate = new DateTime(2024, 10, 5), EndDate = new DateTime(2024, 10, 8), IsCompleted = false, IsAvailable = true, Priority = 1, Status = "Ongoing", CreatedAt = DateTime.Now },
                new CaringTask { Id = 15, PlanId = 6, ProblemId = 5, FarmerId = 5, TaskName = "Phun thuốc sinh học phòng bệnh cho cải ngọt", TaskType = "Pesticide", StartDate = new DateTime(2024, 10, 12), EndDate = new DateTime(2024, 10, 15), IsCompleted = false, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<HarvestingTask>().HasData(
                new HarvestingTask { Id = 1, PlanId = 1, FarmerId = 1, TaskName = "Thu hoạch rau cải", TaskType = "Thu hoạch", Description = "Thu hoạch rau cải trước khi trời quá nắng", HarvestDate = DateTime.Now.AddDays(-1), CompleteDate = DateTime.Now, HarvestedQuantity = 50, HarvestedUnit = "kg", IsCompleted = true, IsAvailable = true, Priority = 1, CreatedAt = DateTime.Now.AddDays(-5), Status = "Cancel" },
                new HarvestingTask { Id = 2, PlanId = 2, FarmerId = 2, TaskName = "Thu hoạch cà chua", TaskType = "Thu hoạch", Description = "Thu hoạch cà chua chín đỏ", HarvestDate = DateTime.Now.AddDays(-2), CompleteDate = DateTime.Now.AddDays(-1), HarvestedQuantity = 30, HarvestedUnit = "kg", IsCompleted = true, IsAvailable = true, Priority = 2, CreatedAt = DateTime.Now.AddDays(-6), Status = "Pending" },
                new HarvestingTask { Id = 3, PlanId = 3, FarmerId = 3, TaskName = "Thu hoạch bắp cải", TaskType = "Thu hoạch", Description = "Thu hoạch bắp cải vào sáng sớm để giữ độ tươi", HarvestDate = DateTime.Now.AddDays(-3), CompleteDate = DateTime.Now.AddDays(-2), HarvestedQuantity = 40, HarvestedUnit = "kg", IsCompleted = true, IsAvailable = true, Priority = 1, CreatedAt = DateTime.Now.AddDays(-7), Status = "Cancel" },
                new HarvestingTask { Id = 4, PlanId = 4, FarmerId = 4, TaskName = "Thu hoạch dưa leo", TaskType = "Thu hoạch", Description = "Thu hoạch dưa leo vào đúng thời điểm chín", HarvestDate = DateTime.Now.AddDays(-1), CompleteDate = DateTime.Now, HarvestedQuantity = 20, HarvestedUnit = "kg", IsCompleted = true, IsAvailable = true, Priority = 3, CreatedAt = DateTime.Now.AddDays(-4), Status = "Pending" },
                new HarvestingTask { Id = 5, PlanId = 5, FarmerId = 5, TaskName = "Thu hoạch bí đỏ", TaskType = "Thu hoạch", Description = "Thu hoạch bí đỏ khi vỏ cứng lại", HarvestDate = DateTime.Now.AddDays(-4), CompleteDate = DateTime.Now.AddDays(-3), HarvestedQuantity = 15, HarvestedUnit = "quả", IsCompleted = true, IsAvailable = true, Priority = 2, CreatedAt = DateTime.Now.AddDays(-8), Status = "Pending" }
            );

            modelBuilder.Entity<InspectingForm>().HasData(
                new InspectingForm
                {
                    Id = 1,
                    PlanId = 1,
                    InspectorId = 2,
                    TaskName = "Kiểm tra rau cải",
                    TaskType = "Kiểm tra chất lượng",
                    Description = "Đánh giá chất lượng rau cải trước khi thu hoạch",
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(-4),
                    ResultContent = "Rau cải đạt chuẩn",
                    BrixPoint = 5.2f,
                    Temperature = 25.5f,
                    Humidity = 70.2f,
                    Moisture = 60.1f,
                    ShellColor = "Xanh tươi",
                    TestGTKitColor = "Xanh",
                    InspectingQuantity = 100,
                    Unit = "kg",
                    IssuePercent = 2.5f,
                    CanHarvest = true,
                    CompletedDate = DateTime.Now.AddDays(-4),
                    Status = "Hoàn thành",
                    Priority = 1,
                    CreatedAt = DateTime.Now.AddDays(-6)
                },

                new InspectingForm
                {
                    Id = 2,
                    PlanId = 2,
                    InspectorId = 1,
                    TaskName = "Kiểm tra cà chua",
                    TaskType = "Kiểm tra độ chín",
                    Description = "Đánh giá màu sắc và chất lượng cà chua",
                    StartDate = DateTime.Now.AddDays(-6),
                    EndDate = DateTime.Now.AddDays(-5),
                    ResultContent = "Cà chua đạt độ chín",
                    BrixPoint = 6.5f,
                    Temperature = 26.0f,
                    Humidity = 68.5f,
                    Moisture = 55.3f,
                    ShellColor = "Đỏ đậm",
                    TestGTKitColor = "Đỏ",
                    InspectingQuantity = 80,
                    Unit = "kg",
                    IssuePercent = 3.0f,
                    CanHarvest = true,
                    CompletedDate = DateTime.Now.AddDays(-5),
                    Status = "Hoàn thành",
                    Priority = 2,
                    CreatedAt = DateTime.Now.AddDays(-7)
                },

                new InspectingForm
                {
                    Id = 3,
                    PlanId = 3,
                    InspectorId = 2,
                    TaskName = "Kiểm tra bắp cải",
                    TaskType = "Kiểm tra độ ẩm",
                    Description = "Kiểm tra độ ẩm và màu sắc bắp cải",
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now.AddDays(-6),
                    ResultContent = "Bắp cải hơi thiếu nước",
                    BrixPoint = 4.8f,
                    Temperature = 24.8f,
                    Humidity = 65.7f,
                    Moisture = 52.9f,
                    ShellColor = "Xanh nhạt",
                    TestGTKitColor = "Vàng",
                    InspectingQuantity = 120,
                    Unit = "kg",
                    IssuePercent = 5.0f,
                    CanHarvest = false,
                    CompletedDate = DateTime.Now.AddDays(-6),
                    Status = "Chờ xử lý",
                    Priority = 1,
                    CreatedAt = DateTime.Now.AddDays(-8)
                },

                new InspectingForm
                {
                    Id = 4,
                    PlanId = 4,
                    InspectorId = 1,
                    TaskName = "Kiểm tra dưa leo",
                    TaskType = "Kiểm tra độ chín",
                    Description = "Xác định độ chín và độ giòn của dưa leo",
                    StartDate = DateTime.Now.AddDays(-8),
                    EndDate = DateTime.Now.AddDays(-7),
                    ResultContent = "Dưa leo đạt chuẩn",
                    BrixPoint = 5.5f,
                    Temperature = 25.0f,
                    Humidity = 70.0f,
                    Moisture = 58.2f,
                    ShellColor = "Xanh đậm",
                    TestGTKitColor = "Xanh lục",
                    InspectingQuantity = 90,
                    Unit = "kg",
                    IssuePercent = 2.0f,
                    CanHarvest = true,
                    CompletedDate = DateTime.Now.AddDays(-7),
                    Status = "Hoàn thành",
                    Priority = 3,
                    CreatedAt = DateTime.Now.AddDays(-9)
                },

                new InspectingForm
                {
                    Id = 5,
                    PlanId = 5,
                    InspectorId = 2,
                    TaskName = "Kiểm tra bí đỏ",
                    TaskType = "Kiểm tra độ cứng",
                    Description = "Kiểm tra vỏ bí đỏ để xác định độ cứng",
                    StartDate = DateTime.Now.AddDays(-9),
                    EndDate = DateTime.Now.AddDays(-8),
                    ResultContent = "Vỏ bí đỏ chưa đủ cứng",
                    BrixPoint = 7f,
                    Temperature = 27.2f,
                    Humidity = 60.5f,
                    Moisture = 48.3f,
                    ShellColor = "Cam nhạt",
                    TestGTKitColor = "Vàng",
                    InspectingQuantity = 50,
                    Unit = "quả",
                    IssuePercent = 4.5f,
                    CanHarvest = false,
                    CompletedDate = DateTime.Now.AddDays(-8),
                    Status = "Cần theo dõi",
                    Priority = 2,
                    CreatedAt = DateTime.Now.AddDays(-10)
                },

                new InspectingForm
                {
                    Id = 6,
                    PlanId = 6,
                    InspectorId = 1,
                    TaskName = "Kiểm tra ớt chuông",
                    TaskType = "Kiểm tra độ ngọt",
                    Description = "Đánh giá độ ngọt và màu sắc của ớt chuông",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-9),
                    ResultContent = "Ớt chuông có độ ngọt tốt",
                    BrixPoint = 8.2f,
                    Temperature = 26.5f,
                    Humidity = 63.8f,
                    Moisture = 50.7f,
                    ShellColor = "Đỏ tươi",
                    TestGTKitColor = "Đỏ sậm",
                    InspectingQuantity = 70,
                    Unit = "kg",
                    IssuePercent = 1.5f,
                    CanHarvest = true,
                    CompletedDate = DateTime.Now.AddDays(-9),
                    Status = "Hoàn thành",
                    Priority = 1,
                    CreatedAt = DateTime.Now.AddDays(-11)
                }
            );

            modelBuilder.Entity<Problem>().HasData(
                new Problem { Id = 1, PlanId = 1, ProblemName = "Sâu bệnh trên lá", Description = "Xuất hiện sâu ăn lá trên cây.", Date = DateTime.Parse("2024-02-01"), ProblemType = "Pest", Status = "Pending", ResultContent = null },
                new Problem { Id = 2, PlanId = 1, ProblemName = "Thiếu nước", Description = "Đất khô, cây có dấu hiệu héo.", Date = DateTime.Parse("2024-02-05"), ProblemType = "Water", Status = "Resolved", ResultContent = "Đã tưới nước bổ sung." },
                new Problem { Id = 3, PlanId = 2, ProblemName = "Đất kém dinh dưỡng", Description = "Lá vàng, cây chậm phát triển.", Date = DateTime.Parse("2024-02-10"), ProblemType = "Soil", Status = "Pending", ResultContent = null },
                new Problem { Id = 4, PlanId = 3, ProblemName = "Cây bị nấm", Description = "Xuất hiện đốm trắng trên lá.", Date = DateTime.Parse("2024-02-12"), ProblemType = "Fungus", Status = "Resolved", ResultContent = "Đã phun thuốc chống nấm." },
                new Problem { Id = 5, PlanId = 3, ProblemName = "Thiếu ánh sáng", Description = "Cây phát triển yếu do ánh sáng yếu.", Date = DateTime.Parse("2024-02-15"), ProblemType = "Light", Status = "Pending", ResultContent = null },
                new Problem { Id = 6, PlanId = 4, ProblemName = "Sâu đục thân", Description = "Phát hiện dấu hiệu sâu đục thân cây.", Date = DateTime.Parse("2024-02-18"), ProblemType = "Pest", Status = "Resolved", ResultContent = "Đã xử lý bằng thuốc trừ sâu." },
                new Problem { Id = 7, PlanId = 4, ProblemName = "Mưa quá nhiều", Description = "Đất ẩm lâu, có nguy cơ úng rễ.", Date = DateTime.Parse("2024-02-20"), ProblemType = "Weather", Status = "Pending", ResultContent = null },
                new Problem { Id = 8, PlanId = 5, ProblemName = "Cây bị héo", Description = "Cây không đủ dinh dưỡng, lá rụng nhiều.", Date = DateTime.Parse("2024-02-22"), ProblemType = "Nutrients", Status = "Resolved", ResultContent = "Đã bổ sung phân bón." },
                new Problem { Id = 9, PlanId = 6, ProblemName = "Bọ trĩ tấn công", Description = "Bọ trĩ gây hại trên lá non.", Date = DateTime.Parse("2024-02-25"), ProblemType = "Pest", Status = "Pending", ResultContent = null },
                new Problem { Id = 10, PlanId = 6, ProblemName = "Nhiệt độ quá cao", Description = "Nắng nóng kéo dài gây stress cho cây.", Date = DateTime.Parse("2024-02-28"), ProblemType = "Weather", Status = "Resolved", ResultContent = "Đã che bóng giảm nhiệt độ." }
            );

            modelBuilder.Entity<Issue>().HasData(
                new Issue { Id = 1, ProblemId = 1, IssueName = "Xuất hiện sâu xanh", Description = "Sâu xanh ăn lá non gây hại cho cây.", IsActive = true },
                new Issue { Id = 2, ProblemId = 1, IssueName = "Lá bị đục lỗ", Description = "Sâu non cắn phá làm lá bị thủng lỗ.", IsActive = true },
                new Issue { Id = 3, ProblemId = 2, IssueName = "Đất khô nứt nẻ", Description = "Lượng nước cung cấp không đủ, đất có dấu hiệu nứt nẻ.", IsActive = false },
                new Issue { Id = 4, ProblemId = 2, IssueName = "Lá héo rũ", Description = "Thiếu nước khiến cây không thể duy trì độ tươi.", IsActive = true },
                new Issue { Id = 5, ProblemId = 3, IssueName = "Thiếu kali", Description = "Lá vàng úa từ mép, cây yếu.", IsActive = true },
                new Issue { Id = 6, ProblemId = 3, IssueName = "Thiếu nitơ", Description = "Lá nhạt màu, cây không phát triển mạnh.", IsActive = false },
                new Issue { Id = 7, ProblemId = 4, IssueName = "Xuất hiện nấm mốc", Description = "Lá có đốm trắng và phát triển chậm.", IsActive = true },
                new Issue { Id = 8, ProblemId = 4, IssueName = "Lá bị úa vàng", Description = "Nấm lây lan làm lá chuyển vàng và rụng.", IsActive = true },
                new Issue { Id = 9, ProblemId = 5, IssueName = "Cây phát triển chậm", Description = "Cây không nhận đủ ánh sáng dẫn đến còi cọc.", IsActive = true },
                new Issue { Id = 10, ProblemId = 6, IssueName = "Có lỗ nhỏ trên thân cây", Description = "Dấu hiệu sâu đục thân hoạt động.", IsActive = true },
                new Issue { Id = 11, ProblemId = 6, IssueName = "Cành bị khô héo", Description = "Sâu làm tổ khiến chất dinh dưỡng không lưu thông.", IsActive = false },
                new Issue { Id = 12, ProblemId = 7, IssueName = "Ngập úng rễ", Description = "Rễ cây bị úng nước dẫn đến chết dần.", IsActive = true },
                new Issue { Id = 13, ProblemId = 8, IssueName = "Thiếu vi lượng", Description = "Cây không hấp thụ đủ khoáng chất cần thiết.", IsActive = true },
                new Issue { Id = 14, ProblemId = 9, IssueName = "Xuất hiện đốm vàng trên lá", Description = "Bọ trĩ chích hút nhựa cây.", IsActive = true },
                new Issue { Id = 15, ProblemId = 9, IssueName = "Lá bị xoăn", Description = "Cây phản ứng với sự tấn công của bọ trĩ.", IsActive = false },
                new Issue { Id = 16, ProblemId = 10, IssueName = "Lá bị cháy nắng", Description = "Ánh nắng quá mạnh làm lá cây héo.", IsActive = true }
            );

            modelBuilder.Entity<SampleSolution>().HasData(
                new SampleSolution { Id = 1, IssueId = 1, FileUrl = "/solutions/sau-xanh.pdf", CreatedAt = DateTime.Parse("2024-02-02"), UpdatedAt = null },
                new SampleSolution { Id = 2, IssueId = 2, FileUrl = "/solutions/la-thung-lo.pdf", CreatedAt = DateTime.Parse("2024-02-03"), UpdatedAt = null },
                new SampleSolution { Id = 3, IssueId = 3, FileUrl = "/solutions/dat-kho.pdf", CreatedAt = DateTime.Parse("2024-02-06"), UpdatedAt = null },
                new SampleSolution { Id = 4, IssueId = 4, FileUrl = "/solutions/la-heo.pdf", CreatedAt = DateTime.Parse("2024-02-07"), UpdatedAt = DateTime.Parse("2024-02-08") },
                new SampleSolution { Id = 5, IssueId = 5, FileUrl = "/solutions/thieu-kali.pdf", CreatedAt = DateTime.Parse("2024-02-11"), UpdatedAt = null },
                new SampleSolution { Id = 6, IssueId = 6, FileUrl = "/solutions/thieu-nito.pdf", CreatedAt = DateTime.Parse("2024-02-12"), UpdatedAt = null },
                new SampleSolution { Id = 7, IssueId = 7, FileUrl = "/solutions/nam-moc.pdf", CreatedAt = DateTime.Parse("2024-02-13"), UpdatedAt = null },
                new SampleSolution { Id = 8, IssueId = 8, FileUrl = "/solutions/la-vang.pdf", CreatedAt = DateTime.Parse("2024-02-14"), UpdatedAt = DateTime.Parse("2024-02-15") },
                new SampleSolution { Id = 9, IssueId = 9, FileUrl = "/solutions/thieu-anh-sang.pdf", CreatedAt = DateTime.Parse("2024-02-16"), UpdatedAt = null },
                new SampleSolution { Id = 10, IssueId = 10, FileUrl = "/solutions/lo-tren-than.pdf", CreatedAt = DateTime.Parse("2024-02-19"), UpdatedAt = null },
                new SampleSolution { Id = 11, IssueId = 11, FileUrl = "/solutions/canh-kho.pdf", CreatedAt = DateTime.Parse("2024-02-20"), UpdatedAt = null },
                new SampleSolution { Id = 12, IssueId = 12, FileUrl = "/solutions/ngap-ung.pdf", CreatedAt = DateTime.Parse("2024-02-21"), UpdatedAt = null },
                new SampleSolution { Id = 13, IssueId = 13, FileUrl = "/solutions/thieu-vi-luong.pdf", CreatedAt = DateTime.Parse("2024-02-23"), UpdatedAt = null },
                new SampleSolution { Id = 14, IssueId = 14, FileUrl = "/solutions/dom-vang-bo-tri.pdf", CreatedAt = DateTime.Parse("2024-02-26"), UpdatedAt = null },
                new SampleSolution { Id = 15, IssueId = 15, FileUrl = "/solutions/la-xoan.pdf", CreatedAt = DateTime.Parse("2024-02-27"), UpdatedAt = null },
                new SampleSolution { Id = 16, IssueId = 16, FileUrl = "/solutions/la-chay-nang.pdf", CreatedAt = DateTime.Parse("2024-02-29"), UpdatedAt = null }
            );

            modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, YieldId = 1, Name = "Cảm biến nhiệt độ", Type = "Temperature Sensor", Location = "Khu A", Status = "Active", DeviceCode = "TEMP-001", InstallationDate = DateTime.Parse("2023-01-10"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 2, YieldId = 1, Name = "Cảm biến độ ẩm đất", Type = "Soil Moisture Sensor", Location = "Khu A", Status = "Active", DeviceCode = "MOIST-002", InstallationDate = DateTime.Parse("2023-02-15"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 3, YieldId = 2, Name = "Cảm biến ánh sáng", Type = "Light Sensor", Location = "Khu B", Status = "Active", DeviceCode = "LIGHT-003", InstallationDate = DateTime.Parse("2023-03-20"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 4, YieldId = 2, Name = "Cảm biến pH đất", Type = "Soil pH Sensor", Location = "Khu B", Status = "Inactive", DeviceCode = "PH-004", InstallationDate = DateTime.Parse("2023-04-05"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 5, YieldId = 3, Name = "Cảm biến độ ẩm không khí", Type = "Humidity Sensor", Location = "Khu C", Status = "Active", DeviceCode = "HUM-005", InstallationDate = DateTime.Parse("2023-05-12"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 6, YieldId = 3, Name = "Cảm biến CO2", Type = "CO2 Sensor", Location = "Khu C", Status = "Active", DeviceCode = "CO2-006", InstallationDate = DateTime.Parse("2023-06-18"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 7, YieldId = 4, Name = "Cảm biến độ mặn", Type = "Salinity Sensor", Location = "Khu D", Status = "Inactive", DeviceCode = "SALIN-007", InstallationDate = DateTime.Parse("2023-07-08"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 8, YieldId = 5, Name = "Cảm biến gió", Type = "Wind Sensor", Location = "Khu E", Status = "Active", DeviceCode = "WIND-008", InstallationDate = DateTime.Parse("2023-08-22"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 9, YieldId = 6, Name = "Cảm biến lượng mưa", Type = "Rain Gauge", Location = "Khu F", Status = "Active", DeviceCode = "RAIN-009", InstallationDate = DateTime.Parse("2023-09-15"), CreatedAt = DateTime.Now, CreatedBy = "Admin" },
                new Device { Id = 10, YieldId = 7, Name = "Cảm biến Nitrogen", Type = "Nitrogen Sensor", Location = "Khu G", Status = "Active", DeviceCode = "NITRO-010", InstallationDate = DateTime.Parse("2023-10-30"), CreatedAt = DateTime.Now, CreatedBy = "Admin" }
            );

            modelBuilder.Entity<Item>().HasData(
                // (Caring Task)
                new Item { Id = 1, Name = "Bình tưới cây", Description = "Bình tưới nước dung tích 5L", Image = "watering_can.jpg", Status = "Available", Type = "Caring" },
                new Item { Id = 2, Name = "Kéo cắt tỉa", Description = "Kéo chuyên dụng để cắt tỉa cành", Image = "pruning_shears.jpg", Status = "Available", Type = "Caring" },
                new Item { Id = 3, Name = "Bón phân hữu cơ", Description = "Dụng cụ bón phân dạng viên", Image = "fertilizer_spreader.jpg", Status = "Available", Type = "Caring" },
                new Item { Id = 4, Name = "Máy đo độ ẩm đất", Description = "Dụng cụ đo độ ẩm của đất", Image = "soil_moisture_meter.jpg", Status = "Available", Type = "Caring" },
                // (Harvesting Task)
                new Item { Id = 5, Name = "Dao thu hoạch", Description = "Dao chuyên dụng để cắt trái cây", Image = "harvesting_knife.jpg", Status = "Available", Type = "Harvesting" },
                new Item { Id = 6, Name = "Giỏ đựng nông sản", Description = "Giỏ chứa nông sản sau thu hoạch", Image = "harvest_basket.jpg", Status = "Available", Type = "Harvesting" },
                new Item { Id = 7, Name = "Máy cắt lúa", Description = "Máy gặt đập liên hợp mini", Image = "rice_harvester.jpg", Status = "In Use", Type = "Harvesting" },
                // (Inspecting Form)
                new Item { Id = 8, Name = "Kính lúp cầm tay", Description = "Dụng cụ kiểm tra sâu bệnh", Image = "magnifying_glass.jpg", Status = "Available", Type = "Inspecting" },
                new Item { Id = 9, Name = "Máy đo pH đất", Description = "Dụng cụ đo độ pH của đất", Image = "ph_meter.jpg", Status = "Available", Type = "Inspecting" },
                new Item { Id = 10, Name = "Bộ kit kiểm tra dư lượng thuốc trừ sâu", Description = "Dụng cụ kiểm định dư lượng hóa chất trên nông sản", Image = "pesticide_test_kit.jpg", Status = "Available", Type = "Inspecting" }
            );

            modelBuilder.Entity<CaringItem>().HasData(
                new CaringItem { Id = 1, ItemId = 1, TaskId = 1, Quantity = 2, Unit = "Cái" },
                new CaringItem { Id = 2, ItemId = 1, TaskId = 5, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 3, ItemId = 2, TaskId = 2, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 4, ItemId = 2, TaskId = 6, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 5, ItemId = 3, TaskId = 3, Quantity = 10, Unit = "Kg" },
                new CaringItem { Id = 6, ItemId = 3, TaskId = 7, Quantity = 15, Unit = "Kg" },
                new CaringItem { Id = 7, ItemId = 4, TaskId = 4, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 8, ItemId = 4, TaskId = 8, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 9, ItemId = 1, TaskId = 9, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 10, ItemId = 3, TaskId = 10, Quantity = 12, Unit = "Kg" },
                new CaringItem { Id = 11, ItemId = 2, TaskId = 11, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 12, ItemId = 4, TaskId = 12, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 13, ItemId = 3, TaskId = 13, Quantity = 8, Unit = "Kg" },
                new CaringItem { Id = 14, ItemId = 2, TaskId = 14, Quantity = 1, Unit = "Cái" },
                new CaringItem { Id = 15, ItemId = 1, TaskId = 15, Quantity = 2, Unit = "Cái" }
            );

            modelBuilder.Entity<HarvestingItem>().HasData(
                new HarvestingItem { Id = 1, ItemId = 5, TaskId = 1, Quantity = 2, Unit = "Cái" },
                new HarvestingItem { Id = 2, ItemId = 6, TaskId = 1, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 3, ItemId = 7, TaskId = 2, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 4, ItemId = 5, TaskId = 2, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 5, ItemId = 5, TaskId = 3, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 6, ItemId = 6, TaskId = 3, Quantity = 2, Unit = "Cái" },
                new HarvestingItem { Id = 7, ItemId = 7, TaskId = 4, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 8, ItemId = 6, TaskId = 4, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 9, ItemId = 6, TaskId = 5, Quantity = 1, Unit = "Cái" },
                new HarvestingItem { Id = 10, ItemId = 5, TaskId = 5, Quantity = 2, Unit = "Cái" }
            );

            modelBuilder.Entity<InspectingItem>().HasData(
                new InspectingItem { Id = 1, ItemId = 8, TaskId = 1, Quantity = 1, Unit = "Bộ" },
                new InspectingItem { Id = 2, ItemId = 9, TaskId = 1, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 3, ItemId = 10, TaskId = 2, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 4, ItemId = 8, TaskId = 2, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 5, ItemId = 9, TaskId = 3, Quantity = 1, Unit = "Bộ" },
                new InspectingItem { Id = 6, ItemId = 10, TaskId = 3, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 7, ItemId = 9, TaskId = 4, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 8, ItemId = 8, TaskId = 4, Quantity = 1, Unit = "Bộ" },
                new InspectingItem { Id = 9, ItemId = 10, TaskId = 5, Quantity = 1, Unit = "Bộ" },
                new InspectingItem { Id = 10, ItemId = 8, TaskId = 5, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 11, ItemId = 9, TaskId = 6, Quantity = 1, Unit = "Cái" },
                new InspectingItem { Id = 12, ItemId = 10, TaskId = 6, Quantity = 1, Unit = "Bộ" }
            );
        }
    }
}