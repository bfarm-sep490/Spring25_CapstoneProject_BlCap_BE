using Microsoft.EntityFrameworkCore;
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
                new Account { Id = 1, Name = "Eurofins Scientific", Email = "nguyenvana@gmail.com", Role = "Inspector", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 2, Name = "Trần Thị B", Email = "farmer@gmail.com", Role = "Farmer", Password = "1@", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 3, Name = "Lê Quang C", Email = "lequangc@gmail.com", Role = "Expert", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 4, Name = "Phạm Minh D", Email = "phaminhd@gmail.com", Role = "Farmer", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 5, Name = "Nguyễn Bình Phương Trâm", Email = "tramnbp@gmail.com", Role = "Farmer", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 6, Name = "Nguyễn Thiện F", Email = "nguyenthienf@gmail.com", Role = "Farmer", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 7, Name = "Trần Bích G", Email = "tranbichg@gmail.com", Role = "Farmer", Password = "1234", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 8, Name = "atvstp - TCCL VIETNAM", Email = "inspector@gmail.com", Role = "Inspector", Password = "1@", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 9, Name = "Phạm Tuan I", Email = "phamtuani@gmail.com", Role = "Expert", Password = "123", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 10, Name = "Hoàng Quỳnh J", Email = "expert@gmail.com", Role = "Expert", Password = "1@", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Account { Id = 11, Name = "Trịnh Xuân Admin", Email = "farmowner@gmail.com", Role = "Farm Owner", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 12, Name = "Trịnh Hữu Tuấn", Email = "retailer@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 13, Name = "Vũ Hoàng Duy Khánh", Email = "khanhvhd@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 14, Name = "Lê Quốc Khánh", Email = "khanhlq@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<Retailer>().HasData(
                new Retailer { Id = 1, AccountId = 12, LongxLat = "10.7769,106.7009", Address = "123 Đường Lê Lợi, Quận 1, TP.HCM", Phone = "0901234567", DOB = new DateTime(1985, 5, 10), Avatar = "https://nationaltoday.com/wp-content/uploads/2022/05/91-Retailer.jpg", Status = "Active" },
                new Retailer { Id = 2, AccountId = 13, LongxLat = "10.7627,106.6822", Address = "456 Đường Nguyễn Huệ, Quận 1, TP.HCM", Phone = "0912345678", DOB = new DateTime(1990, 8, 20), Avatar = "https://thumbs.dreamstime.com/b/hardware-store-worker-smiling-african-standing-fasteners-aisle-41251157.jpg", Status = "Inactive" },
                new Retailer { Id = 3, AccountId = 14, LongxLat = "10.8231,106.6297", Address = "789 Đường Phạm Văn Đồng, Quận Thủ Đức, TP.HCM", Phone = "0923456789", DOB = new DateTime(1995, 12, 15), Avatar = "https://www.kofastudy.com/kike_content/uploads/2021/01/e-Commerce-Today.jpg", Status = "Pending" }
            );

            modelBuilder.Entity<Expert>().HasData(
                new Expert { Id = 1, AccountId = 3, DOB = new DateTime(1985, 5, 20), Phone = "0912345678", Status = "Active", Avatar = "https://images.unsplash.com/photo-1531384441138-2736e62e0919?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Expert { Id = 2, AccountId = 9, DOB = new DateTime(1990, 8, 15), Phone = "0987654321", Status = "Inactive", Avatar = "https://images.unsplash.com/photo-1531901599143-df5010ab9438?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Expert { Id = 3, AccountId = 10, DOB = new DateTime(1995, 12, 5), Phone = "0971122334", Status = "Active", Avatar = "https://images.unsplash.com/photo-1531123897727-8f129e1688ce?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
            );

            modelBuilder.Entity<Farmer>().HasData(
                new Farmer { Id = 1, AccountId = 2, DOB = new DateTime(1980, 4, 15), Phone = "0901234567", Status = "Active", Avatar = "https://plus.unsplash.com/premium_photo-1686269460470-a44c06f16e0a?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 2, AccountId = 4, DOB = new DateTime(1985, 7, 10), Phone = "0912345678", Status = "Inactive", Avatar = "https://images.unsplash.com/photo-1589923188900-85dae523342b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 3, AccountId = 5, DOB = new DateTime(1990, 9, 25), Phone = "0923456789", Status = "Active", Avatar = "https://images.unsplash.com/photo-1593011951342-8426e949371f?q=80&w=1944&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 4, AccountId = 6, DOB = new DateTime(1995, 11, 30), Phone = "0934567890", Status = "Pending", Avatar = "https://images.unsplash.com/photo-1545830790-68595959c491?q=80&w=1935&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 5, AccountId = 7, DOB = new DateTime(2000, 1, 5), Phone = "0945678901", Status = "Active", Avatar = "https://plus.unsplash.com/premium_photo-1661411325413-98a5ff88e8e4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
            );

            modelBuilder.Entity<Inspector>().HasData(
                new Inspector
                {
                    Id = 1,
                    AccountId = 1,
                    Description = "Experienced agricultural inspector with 10 years in the field.",
                    Address = "123 Green Farm Road, Hanoi",
                    Status = "Active",
                    ImageUrl = "https://static.ybox.vn/2024/6/0/1719750238636-eurofins_1200x628.jpg",
                    IsAvailable = true
                },
                new Inspector
                {
                    Id = 2,
                    AccountId = 8,
                    Description = "Expert in organic certification and food safety.",
                    Address = "456 Eco Farm Lane, Ho Chi Minh City",
                    Status = "Inactive",
                    ImageUrl = "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/dataimages/202304/original/images2525186_35b.jpg",
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
                new Fertilizer { Id = 1, Name = "Phân Ure", Description = "Phân đạm Ure giúp cây phát triển xanh tốt", Image = "https://hoachathaiphong.vn/images/attachment/7194phan-bon-ure.jpg", Unit = "kg", AvailableQuantity = 1000, TotalQuantity = 5000, Status = "Available", Type = "Đạm" },
                new Fertilizer { Id = 2, Name = "Phân Kali", Description = "Phân Kali giúp cây tăng sức đề kháng", Image = "https://nongdanbinhthuan.com/uploads/nongdanbinhthuan/images/thuc_c/KALI-PHU-MY-MIENG1.jpg", Unit = "kg", AvailableQuantity = 800, TotalQuantity = 4000, Status = "Available", Type = "Kali" },
                new Fertilizer { Id = 3, Name = "Phân Lân", Description = "Phân Lân kích thích rễ phát triển", Image = "https://phanthuoctg.com/watermark/product/600x600x2/upload/product/fcaed761c2d877badab428d0d4c48a27-1580.jpeg", Unit = "kg", AvailableQuantity = 600, TotalQuantity = 3000, Status = "Available", Type = "Lân" },
                new Fertilizer { Id = 4, Name = "Phân Hữu Cơ", Description = "Phân hữu cơ giúp cải tạo đất", Image = "https://songgianh.com.vn/upload/attachment/9556hcvs.jpg", Unit = "kg", AvailableQuantity = 500, TotalQuantity = 2500, Status = "Available", Type = "Hữu cơ" },
                new Fertilizer { Id = 5, Name = "Phân Vi Sinh", Description = "Phân vi sinh chứa vi khuẩn có lợi", Image = "https://thuykimsinh.com/wp-content/uploads/2021/07/LAN-VI-SINH.jpg", Unit = "kg", AvailableQuantity = 700, TotalQuantity = 3500, Status = "Available", Type = "Vi sinh" }
            );

            modelBuilder.Entity<Pesticide>().HasData(
                new Pesticide { Id = 1, Name = "Thuốc Trừ Sâu Regent", Description = "Diệt sâu hại hiệu quả", Image = "https://sieuthinhanong.com/wp-content/uploads/2021/03/REGENT-0.3GR-1-768x768-1.jpg", Unit = "ml", AvailableQuantity = 500, TotalQuantity = 2000, Status = "Available", Type = "Trừ sâu" },
                new Pesticide { Id = 2, Name = "Thuốc Trừ Nấm Ridomil", Description = "Chống nấm bệnh cho cây trồng", Image = "https://product.hstatic.net/200000692099/product/ridomil-gold-68wg-1kg_8325d07877154f9c823954f0e608a3ae_1024x1024.jpg", Unit = "g", AvailableQuantity = 700, TotalQuantity = 3000, Status = "Available", Type = "Trừ nấm" },
                new Pesticide { Id = 3, Name = "Thuốc Diệt Cỏ Glyphosate", Description = "Diệt cỏ hiệu quả, an toàn", Image = "https://viethoanong.com/resources/images/Amiphosate.jpg", Unit = "l", AvailableQuantity = 400, TotalQuantity = 1500, Status = "Available", Type = "Diệt cỏ" },
                new Pesticide { Id = 4, Name = "Thuốc Trừ Rầy Confidor", Description = "Đặc trị rầy nâu, rệp sáp", Image = "https://sieuthinhanong.com/wp-content/uploads/2021/03/confidor-200SL-1-768x768-1-300x300.jpg", Unit = "ml", AvailableQuantity = 600, TotalQuantity = 2500, Status = "Available", Type = "Trừ rầy" },
                new Pesticide { Id = 5, Name = "Thuốc Trừ Bọ Xít Karate", Description = "Diệt bọ xít, nhện đỏ", Image = "https://product.hstatic.net/200000708159/product/thuoc-tru-sau-karate-2.5ec_5779570d639648adab740a923550ca42.png", Unit = "ml", AvailableQuantity = 800, TotalQuantity = 3500, Status = "Available", Type = "Trừ bọ xít" }
            );

            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, PlantId = 1, YieldId = 1, ExpertId = 1, PlanName = "Trồng cà chua vụ đông", Description = "Kế hoạch trồng cà chua vào mùa đông", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 4, 15), CompleteDate = new DateTime(2024, 4, 15), Status = "Complete", EstimatedProduct = 500, EstimatedUnit = "kg", CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 2, PlantId = 2, YieldId = 2, ExpertId = 2, PlanName = "Trồng dưa lưới", Description = "Kế hoạch trồng dưa lưới trong nhà kính", StartDate = new DateTime(2024, 2, 1), EndDate = new DateTime(2024, 6, 1), CompleteDate = new DateTime(2024, 6, 5), Status = "Complete", EstimatedProduct = 300, EstimatedUnit = "kg", CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = false },
                new Plan { Id = 3, PlantId = 3, YieldId = 3, ExpertId = 3, PlanName = "Trồng bắp cải", Description = "Kế hoạch trồng bắp cải sạch", StartDate = new DateTime(2024, 3, 15), EndDate = new DateTime(2024, 6, 30), CompleteDate = new DateTime(2024, 6, 30), Status = "Complete", EstimatedProduct = 400, EstimatedUnit = "kg", CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 4, PlantId = 4, YieldId = 4, ExpertId = 1, PlanName = "Trồng rau muống", Description = "Kế hoạch trồng rau muống ngắn ngày", StartDate = new DateTime(2024, 4, 5), EndDate = new DateTime(2024, 5, 5), CompleteDate = new DateTime(2024, 5, 5), Status = "Complete", EstimatedProduct = 200, EstimatedUnit = "kg", CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 5, PlantId = 5, YieldId = 5, ExpertId = 2, PlanName = "Trồng cà rốt", Description = "Kế hoạch trồng cà rốt hữu cơ", StartDate = new DateTime(2024, 5, 1), EndDate = new DateTime(2024, 9, 1), CompleteDate = new DateTime(2024, 9, 2), Status = "Complete", EstimatedProduct = 350, EstimatedUnit = "kg", CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = false },
                new Plan { Id = 6, PlantId = 6, YieldId = 6, ExpertId = 3, PlanName = "Trồng hành lá", Description = "Kế hoạch trồng hành lá sạch", StartDate = new DateTime(2024, 6, 10), EndDate = new DateTime(2024, 9, 30), CompleteDate = new DateTime(2024, 9, 30), Status = "Complete", EstimatedProduct = 250, EstimatedUnit = "kg", CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 7, PlantId = 10, YieldId = 2, ExpertId = 2, PlanName = "Mùa vụ trồng củ cải trắng", Description = "Bản kế hoạch chi tiết trồng củ cải trắng ngắn hạn trong vòng 30 ngày", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), Status = "Draft", EstimatedProduct = 500, EstimatedUnit = "kg", CreatedAt = DateTime.Now.AddDays(-10), CreatedBy = "Admin", IsApproved = false }
            );

            modelBuilder.Entity<FarmerPermission>().HasData(
                new FarmerPermission { FarmerId = 4, PlanId = 2, IsActive = true },
                new FarmerPermission { FarmerId = 1, PlanId = 2, IsActive = true },
                new FarmerPermission { FarmerId = 5, PlanId = 2, IsActive = true },
                new FarmerPermission { FarmerId = 2, PlanId = 1, IsActive = true },
                new FarmerPermission { FarmerId = 3, PlanId = 2, IsActive = true },
                new FarmerPermission { FarmerId = 1, PlanId = 1, IsActive = true },
                new FarmerPermission { FarmerId = 3, PlanId = 1, IsActive = true },
                new FarmerPermission { FarmerId = 5, PlanId = 1, IsActive = true }
            );

            modelBuilder.Entity<CaringTask>().HasData(
                new CaringTask { Id = 1, PlanId = 1, ProblemId = 1, FarmerId = 1, TaskName = "Tưới nước cho cà chua", TaskType = "Watering", StartDate = new DateTime(2025, 1, 12), EndDate = new DateTime(2025, 1, 15), CompleteDate = new DateTime(2025, 1, 20), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 2, PlanId = 1, ProblemId = 2, FarmerId = 2, TaskName = "Bón phân hữu cơ cho cà chua", TaskType = "Fertilizing", StartDate = new DateTime(2025, 1, 18), EndDate = new DateTime(2025, 1, 20), CompleteDate = new DateTime(2025, 1, 20), IsCompleted = true, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 3, PlanId = 2, ProblemId = 3, FarmerId = 3, TaskName = "Kiểm tra sâu bệnh trên dưa lưới", TaskType = "Inspecting", StartDate = new DateTime(2025, 2, 10), EndDate = new DateTime(2025, 2, 12), CompleteDate = new DateTime(2025, 2, 13), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 4, PlanId = 2, ProblemId = 4, FarmerId = 4, TaskName = "Lắp hệ thống tưới tự động", TaskType = "Setup", StartDate = new DateTime(2025, 2, 15), EndDate = new DateTime(2025, 2, 18), CompleteDate = new DateTime(2025, 2, 20), IsCompleted = true, IsAvailable = true, Priority = 2, Status = "Ongoing", CreatedAt = DateTime.Now },
                new CaringTask { Id = 5, PlanId = 3, ProblemId = 5, FarmerId = 5, TaskName = "Nhổ cỏ dại quanh bắp cải", TaskType = "Weeding", StartDate = new DateTime(2024, 3, 20), EndDate = new DateTime(2024, 3, 22), CompleteDate = new DateTime(2024, 3, 30), IsCompleted = true, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 6, PlanId = 4, ProblemId = 6, FarmerId = 1, TaskName = "Phun thuốc phòng bệnh cho rau muống", TaskType = "Pesticide", StartDate = new DateTime(2024, 4, 7), EndDate = new DateTime(2024, 4, 10), CompleteDate = new DateTime(2024, 4, 15), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 7, PlanId = 4, ProblemId = 7, FarmerId = 2, TaskName = "Thu gom rác nông nghiệp", TaskType = "Cleaning", StartDate = new DateTime(2024, 4, 12), EndDate = new DateTime(2024, 4, 14), CompleteDate = new DateTime(2024, 4, 20), IsCompleted = true, IsAvailable = true, Priority = 3, Status = "Completed", CreatedAt = DateTime.Now },
                new CaringTask { Id = 8, PlanId = 5, ProblemId = 8, FarmerId = 3, TaskName = "Tưới nước cho cà rốt", TaskType = "Watering", StartDate = new DateTime(2024, 5, 5), EndDate = new DateTime(2024, 5, 7), CompleteDate = new DateTime(2024, 5, 7), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 9, PlanId = 6, ProblemId = 9, FarmerId = 4, TaskName = "Bón phân lá cho hành lá", TaskType = "Fertilizing", StartDate = new DateTime(2024, 6, 15), EndDate = new DateTime(2024, 6, 17), CompleteDate = new DateTime(2024, 6, 20), IsCompleted = true, IsAvailable = true, Priority = 2, Status = "Ongoing", CreatedAt = DateTime.Now },
                new CaringTask { Id = 10, PlanId = 1, ProblemId = 10, FarmerId = 5, TaskName = "Kiểm tra côn trùng gây hại trên mướp hương", TaskType = "Inspecting", StartDate = new DateTime(2024, 7, 12), EndDate = new DateTime(2024, 7, 15), CompleteDate = new DateTime(2024, 7, 20), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 11, PlanId = 2, ProblemId = 1, FarmerId = 1, TaskName = "Cắt tỉa cành ớt chuông", TaskType = "Pruning", StartDate = new DateTime(2024, 8, 20), EndDate = new DateTime(2024, 8, 22), CompleteDate = new DateTime(2024, 8, 22), IsCompleted = true, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 12, PlanId = 3, ProblemId = 2, FarmerId = 2, TaskName = "Tưới phun sương cho rau xà lách", TaskType = "Watering", StartDate = new DateTime(2024, 9, 10), EndDate = new DateTime(2024, 9, 12), CompleteDate = new DateTime(2024, 9, 20), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Pending", CreatedAt = DateTime.Now },
                new CaringTask { Id = 13, PlanId = 4, ProblemId = 3, FarmerId = 3, TaskName = "Nhổ cỏ dại trong vườn xà lách", TaskType = "Weeding", StartDate = new DateTime(2024, 9, 15), EndDate = new DateTime(2024, 9, 17), CompleteDate = new DateTime(2024, 9, 20), IsCompleted = true, IsAvailable = true, Priority = 3, Status = "Completed", CreatedAt = DateTime.Now },
                new CaringTask { Id = 14, PlanId = 5, ProblemId = 4, FarmerId = 4, TaskName = "Bón phân NPK cho cải ngọt", TaskType = "Fertilizing", StartDate = new DateTime(2024, 10, 5), EndDate = new DateTime(2024, 10, 8), CompleteDate = new DateTime (2024, 10, 20), IsCompleted = true, IsAvailable = true, Priority = 1, Status = "Ongoing", CreatedAt = DateTime.Now },
                new CaringTask { Id = 15, PlanId = 6, ProblemId = 5, FarmerId = 5, TaskName = "Phun thuốc sinh học phòng bệnh cho cải ngọt", TaskType = "Pesticide", StartDate = new DateTime(2024, 10, 12), EndDate = new DateTime(2024, 10, 15), CompleteDate = new DateTime(2024, 10, 15), IsCompleted = true, IsAvailable = true, Priority = 2, Status = "Pending", CreatedAt = DateTime.Now }
            );

            modelBuilder.Entity<CaringPesticide>().HasData(
                new CaringPesticide { Id = 1, PesticideId = 1, TaskId = 6, Unit = "Lít", Quantity = 2.5f },
                new CaringPesticide { Id = 2, PesticideId = 2, TaskId = 15, Unit = "Lít", Quantity = 3.0f }
            );

            modelBuilder.Entity<CaringFertilizer>().HasData(
                new CaringFertilizer { Id = 1, FertilizerId = 1, TaskId = 2, Unit = "Kg", Quantity = 5.0f },
                new CaringFertilizer { Id = 2, FertilizerId = 2, TaskId = 9, Unit = "Kg", Quantity = 4.0f },
                new CaringFertilizer { Id = 3, FertilizerId = 3, TaskId = 14, Unit = "Kg", Quantity = 6.0f }
            );


            modelBuilder.Entity<CaringImage>().HasData(
                new CaringImage { Id = 1, TaskId = 1, Url = "https://thumb.photo-ac.com/33/331a66bc564e083cb1c81babfba42d41_t.jpeg" },
                new CaringImage { Id = 2, TaskId = 1, Url = "https://www.vigecam.vn/Portals/27059/10%20quanganh/cachtrongcachuasachvasaiquaquymonongtrai%201.jpg" },
                new CaringImage { Id = 3, TaskId = 2, Url = "https://danviet.ex-cdn.com/files/f1/2017/5/images/10c4864b-trang-trai-ca-chua-nhat-2.jpg" },
                new CaringImage { Id = 4, TaskId = 3, Url = "https://vaas.vn/sites/default/files/inline-images/z4410949507075_34eafecfa0fd04dc99cedfea94f519bb.jpg" },
                new CaringImage { Id = 5, TaskId = 4, Url = "https://danviet.ex-cdn.com/files/f1/296231569849192448/2022/5/13/edit-z3411936151630efaace430e503df8e6a548a064ff5839-1652436512592542646364-1652440184006468269746.jpeg" },
                new CaringImage { Id = 6, TaskId = 5, Url = "https://i.ex-cdn.com/nongnghiep.vn/files/bao_in/2020/08/11/hb-mh-trong-bap-cai-trai-vu-1123_20200811_966-135034.jpeg" },
                new CaringImage { Id = 7, TaskId = 6, Url = "https://danviet.mediacdn.vn/upload/3-2015/images/2015-07-16/1437018395-rau_muong_1211113-035347.jpg" },
                new CaringImage { Id = 8, TaskId = 7, Url = "https://baothainguyen.vn/file/oldimage/baothainguyen/UserFiles/image/tru-sau.jpg" },
                new CaringImage { Id = 9, TaskId = 7, Url = "https://media.quangninh.gov.vn/f5733364-2623-4af8-8267-09c9a345f144/Libraries/hinhanhbaiviet/%E1%BA%A3nh%20%C4%91%C4%83ng%20web/n%C4%83m%202021/cc%20tt-bvtv/bo%20xit/bo%20xit%20gay%20hai.jpg" },
                new CaringImage { Id = 10, TaskId = 7, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSfgHM1ONv6CYGP2H7afMf3Y34z7W6yIY_Erw&s" }
            );

            modelBuilder.Entity<HarvestingTask>().HasData(
                new HarvestingTask { Id = 1, PlanId = 2, FarmerId = 1, TaskName = "Thu hoạch rau cải", Description = "Thu hoạch rau cải trước khi trời quá nắng", ResultContent = "Đã hủy vì cây không đạt chất lượng kiểm định", StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(-3), CompleteDate = DateTime.Now, HarvestedQuantity = 50, HarvestedUnit = "kg", IsAvailable = true, Priority = 1, CreatedAt = DateTime.Now.AddDays(-5), Status = "Cancel" },
                new HarvestingTask { Id = 2, PlanId = 1, FarmerId = 2, TaskName = "Thu hoạch cà chua", Description = "Thu hoạch cà chua chín đỏ", StartDate = DateTime.Now.AddDays(-1).AddDays(-6), EndDate = DateTime.Now.AddDays(-4), CompleteDate = DateTime.Now.AddDays(-1), HarvestedQuantity = 30, HarvestedUnit = "kg", IsAvailable = true, Priority = 2, CreatedAt = DateTime.Now.AddDays(-1).AddDays(-6), Status = "Pending" },
                new HarvestingTask { Id = 3, PlanId = 3, FarmerId = 3, TaskName = "Thu hoạch bắp cải", Description = "Thu hoạch bắp cải vào sáng sớm để giữ độ tươi", ResultContent = "Đã hủy vì cây chết hết rồi", StartDate = DateTime.Now.AddDays(-10).AddDays(-3), EndDate = DateTime.Now.AddDays(-2), CompleteDate = DateTime.Now.AddDays(-2), HarvestedQuantity = 40, HarvestedUnit = "kg", IsAvailable = true, Priority = 3, CreatedAt = DateTime.Now.AddDays(-2).AddDays(-7), Status = "Cancel" },
                new HarvestingTask { Id = 4, PlanId = 4, FarmerId = 4, TaskName = "Thu hoạch dưa leo", Description = "Thu hoạch dưa leo vào đúng thời điểm chín", StartDate = DateTime.Now.AddDays(-9), EndDate = DateTime.Now.AddDays(-1), CompleteDate = DateTime.Now, HarvestedQuantity = 20, HarvestedUnit = "kg", IsAvailable = true, Priority = 3, CreatedAt = DateTime.Now.AddDays(-4), Status = "Pending" },
                new HarvestingTask { Id = 5, PlanId = 5, FarmerId = 5, TaskName = "Thu hoạch bí đỏ", Description = "Thu hoạch bí đỏ khi vỏ cứng lại", StartDate = DateTime.Now.AddDays(-9), EndDate = DateTime.Now.AddDays(-2), CompleteDate = DateTime.Now.AddDays(-1), HarvestedQuantity = 15, HarvestedUnit = "quả", IsAvailable = true, Priority = 2, CreatedAt = DateTime.Now.AddDays(-8), Status = "Pending" }
            );

            modelBuilder.Entity<HarvestingImage>().HasData(
                new HarvestingImage { Id = 1, TaskId = 1, Url = "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2022/1/4/991490/Thu-Hoach-Rau-Cai-Th.jpg" },
                new HarvestingImage { Id = 2, TaskId = 2, Url = "https://kholanhbachkhoa.net/wp-content/uploads/2024/05/vuon-ca-chua-da-lat-3.jpg" },
                new HarvestingImage { Id = 3, TaskId = 3, Url = "https://fatechme.com/uploads/bapcai12.jpg" },
                new HarvestingImage { Id = 4, TaskId = 4, Url = "https://danviet.mediacdn.vn/upload/3-2018/images/2018-07-02/Bi-quyet-trong-dua-leo-moi-vu-moi-trung-cua-nguoi-phu-nu-miet-vuon-anh-trong-2-1530512281-width680height490.jpg" },
                new HarvestingImage { Id = 5, TaskId = 5, Url = "https://tttt.ninhbinh.gov.vn/uploads/images/DAN%20TOC%20MN%202022/DTMN%202024/QUA%20BI%20DO.jpg" }
            );

            modelBuilder.Entity<InspectingForm>().HasData(
                new InspectingForm
                {
                    Id = 1,
                    PlanId = 1,
                    InspectorId = 2,
                    FormName = "Kiểm tra rau cải",
                    FormType = "Kiểm tra chất lượng",
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
                    Status = "Completed",
                    Priority = 1,
                    CreatedAt = DateTime.Now.AddDays(-6)
                },

                new InspectingForm
                {
                    Id = 2,
                    PlanId = 2,
                    InspectorId = 1,
                    FormName = "Kiểm tra cà chua",
                    FormType = "Kiểm tra độ chín",
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
                    Status = "Completed",
                    Priority = 2,
                    CreatedAt = DateTime.Now.AddDays(-7)
                },

                new InspectingForm
                {
                    Id = 3,
                    PlanId = 3,
                    InspectorId = 2,
                    FormName = "Kiểm tra bắp cải",
                    FormType = "Kiểm tra độ ẩm",
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
                    Status = "Pending",
                    Priority = 1,
                    CreatedAt = DateTime.Now.AddDays(-8)
                },

                new InspectingForm
                {
                    Id = 4,
                    PlanId = 4,
                    InspectorId = 1,
                    FormName = "Kiểm tra dưa leo",
                    FormType = "Kiểm tra độ chín",
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
                    Status = "Completed",
                    Priority = 3,
                    CreatedAt = DateTime.Now.AddDays(-9)
                },

                new InspectingForm
                {
                    Id = 5,
                    PlanId = 5,
                    InspectorId = 2,
                    FormName = "Kiểm tra bí đỏ",
                    FormType = "Kiểm tra độ cứng",
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
                    Status = "Completed",
                    Priority = 2,
                    CreatedAt = DateTime.Now.AddDays(-10)
                },

                new InspectingForm
                {
                    Id = 6,
                    PlanId = 6,
                    InspectorId = 1,
                    FormName = "Kiểm tra ớt chuông",
                    FormType = "Kiểm tra độ ngọt",
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
                    Status = "Completed",
                    Priority = 1,
                    CreatedAt = DateTime.Now.AddDays(-11)
                }
            );

            modelBuilder.Entity<PackagingTask>().HasData(
                new PackagingTask { Id = 1, FarmerId = 1, PlanId = 1, TaskName = "Đóng gói gạo", PackedUnit = "kg", ResultContent = "Đã đóng gói theo túi 5kg, thu được 1000 túi", PackedQuantity = 1000, Description = "Đóng gói gạo vào túi 5kg", StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(2), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-6), UpdatedAt = DateTime.Now, Priority = 1 },
                new PackagingTask { Id = 2, FarmerId = 2, PlanId = 2, TaskName = "Đóng gói cà phê", PackedUnit = "kg", ResultContent = "Đã đóng gói được 500 túi", PackedQuantity = 500, Description = "Đóng gói cà phê bột vào túi 1kg", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(1), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-4), UpdatedAt = DateTime.Now, Priority = 2 },
                new PackagingTask { Id = 3, FarmerId = 3, PlanId = 3, TaskName = "Đóng gói trà xanh", PackedUnit = "g", ResultContent = "Đã đóng gói thành công 20000 gói", PackedQuantity = 20000, Description = "Đóng gói trà xanh vào hộp 100g", StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(3), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-3), UpdatedAt = DateTime.Now, Priority = 3 },
                new PackagingTask { Id = 4, FarmerId = 4, PlanId = 4, TaskName = "Đóng gói hạt điều", PackedUnit = "kg", ResultContent = "Đã đóng gói thành công", PackedQuantity = 300, Description = "Đóng gói hạt điều vào túi 500g", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(4), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-1), UpdatedAt = DateTime.Now, Priority = 2 },
                new PackagingTask { Id = 5, FarmerId = 5, PlanId = 5, TaskName = "Đóng gói xoài sấy", PackedUnit = "g", ResultContent = "Đã đóng gói 10000 gói", PackedQuantity = 10000, Description = "Đóng gói xoài sấy vào túi 250g", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(5), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Priority = 1 }
            );

            modelBuilder.Entity<PackagingImage>().HasData(
                new PackagingImage { Id = 1, TaskId = 1, Url = "https://maydonggoi.com.vn/wp-content/uploads/2021/12/quy-trinh-dong-goi-gao-6.jpg" },
                new PackagingImage { Id = 2, TaskId = 2, Url = "https://maygoi.vn/wp-content/uploads/2019/03/maxresdefault-1.jpg" },
                new PackagingImage { Id = 3, TaskId = 3, Url = "https://thainguyencity.gov.vn/upload/news/2013/11/532/image/img5751.jpg" },
                new PackagingImage { Id = 4, TaskId = 4, Url = "https://jraifarm.com/files/upload/files/h%E1%BA%A1t%20%C4%91i%E1%BB%81u/Quy%20c%C3%A1ch%20%C4%91%C3%B3ng%20g%C3%B3i%20h%E1%BA%A1t%20%C4%91i%E1%BB%81u%20xu%E1%BA%A5t%20kh%E1%BA%A9u%204.png" },
                new PackagingImage { Id = 5, TaskId = 5, Url = "https://nanufoods.vn/wp-content/uploads/2020/05/Soft-dried-mango-in-carton-579x400.jpg" }
            );

            modelBuilder.Entity<Problem>().HasData(
                new Problem { Id = 1, PlanId = 1, IssueId = 1, ProblemName = "Sâu bệnh trên lá", Description = "Xuất hiện sâu ăn lá trên cây.", Date = DateTime.Parse("2024-02-01"), ProblemType = "Pest", Status = "Pending", ResultContent = null },
                new Problem { Id = 2, PlanId = 1, IssueId = 2, ProblemName = "Thiếu nước", Description = "Đất khô, cây có dấu hiệu héo.", Date = DateTime.Parse("2024-02-05"), ProblemType = "Water", Status = "Resolved", ResultContent = "Đã tưới nước bổ sung." },
                new Problem { Id = 3, PlanId = 2, IssueId = 3, ProblemName = "Đất kém dinh dưỡng", Description = "Lá vàng, cây chậm phát triển.", Date = DateTime.Parse("2024-02-10"), ProblemType = "Soil", Status = "Pending", ResultContent = null },
                new Problem { Id = 4, PlanId = 3, IssueId = 4, ProblemName = "Cây bị nấm", Description = "Xuất hiện đốm trắng trên lá.", Date = DateTime.Parse("2024-02-12"), ProblemType = "Fungus", Status = "Resolved", ResultContent = "Đã phun thuốc chống nấm." },
                new Problem { Id = 5, PlanId = 3, IssueId = 5, ProblemName = "Thiếu ánh sáng", Description = "Cây phát triển yếu do ánh sáng yếu.", Date = DateTime.Parse("2024-02-15"), ProblemType = "Light", Status = "Pending", ResultContent = null },
                new Problem { Id = 6, PlanId = 4, IssueId = 6, ProblemName = "Sâu đục thân", Description = "Phát hiện dấu hiệu sâu đục thân cây.", Date = DateTime.Parse("2024-02-18"), ProblemType = "Pest", Status = "Resolved", ResultContent = "Đã xử lý bằng thuốc trừ sâu." },
                new Problem { Id = 7, PlanId = 4, IssueId = 7, ProblemName = "Mưa quá nhiều", Description = "Đất ẩm lâu, có nguy cơ úng rễ.", Date = DateTime.Parse("2024-02-20"), ProblemType = "Weather", Status = "Pending", ResultContent = null },
                new Problem { Id = 8, PlanId = 5, IssueId = 8, ProblemName = "Cây bị héo", Description = "Cây không đủ dinh dưỡng, lá rụng nhiều.", Date = DateTime.Parse("2024-02-22"), ProblemType = "Nutrients", Status = "Resolved", ResultContent = "Đã bổ sung phân bón." },
                new Problem { Id = 9, PlanId = 6, IssueId = 9, ProblemName = "Bọ trĩ tấn công", Description = "Bọ trĩ gây hại trên lá non.", Date = DateTime.Parse("2024-02-25"), ProblemType = "Pest", Status = "Pending", ResultContent = null },
                new Problem { Id = 10, PlanId = 6, IssueId = 10, ProblemName = "Nhiệt độ quá cao", Description = "Nắng nóng kéo dài gây stress cho cây.", Date = DateTime.Parse("2024-02-28"), ProblemType = "Weather", Status = "Resolved", ResultContent = "Đã che bóng giảm nhiệt độ." }
            );

            modelBuilder.Entity<ProblemImage>().HasData(
                new ProblemImage { Id = 1, ProblemId = 1, Url = "https://vnmedia.vn/file/8a10a0d36ccebc89016ce0c6fa3e1b83/8a10a0d3761897b0017665518e9b6a91/072022/2.sau_to.sau_bap_cai_20220714112122.jpeg" },
                new ProblemImage { Id = 2, ProblemId = 1, Url = "https://lh6.googleusercontent.com/proxy/izOE5Anqbg4twC9njJa03WFsLuRu1J46zvsAGTocWFn5jcMDko9HXi_8TujVy5rDMKXI2NSfy4cot2z3H4-9PBwzBA" },
                new ProblemImage { Id = 3, ProblemId = 2, Url = "https://dongthanhcong.vn/wp-content/uploads/2024/07/dau-hieu-cua-cay-bi-thieu-nuoc-1200x720.jpg" },
                new ProblemImage { Id = 4, ProblemId = 2, Url = "https://thmh.vn/wp-content/uploads/2024/10/cay-bi-heo.png" },
                new ProblemImage { Id = 5, ProblemId = 3, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJnmU_pF5Xx89ht6XZItdpbtLKm6Xk_e_xaw&s" },
                new ProblemImage { Id = 6, ProblemId = 3, Url = "https://biovina.com.vn/wp-content/uploads/2016/05/thieudinhduong.jpg" },
                new ProblemImage { Id = 7, ProblemId = 4, Url = "https://bachnong.vn/upload/tin-tuc/phong-tri-benh-cay-trong/benh-nam-o-cay-trong_1.jpg" },
                new ProblemImage { Id = 8, ProblemId = 4, Url = "https://nhaluoiviet.vn/images/tin-tuc/trong-rau/cac-loai-nam-thuong-gap-o-cay-trong.jpg" },
                new ProblemImage { Id = 9, ProblemId = 5, Url = "https://camnangnhanong.wordpress.com/wp-content/uploads/2023/11/image-1.png?w=1024" },
                new ProblemImage { Id = 10, ProblemId = 5, Url = "https://bizweb.dktcdn.net/100/521/346/files/cay-thieu-anh-sang-co-bieu-hien-gi.jpg?v=1735714018430" },
                new ProblemImage { Id = 11, ProblemId = 6, Url = "https://bvtvthienbinh.com/files/upload/TIN-TUC/can-canh-sau-duc-than.jpg" },
                new ProblemImage { Id = 12, ProblemId = 6, Url = "https://kimnonggoldstar.vn/wp-content/uploads/2022/12/sau-duc-than-hai-sau-rieng-kimnonggoldstar-vn-1.jpg" },
                new ProblemImage { Id = 13, ProblemId = 7, Url = "https://file1.dangcongsan.vn/data/0/images/2022/09/16/vanphong/imager-8-64713-700.jpg" }
                );

            modelBuilder.Entity<Issue>().HasData(
                new Issue { Id = 1, IssueName = "Xuất hiện sâu xanh", Description = "Sâu xanh ăn lá non gây hại cho cây.", IsActive = true },
                new Issue { Id = 2, IssueName = "Lá bị đục lỗ", Description = "Sâu non cắn phá làm lá bị thủng lỗ.", IsActive = true },
                new Issue { Id = 3, IssueName = "Đất khô nứt nẻ", Description = "Lượng nước cung cấp không đủ, đất có dấu hiệu nứt nẻ.", IsActive = false },
                new Issue { Id = 4, IssueName = "Lá héo rũ", Description = "Thiếu nước khiến cây không thể duy trì độ tươi.", IsActive = true },
                new Issue { Id = 5, IssueName = "Thiếu kali", Description = "Lá vàng úa từ mép, cây yếu.", IsActive = true },
                new Issue { Id = 6, IssueName = "Thiếu nitơ", Description = "Lá nhạt màu, cây không phát triển mạnh.", IsActive = false },
                new Issue { Id = 7, IssueName = "Xuất hiện nấm mốc", Description = "Lá có đốm trắng và phát triển chậm.", IsActive = true },
                new Issue { Id = 8, IssueName = "Lá bị úa vàng", Description = "Nấm lây lan làm lá chuyển vàng và rụng.", IsActive = true },
                new Issue { Id = 9, IssueName = "Cây phát triển chậm", Description = "Cây không nhận đủ ánh sáng dẫn đến còi cọc.", IsActive = true },
                new Issue { Id = 10, IssueName = "Có lỗ nhỏ trên thân cây", Description = "Dấu hiệu sâu đục thân hoạt động.", IsActive = true },
                new Issue { Id = 11, IssueName = "Cành bị khô héo", Description = "Sâu làm tổ khiến chất dinh dưỡng không lưu thông.", IsActive = false },
                new Issue { Id = 12, IssueName = "Ngập úng rễ", Description = "Rễ cây bị úng nước dẫn đến chết dần.", IsActive = true },
                new Issue { Id = 13, IssueName = "Thiếu vi lượng", Description = "Cây không hấp thụ đủ khoáng chất cần thiết.", IsActive = true },
                new Issue { Id = 14, IssueName = "Xuất hiện đốm vàng trên lá", Description = "Bọ trĩ chích hút nhựa cây.", IsActive = true },
                new Issue { Id = 15, IssueName = "Lá bị xoăn", Description = "Cây phản ứng với sự tấn công của bọ trĩ.", IsActive = false },
                new Issue { Id = 16, IssueName = "Lá bị cháy nắng", Description = "Ánh nắng quá mạnh làm lá cây héo.", IsActive = true }
            );

            modelBuilder.Entity<SampleSolution>().HasData(
                new SampleSolution { Id = 1, IssueId = 1, FileUrl = "/solutions/sau-xanh.pdf", Description = "Có sâu xanh", TypeTask = "Nurturing" },
                new SampleSolution { Id = 2, IssueId = 2, FileUrl = "/solutions/la-thung-lo.pdf", Description = "Lá thủng lỗ", TypeTask = "Nurturing" },
                new SampleSolution { Id = 3, IssueId = 3, FileUrl = "/solutions/dat-kho.pdf", Description = "Đất khô", TypeTask = "Nurturing" },
                new SampleSolution { Id = 4, IssueId = 4, FileUrl = "/solutions/la-heo.pdf", Description = "Lá héo", TypeTask = "Nurturing" },
                new SampleSolution { Id = 5, IssueId = 5, FileUrl = "/solutions/thieu-kali.pdf", Description = "Thiếu Kali", TypeTask = "Fertilizing" },
                new SampleSolution { Id = 6, IssueId = 6, FileUrl = "/solutions/thieu-nito.pdf", Description = "Thiếu nito", TypeTask = "Fertilizing" },
                new SampleSolution { Id = 7, IssueId = 7, FileUrl = "/solutions/nam-moc.pdf", Description = "Nấm mốc", TypeTask = "Nurturing" },
                new SampleSolution { Id = 8, IssueId = 8, FileUrl = "/solutions/la-vang.pdf", Description = "Lá vàng", TypeTask = "Nurturing" },
                new SampleSolution { Id = 9, IssueId = 9, FileUrl = "/solutions/thieu-anh-sang.pdf", Description = "Thiếu ánh sáng", TypeTask = "Nurturing" },
                new SampleSolution { Id = 10, IssueId = 10, FileUrl = "/solutions/lo-tren-than.pdf", Description = "Lở trên thân", TypeTask = "Nurturing" },
                new SampleSolution { Id = 11, IssueId = 11, FileUrl = "/solutions/canh-kho.pdf", Description = "Cành khô", TypeTask = "Watering" },
                new SampleSolution { Id = 12, IssueId = 12, FileUrl = "/solutions/ngap-ung.pdf", Description = "Ngập úng", TypeTask = "Watering" },
                new SampleSolution { Id = 13, IssueId = 13, FileUrl = "/solutions/thieu-vi-luong.pdf", Description = "Thiếu vi lượng", TypeTask = "Nurturing" },
                new SampleSolution { Id = 14, IssueId = 14, FileUrl = "/solutions/dom-vang-bo-tri.pdf", Description = "Đốm vàng bố trí", TypeTask = "Nurturing" },
                new SampleSolution { Id = 15, IssueId = 15, FileUrl = "/solutions/la-xoan.pdf", Description = "Lá xoăn", TypeTask = "Nurturing" },
                new SampleSolution { Id = 16, IssueId = 16, FileUrl = "/solutions/la-chay-nang.pdf", Description = "Lá cháy nắng", TypeTask = "Planting" }
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
                new Device { Id = 10, YieldId = 7, Name = "Cảm biến Nitrogen", Type = "Nitrogen Sensor", Location = "Khu G", Status = "Error", DeviceCode = "NITRO-010", InstallationDate = DateTime.Parse("2023-10-30"), CreatedAt = DateTime.Now, CreatedBy = "Admin" }
            );

            modelBuilder.Entity<Item>().HasData(
                // (Caring Task)
                new Item { Id = 1, Name = "Bình tưới cây", Description = "Bình tưới nước dung tích 5L", Quantity = 100, Unit = "Cái", Image = "https://product.hstatic.net/200000199113/product/7220965_7709e84c2e3f40cf8111c44225c96646_large.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 2, Name = "Kéo cắt tỉa", Description = "Kéo chuyên dụng để cắt tỉa cành", Quantity = 100, Unit = "Cái", Image = "https://fact-depot.com/tmp/cache/images/_thumbs/720x720/media/product/30542/Keo-cat-tia-cong-vien-cay-xanh-HM044-cat-tia-co-la.png", Status = "Active", Type = "Caring" },
                new Item {Id = 3, Name = "Bón phân hữu cơ", Description = "Dụng cụ bón phân dạng viên", Quantity = 100, Unit = "Hộp", Image = "https://vn-live-01.slatic.net/p/eab87be47ffa092ca1becbc00ff06ed2.jpg", Status = "In-stock", Type = "Caring" },
                new Item {Id = 4, Name = "Máy đo độ ẩm đất", Description = "Dụng cụ đo độ ẩm của đất", Quantity = 100, Unit = "Cái", Image = "https://thbvn.com/cdn/images/may-do-do-am/dung-cu-do-do-am-dat-tot-1.jpg", Status = "Active", Type = "Caring" },
                // (Harvesting Task)
                new Item { Id = 5, Name = "Dao thu hoạch", Description = "Dao chuyên dụng để cắt trái cây", Quantity = 100, Unit = "Cái", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMlW2sl4uFDnr7wiHZo420jhTUDtPZmPQkqw&s", Status = "In-stock", Type = "Harvesting" },
                new Item {Id = 6, Name = "Giỏ đựng nông sản", Description = "Giỏ chứa nông sản sau thu hoạch", Quantity = 100, Unit = "Giỏ", Image = "https://ecohub.vn/wp-content/uploads/2021/08/thung-go-luu-tru-nong-san-do-dung-1.jpg", Status = "In-stock", Type = "Harvesting" },
                new Item { Id = 7, Name = "Máy cắt lúa", Description = "Máy gặt đập liên hợp mini", Quantity = 100, Unit = "Máy", Image = "https://mayxaydungmlk.vn/wp-content/uploads/2022/07/may-gat-DC60.jpg", Status = "In-stock", Type = "Harvesting" },
                // (Packaging Task)
                new Item { Id = 8, Name = "Máy đóng gói tự động", Unit = "Máy", Description = "Máy đóng gói tốc độ cao cho nông sản.", Image = "https://dienmayviteko.com/pic/Product/VPM-BZJ600-4_1029_HasThumb.webp", Quantity = 5, Status = "Active", Type = "Packaging" },
                new Item { Id = 9, Name = "Máy hút chân không", Unit = "Máy", Description = "Thiết bị bảo quản sản phẩm bằng cách hút chân không.", Image = "https://dbk.vn/uploads/ckfinder/images/may-hut-chan-khong/may-hut-chan-khong-cong-nghiep-Magic-Air-MZ600.jpg", Quantity = 8, Status = "Active", Type = "Packaging" },
                new Item { Id = 10, Name = "Cân điện tử đóng gói", Unit = "Máy", Description = "Cân chính xác dùng trong quy trình đóng gói.", Image = "https://cokhitanminh.com/may-dong-goi/wp-content/uploads/2021/11/may-dong-goi-can-dien-tu-3-bien-tmdg-2f14-ckmdg-1.jpg", Quantity = 10, Status = "Active", Type = "Packaging" },
                new Item { Id = 11, Name = "Máy đóng gói túi", Unit = "Máy", Description = "Máy đóng gói túi ni lông hoặc túi giấy cho sản phẩm nông nghiệp.", Image = "https://cnva.vn/wp-content/uploads/2024/02/may-dong-goi-tui-roi-tui-zip.jpg", Quantity = 7, Status = "In-stock", Type = "Packaging" },
                new Item { Id = 12, Name = "Dây chuyền đóng gói", Unit = "Dây", Description = "Dây chuyền đóng gói tự động hỗ trợ quy trình sản xuất.", Image = "https://mayduoctiendat.com/upload/filemanager/files/day-chuyen-dong-goi-bot-hop-thiec-tu-dong.jpg", Quantity = 2, Status = "Out-stock", Type = "Packaging" }
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

            modelBuilder.Entity<PackagingItem>().HasData(
                new PackagingItem { Id = 1, ItemId = 8, TaskId = 1, Quantity = 2, Unit = "machine" },
                new PackagingItem { Id = 2, ItemId = 9, TaskId = 2, Quantity = 4, Unit = "unit" },
                new PackagingItem { Id = 3, ItemId = 10, TaskId = 1, Quantity = 6, Unit = "unit" },
                new PackagingItem { Id = 4, ItemId = 11, TaskId = 4, Quantity = 3, Unit = "machine" },
                new PackagingItem { Id = 5, ItemId = 12, TaskId = 5, Quantity = 1, Unit = "line" }
            );
        }
    }
}