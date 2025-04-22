using Microsoft.EntityFrameworkCore;
using Spring25.BlCapstone.BE.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new Account { Id = 14, Name = "Lê Quốc Khánh", Email = "khanhlq@gmail.com", Role = "Retailer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now },
                new Account { Id = 15, Name = "Xoo Binf", Email = "binhbeopro1122@gmail.com", Role = "Farmer", Password = "1@", IsActive = true, CreatedAt = DateTime.Now}
            );

            modelBuilder.Entity<Retailer>().HasData(
                new Retailer { Id = 1, AccountId = 12, Longitude = 10.7769f, Latitude = 106.7009f, Address = "123 Đường Lê Lợi, Quận 1, TP.HCM", Phone = "0901234567", DOB = new DateTime(1985, 5, 10), Avatar = "https://nationaltoday.com/wp-content/uploads/2022/05/91-Retailer.jpg" },
                new Retailer { Id = 2, AccountId = 13, Longitude = 10.7627f, Latitude = 106.6822f, Address = "456 Đường Nguyễn Huệ, Quận 1, TP.HCM", Phone = "0912345678", DOB = new DateTime(1990, 8, 20), Avatar = "https://thumbs.dreamstime.com/b/hardware-store-worker-smiling-african-standing-fasteners-aisle-41251157.jpg" },
                new Retailer { Id = 3, AccountId = 14, Longitude = 10.8231f, Latitude = 106.6297f, Address = "789 Đường Phạm Văn Đồng, Quận Thủ Đức, TP.HCM", Phone = "0923456789", DOB = new DateTime(1995, 12, 15), Avatar = "https://www.kofastudy.com/kike_content/uploads/2021/01/e-Commerce-Today.jpg" }
            );

            modelBuilder.Entity<Expert>().HasData(
                new Expert { Id = 1, AccountId = 3, DOB = new DateTime(1985, 5, 20), Phone = "0912345678", Avatar = "https://images.unsplash.com/photo-1531384441138-2736e62e0919?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Expert { Id = 2, AccountId = 9, DOB = new DateTime(1990, 8, 15), Phone = "0987654321", Avatar = "https://images.unsplash.com/photo-1531901599143-df5010ab9438?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Expert { Id = 3, AccountId = 10, DOB = new DateTime(1995, 12, 5), Phone = "0971122334", Avatar = "https://images.unsplash.com/photo-1531123897727-8f129e1688ce?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" }
            );

            modelBuilder.Entity<Farmer>().HasData(
                new Farmer { Id = 1, AccountId = 2, DOB = new DateTime(1980, 4, 15), Phone = "0901234567", Avatar = "https://plus.unsplash.com/premium_photo-1686269460470-a44c06f16e0a?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 2, AccountId = 4, DOB = new DateTime(1985, 7, 10), Phone = "0912345678", Avatar = "https://images.unsplash.com/photo-1589923188900-85dae523342b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 3, AccountId = 5, DOB = new DateTime(1990, 9, 25), Phone = "0923456789", Avatar = "https://images.unsplash.com/photo-1593011951342-8426e949371f?q=80&w=1944&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 4, AccountId = 6, DOB = new DateTime(1995, 11, 30), Phone = "0934567890", Avatar = "https://images.unsplash.com/photo-1545830790-68595959c491?q=80&w=1935&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 5, AccountId = 7, DOB = new DateTime(2000, 1, 5), Phone = "0945678901", Avatar = "https://plus.unsplash.com/premium_photo-1661411325413-98a5ff88e8e4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" },
                new Farmer { Id = 6, AccountId = 15, DOB = new DateTime(2003, 5, 21), Phone = "0838097512", Avatar = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRGdR1EiC3BMaU8EUeRTp7Vo8oqhfLkySpTsw&s" }
            );

            modelBuilder.Entity<FarmerPerformance>().HasData(
                new FarmerPerformance
                {
                    Id = 1,
                    CompletedTasks = 20,
                    IncompleteTasks = 2,
                    PerformanceScore = 90.5
                },
                new FarmerPerformance
                {
                    Id = 2,
                    CompletedTasks = 15,
                    IncompleteTasks = 5,
                    PerformanceScore = 75.0
                },
                new FarmerPerformance
                {
                    Id = 3,
                    CompletedTasks = 10,
                    IncompleteTasks = 10,
                    PerformanceScore = 50.0
                },
                new FarmerPerformance
                {
                    Id = 4,
                    CompletedTasks = 25,
                    IncompleteTasks = 1,
                    PerformanceScore = 96.0
                },
                new FarmerPerformance
                {
                    Id = 5,
                    CompletedTasks = 12,
                    IncompleteTasks = 3,
                    PerformanceScore = 80.0
                },
                new FarmerPerformance
                {
                    Id = 6,
                    CompletedTasks = 8,
                    IncompleteTasks = 7,
                    PerformanceScore = 53.3
                }
            );


            modelBuilder.Entity<Inspector>().HasData(
                new Inspector
                {
                    Id = 1,
                    AccountId = 1,
                    Description = "Experienced agricultural inspector with 10 years in the field.",
                    Address = "123 Green Farm Road, Hanoi",
                    Hotline = "0123456789",
                    ImageUrl = "https://static.ybox.vn/2024/6/0/1719750238636-eurofins_1200x628.jpg",
                },
                new Inspector
                {
                    Id = 2,
                    AccountId = 8,
                    Description = "Expert in organic certification and food safety.",
                    Address = "456 Eco Farm Lane, Ho Chi Minh City",
                    Hotline = "0987654321",
                    ImageUrl = "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/dataimages/202304/original/images2525186_35b.jpg",
                }
            );

            modelBuilder.Entity<Plant>().HasData(
                new Plant
                {
                    Id = 1,
                    PlantName = "Rau muống",
                    Description = "Loại rau dễ trồng, phát triển nhanh, giàu dinh dưỡng.",
                    Quantity = 100,
                    Status = "Available",
                    BasePrice = 15000,
                    Type = "Rau ăn lá",
                    ImageUrl = "https://thanhnien.mediacdn.vn/Uploaded/camlt/2022_09_08/anh-chup-man-hinh-2022-09-08-luc-155118-6476.png",
                    DeltaOne = 1.2f,
                    DeltaTwo = 0.8f,
                    DeltaThree = 1.1f,
                    PreservationDay = 5
                },
                new Plant
                {
                    Id = 2,
                    PlantName = "Cải ngọt",
                    Description = "Loại rau cải phổ biến, dễ trồng, thu hoạch nhanh.",
                    Quantity = 80,
                    Status = "Available",
                    BasePrice = 18000,
                    Type = "Rau ăn lá",
                    ImageUrl = "https://fresco.vn/public/upload/product/cai-ngot-thuy-canh-hsxNzHwGZn.jpg",
                    DeltaOne = 1.3f,
                    DeltaTwo = 0.7f,
                    DeltaThree = 1.0f,
                    PreservationDay = 7
                },
                new Plant
                {
                    Id = 3,
                    PlantName = "Xà lách xoăn",
                    Description = "Rau ăn sống, dễ trồng, nhanh thu hoạch.",
                    Quantity = 90,
                    Status = "Available",
                    BasePrice = 20000,
                    Type = "Rau ăn lá",
                    ImageUrl = "https://www.cleanipedia.com/images/5iwkm8ckyw6v/6fcJ3CHTOLA35rCtQEQixF/fb1c276fe0c96d6922838248949b96a4/eGEtbGFjaC5qcGVn/1200w/rau-di%E1%BA%BFp-c%C3%A1-%C4%91%E1%BA%B7t-trong-gi%E1%BB%8F-m%C3%A2y%2C-n%E1%BB%81n-tr%E1%BA%AFng..jpg",
                    DeltaOne = 1.5f,
                    DeltaTwo = 0.9f,
                    DeltaThree = 1.2f,
                    PreservationDay = 6
                },
                new Plant
                {
                    Id = 4,
                    PlantName = "Hành lá",
                    Description = "Gia vị phổ biến, dễ trồng, thu hoạch nhanh.",
                    Quantity = 70,
                    Status = "Available",
                    BasePrice = 25000,
                    Type = "Hành",
                    ImageUrl = "https://www.cleanipedia.com/images/5iwkm8ckyw6v/6fcJ3CHTOLA35rCtQEQixF/fb1c276fe0c96d6922838248949b96a4/eGEtbGFjaC5qcGVn/1200w/rau-di%E1%BA%BFp-c%C3%A1-%C4%91%E1%BA%B7t-trong-gi%E1%BB%8F-m%C3%A2y%2C-n%E1%BB%81n-tr%E1%BA%AFng..jpg",
                    DeltaOne = 1.4f,
                    DeltaTwo = 0.9f,
                    DeltaThree = 1.3f,
                    PreservationDay = 10
                },
                new Plant
                {
                    Id = 5,
                    PlantName = "Mồng tơi",
                    Description = "Rau leo, phát triển nhanh, thích hợp trồng mùa hè.",
                    Quantity = 85,
                    Status = "Available",
                    BasePrice = 16000,
                    Type = "Rau ăn lá",
                    ImageUrl = "https://hatgiongphuongnam.com/asset/upload/image/hat-giong-rau-mong-toi-1.8_.png?v=20190410",
                    DeltaOne = 1.3f,
                    DeltaTwo = 0.8f,
                    DeltaThree = 1.1f,
                    PreservationDay = 5
                },
                new Plant
                {
                    Id = 6,
                    PlantName = "Cải bó xôi",
                    Description = "Rau giàu dinh dưỡng, tốt cho sức khỏe.",
                    Quantity = 75,
                    Status = "Available",
                    BasePrice = 22000,
                    Type = "Rau ăn lá",
                    ImageUrl = "https://product.hstatic.net/200000423303/product/cai-bo-xoi-huu-co_dcef0c0e1fc1491599583cc06a19b830.jpg",
                    DeltaOne = 1.6f,
                    DeltaTwo = 1.0f,
                    DeltaThree = 1.2f,
                    PreservationDay = 6
                },
                new Plant
                {
                    Id = 7,
                    PlantName = "Củ cải trắng",
                    Description = "Loại củ phát triển nhanh, giàu dinh dưỡng.",
                    Quantity = 60,
                    Status = "Available",
                    BasePrice = 20000,
                    Type = "Rau củ quả",
                    ImageUrl = "https://dalafood.vn/wp-content/uploads/2022/06/cu-cai-trang.jpg",
                    DeltaOne = 1.4f,
                    DeltaTwo = 0.9f,
                    DeltaThree = 1.3f,
                    PreservationDay = 12
                },
                new Plant
                {
                    Id = 8,
                    PlantName = "Đậu bắp",
                    Description = "Rau quả dễ trồng, thu hoạch nhanh.",
                    Quantity = 65,
                    Status = "Available",
                    BasePrice = 19000,
                    Type = "Rau củ quả",
                    ImageUrl = "https://bizweb.dktcdn.net/100/390/808/products/dau-bap-huu-co-500x500.jpg?v=1600504946570",
                    DeltaOne = 1.2f,
                    DeltaTwo = 0.8f,
                    DeltaThree = 1.0f,
                    PreservationDay = 8
                },
                new Plant
                {
                    Id = 9,
                    PlantName = "Dưa leo",
                    Description = "Rau quả dễ trồng, thu hoạch nhanh, giàu nước.",
                    Quantity = 78,
                    Status = "Available",
                    BasePrice = 18000,
                    Type = "Rau củ quả",
                    ImageUrl = "https://hoayeuthuong.com/hinh-hoa-tuoi/moingay/11896_dua-leo-lon-kg.jpg",
                    DeltaOne = 1.5f,
                    DeltaTwo = 0.9f,
                    DeltaThree = 1.2f,
                    PreservationDay = 7
                },
                new Plant
                {
                    Id = 10,
                    PlantName = "Cà chua",
                    Description = "Loại quả nhiều vitamin, dễ trồng, nhanh thu hoạch.",
                    Quantity = 72,
                    Status = "Available",
                    BasePrice = 25000,
                    Type = "Rau củ quả",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSQHrOrbNd1JxfpvcHVUqe5bklaBHYxon-Qew&s",
                    DeltaOne = 1.6f,
                    DeltaTwo = 1.0f,
                    DeltaThree = 1.3f,
                    PreservationDay = 10
                }
            );

            modelBuilder.Entity<Yield>().HasData(
                new Yield { Id = 1, YieldName = "Trang trại A", AreaUnit = "m2", Area = 500, Description = "Đất hữu cơ màu mỡ", Type = "Luân canh", Status = "Available" },
                new Yield { Id = 2, YieldName = "Nông trại B", AreaUnit = "m2", Area = 300, Description = "Đất chua cần cải tạo", Type = "Thâm canh", Status = "Maintenance" },
                new Yield { Id = 3, YieldName = "Ruộng C", AreaUnit = "m2", Area = 800, Description = "Đất phèn nhẹ, thích hợp trồng lúa", Type = "Luân canh", Status = "Available" },
                new Yield { Id = 4, YieldName = "Nông trại D", AreaUnit = "m2", Area = 450, Description = "Đất đen màu mỡ", Type = "Thâm canh", Status = "Available" },
                new Yield { Id = 5, YieldName = "Trang trại E", AreaUnit = "m2", Area = 600, Description = "Đất xám, thoát nước tốt", Type = "Luân canh", Status = "Available" },
                new Yield { Id = 6, YieldName = "Khu vực F", AreaUnit = "m2", Area = 350, Description = "Đất cát pha, cần giữ ẩm tốt", Type = "Thâm canh", Status = "In-Use" },
                new Yield { Id = 7, YieldName = "Vườn G", AreaUnit = "m2", Area = 400, Description = "Đất đỏ bazan giàu dinh dưỡng", Type = "Thâm canh", Status = "Available" }
            );

            modelBuilder.Entity<Fertilizer>().HasData(
                new Fertilizer { Id = 1, Name = "Phân Hữu Cơ", Type = "Organic", Description = "Apply 100g per square meter every 2 weeks. Suitable for vegetables.", Quantity = 50, Unit = "kg", Image = "https://happyagri.com.vn/storage/jf/u8/jfu8y6304pvhe0zuxf3o3dwegwna_phan-bon-la-abaxton-hieu-ocenum-organic-plus.webp", Status = "Available" },
                new Fertilizer { Id = 2, Name = "Phân NPK 16-16-8", Type = "Chemical", Description = "Use 50g per plant every month. Mix with water before applying.", Quantity = 100, Unit = "kg", Image = "https://jvf.com.vn/vnt_upload/product/05_2019/Hinhbaobi/g1a_mat_truoc.jpg", Status = "Available" },
                new Fertilizer { Id = 3, Name = "Phân bón Humic", Type = "Organic", Description = "Dissolve 5ml in 1 liter of water. Spray on leaves weekly.", Quantity = 30, Unit = "liters", Image = "https://nongnghiephoangphuc.com/thumbs/1600x1600x2/upload/product/hm-99-moi-2058.png", Status = "Out of Stock" },
                new Fertilizer { Id = 4, Name = "Phân Lân Vô Cơ", Type = "Mineral", Description = "Apply 20g per square meter before planting. Improves root growth.", Quantity = 80, Unit = "kg", Image = "https://cdn.mos.cms.futurecdn.net/rRaQV8Td8U78mUXoeaA2j7.jpg", Status = "Available" },
                new Fertilizer { Id = 5, Name = "Potassium Sulfate", Type = "Chemical", Description = "Use 30g per tree during flowering season. Helps in fruit development.", Quantity = 60, Unit = "kg", Image = "https://hoachatthinghiem.org/wp-content/uploads/2022/10/Potassium-Sulphate-DUKSAN.jpg", Status = "Limited Stock" },
                new Fertilizer { Id = 8, Name = "Phân Đạm Urea – Urê - Urea N 46%", Type = "Chemical", Description = "Phân đạm Urea (hay còn có có tên gọi thân thuộc khác là Phân Urê) là loại phân đạm dạng hữu có có tỷ lệ N cao nhất. Đây là hợp chất hữu cơ của nito, cacbon, oxy,hidro.\r\n\r\nTrong đạm Urea thường có chứa 44-48% nguyên tố N. Với hàm lượng N cao cùng với nhiều lợi ích khác, phân Urê rất được bà con yêu thích và sử dụng trong nông nghiệp. Đây loại phân bón chiếm 59% trên tổng số các loại phân đạm được sản xuất ở các nước trên thế giới.\r\n\r\nPhân URE thích hợp trên rất nhiều loại đất với nhiều loại cây trồng khác nhau, có hiệu quả kinh tế cao trong nông nghiệp.", Quantity = 60, Unit = "kg", Image = "https://vntradimex.com/public/files/product/phan-dam-urea-cao-cap-61e57d5cec5d5.jpg", Status = "Available" },
                new Fertilizer { Id = 6, Name = "Phân bò Hữu cơ hoai mục", Type = "Organic", Description = "Nên sử dụng phân bò ủ hoai mục bón cho cây trồng để tránh sâu bệnh gây hại. Tốt nhất nên trộn phân bò với các giá thể khác như tro trấu, đất sạch để tăng độ ẩm, giúp đất tơi xốp thoáng khí và cho cây phát triển nhanh hơn.\r\nPhân bò thích hợp để bón lót cho cây. Nên chọn phân bò khô để tránh tình trạng phân đóng sền sệt gây ra bệnh thối rễ cây.\r\nCho một lượng phân bò vừa đủ xung quanh gốc cây sau đó lấp một lớp đất khoảng mỏng lên, tưới nước đủ ẩm.\r\nPhân bò bón rất tốt cho rất nhiều loại cây trồng.", Quantity = 100, Unit = "kg", Image = "https://ketrongrau.com/wp-content/uploads/2013/08/phan_bo_hoai_muc-3.jpg", Status = "Available" },
                new Fertilizer { Id = 7, Name = "Vôi bột", Type = "Chemical", Description = "Khi sử dụng loại vôi bột này quý vị phải hết sức lưu ý bởi nó rất dễ gây ra hiện tượng nguy hiểm như sau:\r\n\r\nĐối với cây trồng: Gây ra hiện tượng cháy lá và rễ cây\r\nCó thể gây bỏng da tay nếu tiếp xúc trực tiếp không có dụng cụ bảo hộ đúng chuẩn.\r\nNhiệt nóng trong quá trình phản ứng với nước có thể gây cháy hỏng dụng cụ chứa bằng nhựa\r\n", Quantity = 100, Unit = "kg", Image = "https://ghgroup.com.vn/storage/0q/ag/0qagxc6uaer3ee0dcjvakeaf1uyi_gia-voi-bot-4.webp", Status = "Available" },
                new Fertilizer { Id = 9, Name = "Phân Trùn Quế", Type = "Organic", Description = "Phân trùn quế mang lại nhiều lợi ích cho cây trồng, đặc biệt là cho những người trồng rau và cây cảnh tại đô thị. Một trong những lợi ích quan trọng nhất của phân trùn quế là cung cấp dinh dưỡng cho cây trồng một cách cân bằng và dễ hấp thu. Phân trùn quế có chứa nhiều loại vi lượng và đa lượng như đạm, lân, kali, canxi, magie, sắt… giúp cây trồng phát triển khỏe mạnh và tăng năng suất. Phân trùn quế cũng có chứa các vi sinh vật có lợi như vi khuẩn, nấm, vi rút… giúp cải thiện độ phì nhiêu của đất và hỗ trợ quá trình hấp thu dinh dưỡng của cây. Ngoài ra phân trùn quế còn có các lợi ích khác như: \r\n\r\nGiữ ẩm cho đất và cây: Phân trùn quế có dạng hạt nhỏ và mềm, có khả năng giữ nước cao. Khi bón phân trùn quế cho đất, bạn sẽ giảm thiểu sự bay hơi của nước và duy trì độ ẩm cho đất và cây. Điều này rất hữu ích cho những người trồng rau và cây cảnh tại đô thị, vì họ không cần tưới nước thường xuyên và tiết kiệm chi phí.\r\n\r\nTăng sức đề kháng cho cây trồng: Phân trùn quế có chứa các chất kháng sinh tự nhiên, giúp ngăn ngừa và điều trị các bệnh và sâu bọ gây hại cho cây. Phân trùn quế cũng có chứa các chất kích thích sinh học, giúp tăng cường hệ miễn dịch của cây và giảm stress khi gặp điều kiện bất lợi.\r\n\r\nKích thích sinh trưởng của cây trồng: Phân trùn quế có chứa các hormon sinh học như auxin, cytokinin, gibberellin… giúp thúc đẩy sự phát triển của rễ, lá, hoa và quả của cây. Phân trùn quế cũng giúp cây ra hoa và kết trái nhiều hơn, tăng chất lượng và hương vị của rau và hoa quả.", Quantity = 100, Unit = "kg", Image = "https://product.hstatic.net/1000269461/product/_ko_logo_-_1000x1000-phan_trun_que__da_qua_xu_ly__sfarm_pb01_-_bao_2kg_4d74fe8899c548f6b10cc76e224f97fc.jpg", Status = "Available" },
                new Fertilizer { Id = 10, Name = "Phân Hữu cơ Vi sinh", Type = "Organic", Description = "Sử dụng phân bón hữu cơ vi sinh mang lại rất nhiều lợi ích trong nông nghiệp  và đây còn được coi là loại phân bón được khuyến khích người dân sử dụng nhất hiện nay. Dưới đây là những lợi ích từ việc bón phân hữu cơ.\r\n\r\nPhân bón hữu cơ cung cấp đầy đủ dưỡng chất cho cây trồng (các dưỡng chất cần thiết (N, P, K)\r\nSử dụng phân bón hữu cơ vi sinh giúp cây trồng phát triển cân đối, bền vững và ổn định tăng chất lượng nông sản.\r\nĐảm bảo hàm lượng dinh dưỡng bổ sung chất mùn cho đất cân bằng vi sinh vật cho đất\r\nHạn chế xói mòn và hạn chế rửa trôi các chất dinh dưỡng, bảo vệ cấu trúc đất.\r\nPhân hữu cơ vi sinh còn có tác dụng rất lớn về việc cải tạo đất trồng đặc biệt là đất cát đất bạc màu.", Quantity = 100, Unit = "kg", Image = "https://songgianh.com.vn/upload/attachment/336vi-sinh-cc-sua.jpg", Status = "Available" },
                new Fertilizer { Id = 11, Name = "Dịch chiết Rong Biển", Type = "Chemical", Description = "Phân bón hữu cơ rong biển (Seaweed Extract Powder) là sản phẩm chiết xuất từ tảo biển tự nhiên, giàu khoáng chất, axit amin, hormone sinh trưởng và vi lượng thiết yếu giúp cây trồng phát triển khỏe mạnh, tăng sức đề kháng và nâng cao năng suất. Sản phẩm này hoàn toàn hữu cơ, an toàn cho môi trường, thích hợp cho nhiều loại cây trồng như rau màu, cây ăn trái, hoa kiểng và cây công nghiệp.", Quantity = 1000, Unit = "ml", Image = "https://salt.tikicdn.com/cache/w300/ts/product/c5/68/9e/70ee293fb7b47d89e77aa217a6d60511.jpg", Status = "Available" },
                new Fertilizer { Id = 12, Name = "Kali Sulphate", Type = "Chemical", Description = "K2SO4, hay Kali Sulphate, là một loại phân bón quan trọng trong nông nghiệp, cung cấp cả kali (K) và lưu huỳnh (S) cho cây trồng. Với 52% kali và 18% lưu huỳnh, K2SO4 đặc biệt hữu ích cho các loại cây có nhu cầu lưu huỳnh cao. Phân bón này thúc đẩy sự ra hoa sớm, chín sớm, cải thiện hương vị, màu sắc và mùi thơm của trái cây. Việc sử dụng K2SO4 đóng vai trò quan trọng trong việc tăng năng suất và chất lượng nông sản.", Quantity = 1000, Unit = "g", Image = "https://product.hstatic.net/1000238788/product/phan-bon-la-kali-sunphat-goi-1kg-anbio-vn_672ea77a6f984852b441edf45bb37a2f_master.jpg", Status = "Available" },
                new Fertilizer { Id = 13, Name = "Phân bò Tribat đã qua xử lý", Type = "Organic", Description = "Hướng dẫn sử dụng\r\n• Căn cứ theo từng loại cây trồng hoặc độ rộng của diện tích cần bón, cho một lượng phân bò vừa đủ xung quanh gốc cây, sau đó lấp một lớp đất khoảng 7- 8cm lên trên, tưới nước đủ ẩm\r\n\r\n• Không còn tuyến trùng và vi sinh vật gây hại bộ rễ của cây trồng, không còn mầm cỏ dại.\r\n\r\n• Cung cấp dinh dưỡng tự nhiên, giúp tất cả các loại cây trồng sinh trưởng và phát triển tốt, đặc biệt cho cây cảnh.", Quantity = 10000, Unit = "kg", Image = "https://product.hstatic.net/1000269461/product/11_e91236cef9c14acbafdc2e50684524d1_large.png", Status = "Available" },
                new Fertilizer { Id = 14, Name = "Phân Kali đỏ", Type = "Chemical", Description = "Phân kali là nhóm phân bón cung cấp kali, một chất dinh dưỡng thiết yếu cho cây trồng, dưới dạng ion K+. Các loại phân kali thông dụng bao gồm kali sunfat (K2SO4), có chứa 29% K2SO4 và 9% MgO. Phân kali thường là phân chua sinh lý và dễ hòa tan. Kali đóng vai trò quan trọng trong nhiều quá trình sinh lý của cây, ảnh hưởng đến sự phát triển, năng suất và chất lượng của cây trồng.\r\n\r\n", Quantity = 10000, Unit = "kg", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdhNtAr4gkSnKOBP7JXrthVCZxvtUzwsco4w&s", Status = "Available" }
            );

            modelBuilder.Entity<Pesticide>().HasData(
                new Pesticide { Id = 1, Name = "Neem Oil", Type = "Organic", Description = "Mix 5ml with 1 liter of water. Spray on plants every 7 days.", Quantity = 20, Unit = "liters", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCFbvgQnnXLUDetHBrkklU5PV1fsamB8Yt2Q&s", Status = "Available" },
                new Pesticide { Id = 2, Name = "Pyrethrin", Type = "Chemical", Description = "Dilute 10ml in 1 liter of water. Use in the evening for best results.", Quantity = 15, Unit = "liters", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyaA3fAHWe-2ncRTLEDdF5yQMCeXNlLWGW0w&s", Status = "Limited Stock" },
                new Pesticide { Id = 3, Name = "Sulfur Dust", Type = "Mineral", Description = "Apply 5g per square meter. Helps prevent fungal diseases.", Quantity = 50, Unit = "kg", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSly88nvIfyDWzSM0aNglb4NYlimIHfZQ7KDA&s", Status = "Out of Stock" },
                new Pesticide { Id = 4, Name = "Spinosad", Type = "Biological", Description = "Use 2ml per liter of water. Effective against caterpillars and thrips.", Quantity = 10, Unit = "liters", Image = "https://image.made-in-china.com/2f0j00rvSoQMsPbDbU/Spinosad-45-Sc-Agriculture-Insecticide-Agro-Chemicals.webp", Status = "Available" },
                new Pesticide { Id = 5, Name = "Copper Fungicide", Type = "Mineral", Description = "Dissolve 1g in 1 liter of water. Spray on leaves to prevent blight.", Quantity = 25, Unit = "kg", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTg9gWe6ZUxCxCQu_p89Run1nw2zS6PyjafCw&s", Status = "Available" },
                new Pesticide { Id = 6, Name = "Thuốc sinh học Radiant", Type = "Chemical", Description = "Thuốc trừ sâu bọ trĩ RADIANT 60SC được chiết xuất rất độc đáo từ thiên nhiên qua quy trình lên men và bán tổng hợp hiện đại nhất. Thế hệ thuốc trừ sâu tiên tiến nhất hiện nay, an toàn và thân thiện với môi trường. Hiệu lực nhanh, kéo dài. Thuốc có cơ chế tác động khác với các thuốc khác nên diệt trừ được các loại sâu hại khó trị và chống kháng thuốc.", Quantity = 100, Unit = "ml", Image = "https://loctroi.vn/UploadFiles/HinhDaiDien/311022094253393_Radiant--moi-.png", Status = "Available" },
                new Pesticide { Id = 7, Name = "Norshield 86.2WG", Type = "Chemical", Description = "Norshield 86.2WG đặc biệt hiệu quả khi kết hợp với Phytocide 50WP để phòng trừ các bệnh như thối thân, thối trái, vàng lá, chết nhanh, xì mủ và sương mai trên nhiều loại cây trồng. Hiệu quả này được đề cập trong các kết quả tìm kiếm và nhấn mạnh vai trò của sự kết hợp này trong việc kiểm soát các bệnh do nấm gây ra.", Quantity = 100, Unit = "g", Image = "https://www.hoptri.com/media/k2/items/cache/fbb4b6a86e716d470f9aaf0dffc43cd5_XL.jpg", Status = "Available" },
                new Pesticide { Id = 8, Name = "Eddy 72WP", Type = "Chemical", Description = "Eddy 72WP là dạng thuốc hỗn hợp với 2 hoạt chất:\r\n\r\nCuprous oxide: có độ bao phủ và trải đều trên bề mặt tiếp xúc với cây trồng rất tốt, độ bám dính cao, phổ tác dụng rộng nên hiệu quả phòng trừ bệnh kéo dài. Có khả năng phòng và trừ các bệnh do nấm và vi khuẩn gây ra.\r\nDimethomorph (hoạt chất mới): có khả năng lưu dẫn mạnh, hiệu quả với nhiều giai đoạn phát triển của nấm bệnh thông qua ức chế tổng hợp phospholipid trong màng tế bào nên hiệu quả phòng trừ bệnh rất cao. Hiệu quả cao với nấm Phytophthora đã kháng các loại thuốc trừ nấm thông dụng khác (Metalaxyl, Mefenoxam).", Quantity = 100, Unit = "g", Image = "https://www.hoptri.com/media/k2/items/cache/32088387da419227f20729e6cf7687d8_L.jpg", Status = "Available" },
                new Pesticide { Id = 9, Name = "Score 250EC", Type = "Chemical", Description = "HƯỚNG DẪN SỬ DỤNG:\r\n\r\nÓt, rau các loại trị bệnh Thán thư 0,3 – 0,5 Pha 5 – 10 ml /bình 8 lit Phun 300 – 500 lit nước /ha\r\nNho, táo, xoài Mốc xám, trị bệnh phấn trắng, sương mai 0,2 – 0,5 Pha 4 – 10 ml /bình 8 lit Phun 300 – 500 lit nước /ha\r\nXoài  bệnh Thán thư 0,08 – 0,1Pha 6 – 8 ml /bình 8 litPhun 300 – 500 lit nước /ha\r\nCà chua  bệnh Đốm vòng 0,3 – 0,5 Pha 5 – 10 ml /bình 8 lit Phun 300 – 500 lit nước /ha\r\nDưa hấu  bệnh  Nứt dây 0,3 – 0,5", Quantity = 1000, Unit = "ml", Image = "https://www.phandoiso1.com/wp-content/uploads/2021/01/Score-250EC-50ml-1.jpg", Status = "Available" },
                new Pesticide { Id = 10, Name = "Antracol 70WP", Type = "Chemical", Description = "- Giúp lá (xanh) khỏe, thẳng đứng và sạch bệnh.\r\n- Đặc trị phổ rộng nhiều loại bệnh hại trên lúa, rau, cây ăn trái, cây công nghiệp.\r\n- An toàn cho cây trồng, góp phần tăng năng suất và chất lượng nông sản.\r\n- 1kg Antracol chứa 150g kẽm tinh khiết cho cây.", Quantity = 1000, Unit = "g", Image = "https://www.cropscience.bayer.com.vn/-/media/Bayer%20CropScience/Country-Vietnam-Internet/2019/07/antracol_package.jpg", Status = "Available" },
                new Pesticide { Id = 11, Name = "Kasumin 2SL", Type = "Chemical", Description = "Hướng dẫn sử dụng\r\nCây trồng\tBệnh hại\tLiều lượng\r\nLúa\t\r\nBạc lá\r\n\r\nPha 32 – 45 ml/ 16 lít nước.\r\nPhun 2 lần: lần 1 khi lúa sắp trổ và lần 2 khi lúa trổ đều\r\n\r\nĐen lép hạt\r\n\r\nĐạo ôn\tPha 32 – 45 ml/ 16 lít nước.\r\nĐốm sọc\tPha 30 – 35 ml/ 16 lít nước.\r\nRau\tThối vi khuẩn\tPha 50 – 60 ml/ 16 lít nước.\r\nBắp cải\tThối vi khuẩn\tPha 40 – 45 ml/ 16 lít nước.\r\nĐậu phộng\tĐốm lá\r\nCam\tUng thư (Loét trái)\tPha 30 – 40 ml/ 16 lít nước.\r\nPhun khi tỉ lệ bệnh khoảng 5%\r\n\r\nThời gian cách ly : 7 ngày", Quantity = 1000, Unit = "ml", Image = "https://product.hstatic.net/1000238788/product/arysta-kasumin-2sl-425ml-moi-2022-anbio-vn_476cd4108f3b4f72981ba3d62be70921_master.jpg", Status = "Available" },
                new Pesticide { Id = 12, Name = "Trichoderma", Type = "Chemical", Description = "Nấm Trichoderma có thể cải thiện sức khỏe của cây trồng ngay cả khi không có sự hiện diện của các tác nhân gây bệnh. Trichoderma phát triển tốt nhất ở môi trường đất có độ pH thấp, và giúp tạo ra môi trường này bằng cách tiết ra các axit hữu cơ. Các axit này có một tác dụng bổ sung rất quan trọng – chúng giúp hoà tan các phốt phát và các ion khoáng chất, như sắt, magie, và mangan. Điều này làm cho các chất dinh dưỡng này dễ dàng hơn để cây hấp thu. Các chất dinh dưỡng này thường rất khan hiếm trong đất. Sự tăng trưởng và năng suất của cây trồng sẽ cao hơn khi đất ban đầu rất kém.", Quantity = 1000, Unit = "kg", Image = "https://pos.nvncdn.com/852a10-72624/ps/content/20230307_cHFn919Plgk6wb9UO6Hq8ojr.jpg", Status = "Available" },
                new Pesticide { Id = 13, Name = "Mancozeb", Type = "Chemical", Description = "Mancozeb là một hoạt chất trừ nấm hiệu quả, được sử dụng để phòng trừ hơn 400 bệnh hại trên 70 loại cây trồng. Hoạt chất này là sự kết hợp của Mangan và Kẽm, thuộc nhóm Dithiocarbamate, và có tác dụng tiếp xúc, ngăn chặn sự phát triển của nấm bệnh. Mancozeb hoạt động bằng cách ức chế enzyme trong nấm.", Quantity = 10000, Unit = "g", Image = "https://phunthuoctudong.com/storage/479/Mancozeb-xanh.jpg", Status = "Available" },
                new Pesticide { Id = 14, Name = "Metalaxyl", Type = "Chemical", Description = "Metalaxyl là một loại thuốc trừ nấm phổ rộng, thuộc nhóm acylalanine, được sử dụng để phòng và trị nhiều bệnh do nấm gây ra trên cây trồng. Nó có hiệu quả chống lại nhiều loại nấm, bao gồm các bệnh như vàng lá thối rễ, nứt thân xì mủ, thối trái và các bệnh do nấm thán thư, mốc sương gây ra. Metalaxyl được ứng dụng trong nông nghiệp để bảo vệ cây trồng khỏi sự tấn công của các loại nấm bệnh, giúp nâng cao năng suất và chất lượng cây trồng.", Quantity = 10000, Unit = "g", Image = "https://nongnghiepdep.vn/watermark/product/550x530x2/upload/product/metalaxyl-do-1569.png", Status = "Available" },
                new Pesticide { Id = 15, Name = "Emamectin Benzoate", Type = "Chemical", Description = "Emamectin Benzoate là một hoạt chất trừ sâu sinh học, phổ tác động rộng, được chiết xuất từ xạ khuẩn Streptomyces Avermitilis. Nó có tác dụng vị độc và tiếp xúc, hiệu quả với nhiều loại côn trùng miệng nhai và chích hút. Hoạt chất này không có tính nội hấp.\r\n\r\n", Quantity = 10000, Unit = "ml", Image = "https://vietthanghanoi.vn/wp-content/uploads/2022/07/Tasieu-1.9EC-450ml-1.jpg", Status = "Available" },
                new Pesticide { Id = 16, Name = "Thiamethoxam", Type = "Chemical", Description = "Thiamethoxam là một hoạt chất phổ biến có trong một số loại thuốc trừ sâu phổ rộng để kiểm soát sâu bệnh. Thiamethoxam là một chất tổng hợp và được biết đến như một hợp chất neonicotinoid thế hệ thứ hai thuộc phân nhóm hóa học Thianicotinyl. Thiamethoxam tác động lên một số thụ thể trong khớp thần kinh của côn trùng làm ảnh hưởng đến các chức năng bên trong cơ thể chúng.", Quantity = 10000, Unit = "ml", Image = "https://www.pomais.com/wp-content/uploads/2024/10/Thiamethoxam25SC-.webp", Status = "Available" },
                new Pesticide { Id = 17, Name = "Bacillus thuringiensis", Type = "Chemical", Description = "Loài Bacillus thuringiensis (đôi khi được viết tắt là Bt) là một loại vi khuẩn sống trong nước và đất. Nó là một tác nhân kiểm soát sinh học quan trọng vì nó có thể tiêu diệt nhiều loài gây hại thông thường gây hại cho cây trồng, nhưng nó tương đối vô hại đối với con người, động vật và côn trùng không gây hại.", Quantity = 10000, Unit = "g", Image = "https://image.made-in-china.com/202f0j00fHURDGPsTLbz/Bacillus-Thuringiensis-32000-Iu-Mg-Wp-Insecticides-for-Agriculture.webp", Status = "Available" },
                new Pesticide { Id = 18, Name = "Song sát vi khuẩn Chitosan + Polyoxin", Type = "Chemical", Description = "Công dụng: Sản phẩm là sự kết hợp của 2 hoạt chất sinh học(Chitosan và Polyoxin B), Được phối trộn với những phụ gia công nghệ mới, tạo nên bí quyết riêng biệt, phòng trừ hiệu quả các bệnh do Nấm và Vj khuẩn gây ra.Đặc biệt: Vàng lá, bạc lá/lúa;Thán thư/vải; Héo xanh/ cà chua; thối trái/ớt; thối nhũn/cải bắp; thối củ/hành; nứt thân xì mủ/dưa hấu, bầu bí; sẹo loét/ cam chanh….", Quantity = 10000, Unit = "g", Image = "https://nongnghiepxuyena.com/wp-content/uploads/2018/10/z3007940600542_6a8755b7e0abb45f73158da6cb3e2911-692x1024.jpg", Status = "Available" }
            );

            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, PlantId = 1, YieldId = 1, ExpertId = 1, PlanName = "Trồng cà chua vụ đông", Description = "Kế hoạch trồng cà chua vào mùa đông", StartDate = new DateTime(2025, 1, 10), EndDate = new DateTime(2025, 4, 15), CompleteDate = new DateTime(2024, 4, 15), Status = "Pending", EstimatedProduct = 500, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 2, PlantId = 2, YieldId = 2, ExpertId = 2, PlanName = "Trồng dưa lưới", Description = "Kế hoạch trồng dưa lưới trong nhà kính", StartDate = new DateTime(2025, 2, 1), EndDate = new DateTime(2025, 6, 1), CompleteDate = new DateTime(2024, 6, 5), Status = "Pending", EstimatedProduct = 300, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = false },
                new Plan { Id = 3, PlantId = 3, YieldId = 3, ExpertId = 3, PlanName = "Trồng bắp cải", Description = "Kế hoạch trồng bắp cải sạch", StartDate = new DateTime(2025, 3, 15), EndDate = new DateTime(2025, 6, 30), CompleteDate = new DateTime(2024, 6, 30), Status = "Pending", EstimatedProduct = 400, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 4, PlantId = 4, YieldId = 4, ExpertId = 1, PlanName = "Trồng rau muống", Description = "Kế hoạch trồng rau muống ngắn ngày", StartDate = new DateTime(2025, 4, 5), EndDate = new DateTime(2025, 5, 5), CompleteDate = new DateTime(2024, 5, 5), Status = "Pending", EstimatedProduct = 200, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 5, PlantId = 5, YieldId = 5, ExpertId = 2, PlanName = "Trồng cà rốt", Description = "Kế hoạch trồng cà rốt hữu cơ", StartDate = new DateTime(2025, 5, 1), EndDate = new DateTime(2025, 9, 1), CompleteDate = new DateTime(2024, 9, 2), Status = "Pending", EstimatedProduct = 350, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = false },
                new Plan { Id = 6, PlantId = 6, YieldId = 6, ExpertId = 3, PlanName = "Trồng hành lá", Description = "Kế hoạch trồng hành lá sạch", StartDate = new DateTime(2025, 6, 10), EndDate = new DateTime(2025, 9, 30), CompleteDate = new DateTime(2024, 9, 30), Status = "Pending", EstimatedProduct = 250, CreatedAt = DateTime.Now, CreatedBy = "Admin", IsApproved = true },
                new Plan { Id = 7, PlantId = 10, YieldId = 2, ExpertId = 2, PlanName = "Mùa vụ trồng củ cải trắng", Description = "Bản kế hoạch chi tiết trồng củ cải trắng ngắn hạn trong vòng 30 ngày", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), Status = "Draft", EstimatedProduct = 500, CreatedAt = DateTime.Now.AddDays(-10), CreatedBy = "Admin", IsApproved = false }
            );

            modelBuilder.Entity<FarmerPermission>().HasData(
                new FarmerPermission { FarmerId = 4, PlanId = 2, Status = "Active", CreatedAt = DateTime.Now.AddDays(-35) },
                new FarmerPermission { FarmerId = 1, PlanId = 2, Status = "Active", CreatedAt = DateTime.Now.AddDays(-32) },
                new FarmerPermission { FarmerId = 5, PlanId = 2, Status = "Active", CreatedAt = DateTime.Now.AddDays(-31) },
                new FarmerPermission { FarmerId = 2, PlanId = 1, Status = "Active", CreatedAt = DateTime.Now.AddDays(-10) },
                new FarmerPermission { FarmerId = 3, PlanId = 2, Status = "Active", CreatedAt = DateTime.Now.AddDays(-5) },
                new FarmerPermission { FarmerId = 1, PlanId = 1, Status = "Active", CreatedAt = DateTime.Now.AddDays(-55) },
                new FarmerPermission { FarmerId = 3, PlanId = 1, Status = "Active", CreatedAt = DateTime.Now.AddDays(-30) },
                new FarmerPermission { FarmerId = 5, PlanId = 1, Status = "Active", CreatedAt = DateTime.Now.AddDays(-2) }
            );

            modelBuilder.Entity<CaringTask>().HasData(
                new CaringTask { Id = 1, PlanId = 1, ProblemId = 1, Description = "Tưới nước đều đặn vào sáng sớm và chiều tối để giữ ẩm cho cây cà chua, tránh tưới quá nhiều gây ngập úng.", TaskName = "Tưới nước cho cà chua", TaskType = "Watering", StartDate = new DateTime(2025, 1, 12), EndDate = new DateTime(2025, 1, 15), CompleteDate = new DateTime(2025, 1, 20), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 2, PlanId = 2, ProblemId = 2, Description = "Sử dụng phân hữu cơ để cung cấp dưỡng chất cho cây cà chua, bón vào gốc cây tránh tiếp xúc trực tiếp với lá.", TaskName = "Bón phân hữu cơ cho cà chua", TaskType = "Fertilizing", StartDate = new DateTime(2025, 2, 18), EndDate = new DateTime(2025, 2, 20), CompleteDate = new DateTime(2025, 2, 20), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 3, PlanId = 2, Description = "Quan sát lá, thân và quả dưa lưới để phát hiện dấu hiệu sâu bệnh, sử dụng biện pháp phòng trừ phù hợp.", TaskName = "Kiểm tra sâu bệnh trên dưa lưới", TaskType = "Weeding", StartDate = new DateTime(2025, 2, 10), EndDate = new DateTime(2025, 2, 12), CompleteDate = new DateTime(2025, 2, 13), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 4, PlanId = 2, ProblemId = 4, Description = "Thiết lập hệ thống tưới nhỏ giọt giúp cây nhận đủ nước mà không gây lãng phí.", TaskName = "Lắp hệ thống tưới tự động", TaskType = "Setup", StartDate = new DateTime(2025, 2, 15), EndDate = new DateTime(2025, 2, 18), CompleteDate = new DateTime(2025, 2, 20), Status = "Ongoing", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 5, PlanId = 3, ProblemId = 5, Description = "Loại bỏ cỏ dại xung quanh bắp cải để tránh cạnh tranh dinh dưỡng và ngăn ngừa sâu bệnh.", TaskName = "Nhổ cỏ dại quanh bắp cải", TaskType = "Weeding", StartDate = new DateTime(2025, 3, 20), EndDate = new DateTime(2024, 3, 22), CompleteDate = new DateTime(2025, 3, 30), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 6, PlanId = 4, Description = "Sử dụng thuốc sinh học phòng trừ bệnh nấm và sâu hại trên rau muống, đảm bảo an toàn thực phẩm.", TaskName = "Phun thuốc phòng bệnh cho rau muống", TaskType = "Pesticide", StartDate = new DateTime(2025, 4, 7), EndDate = new DateTime(2025, 4, 10), CompleteDate = new DateTime(2025, 4, 15), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 7, PlanId = 4, ProblemId = 7, Description = "Dọn sạch nilon, chai lọ, bao bì thuốc bảo vệ thực vật để giữ gìn môi trường sạch sẽ.", TaskName = "Thu gom rác nông nghiệp", TaskType = "Cleaning", StartDate = new DateTime(2025, 4, 12), EndDate = new DateTime(2025, 4, 14), CompleteDate = new DateTime(2025, 4, 20), Status = "Complete", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 8, PlanId = 5, ProblemId = 8, Description = "Tưới nước vừa đủ giúp cà rốt phát triển đều, tránh tình trạng úng rễ hoặc khô hạn.", TaskName = "Tưới nước cho cà rốt", TaskType = "Watering", StartDate = new DateTime(2025, 5, 5), EndDate = new DateTime(2025, 5, 7), CompleteDate = new DateTime(2025, 5, 7), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 9, PlanId = 6, ProblemId = 9, Description = "Phun phân bón lá để thúc đẩy sự phát triển của hành lá, đảm bảo đủ dưỡng chất.", TaskName = "Bón phân lá cho hành lá", TaskType = "Fertilizing", StartDate = new DateTime(2025, 6, 15), EndDate = new DateTime(2025, 6, 17), CompleteDate = new DateTime(2025, 6, 20), Status = "Ongoing", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 10, PlanId = 1, Description = "Quan sát kỹ các lá non và bông mướp để phát hiện dấu hiệu sâu bệnh sớm.", TaskName = "Kiểm tra côn trùng gây hại trên mướp hương", TaskType = "Weeding", StartDate = new DateTime(2025, 7, 12), EndDate = new DateTime(2025, 7, 15), CompleteDate = new DateTime(2025, 7, 20), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 11, PlanId = 2, ProblemId = 1, Description = "Loại bỏ cành không cần thiết để tập trung dinh dưỡng cho quả ớt phát triển.", TaskName = "Cắt tỉa cành ớt chuông", TaskType = "Pruning", StartDate = new DateTime(2025, 8, 20), EndDate = new DateTime(2025, 8, 22), CompleteDate = new DateTime(2025, 8, 22), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 12, PlanId = 3, ProblemId = 2, Description = "Dùng hệ thống phun sương để tưới nước cho rau xà lách, giúp lá luôn tươi tốt.", TaskName = "Tưới phun sương cho rau xà lách", TaskType = "Watering", StartDate = new DateTime(2025, 9, 10), EndDate = new DateTime(2025, 9, 12), CompleteDate = new DateTime(2025, 9, 20), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 13, PlanId = 4, ProblemId = 3, Description = "Loại bỏ cỏ dại thủ công để tránh ảnh hưởng đến xà lách non.", TaskName = "Nhổ cỏ dại trong vườn xà lách", TaskType = "Weeding", StartDate = new DateTime(2025, 9, 15), EndDate = new DateTime(2025, 9, 17), CompleteDate = new DateTime(2025, 9, 20), Status = "Complete", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 14, PlanId = 5, Description = "Loại bỏ cỏ dại thủ công để tránh ảnh hưởng đến xà lách non.", TaskName = "Bón phân NPK cho cải ngọt", TaskType = "Fertilizing", StartDate = new DateTime(2025, 10, 5), EndDate = new DateTime(2025, 10, 8), CompleteDate = new DateTime(2025, 10, 20), Status = "Ongoing", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" },
                new CaringTask { Id = 15, PlanId = 6, ProblemId = 5, Description = "Loại bỏ cỏ dại thủ công để tránh ảnh hưởng đến xà lách non.", TaskName = "Phun thuốc sinh học phòng bệnh cho cải ngọt", TaskType = "Pesticide", StartDate = new DateTime(2025, 10, 12), EndDate = new DateTime(2025, 10, 15), CompleteDate = new DateTime(2025, 10, 15), Status = "Pending", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo" }
            );

            modelBuilder.Entity<FarmerCaringTask>().HasData(
                new FarmerCaringTask { FarmerId = 1, TaskId = 1, Description = "Soil preparation delayed due to unexpected rain.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(3) },
                new FarmerCaringTask { FarmerId = 2, TaskId = 3, Description = "Weeding completed successfully.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-1) },
                new FarmerCaringTask { FarmerId = 3, TaskId = 5, Description = "Fertilizer application postponed due to supply shortage.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(5) },
                new FarmerCaringTask { FarmerId = 4, TaskId = 7, Description = "Irrigation system maintenance completed.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-2) },
                new FarmerCaringTask { FarmerId = 5, TaskId = 9, Description = "Pest control activity in progress.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(2) },
                new FarmerCaringTask { FarmerId = 6, TaskId = 11, Description = "Harvest preparation started.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(4) },
                new FarmerCaringTask { FarmerId = 1, TaskId = 13, Description = "Crop monitoring performed with drone imaging.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-1) },
                new FarmerCaringTask { FarmerId = 2, TaskId = 2, Description = "Seed sowing delayed due to broken equipment.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(6) },
                new FarmerCaringTask { FarmerId = 3, TaskId = 4, Description = "Applying compost to improve soil fertility.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-3) },
                new FarmerCaringTask { FarmerId = 4, TaskId = 6, Description = "Plant disease detected, applying treatment.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(3) },
                new FarmerCaringTask { FarmerId = 5, TaskId = 8, Description = "Weed removal completed successfully.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-1) },
                new FarmerCaringTask { FarmerId = 6, TaskId = 10, Description = "Installing new irrigation pipes.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(5) },
                new FarmerCaringTask { FarmerId = 1, TaskId = 12, Description = "Harvesting completed for lettuce field.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-2) },
                new FarmerCaringTask { FarmerId = 2, TaskId = 14, Description = "Scheduled pest control task delayed.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(4) },
                new FarmerCaringTask { FarmerId = 3, TaskId = 15, Description = "Monitoring crop growth using sensors.", Status = "Active", ExpiredDate = DateTime.Now.AddDays(-1) }
            );

            modelBuilder.Entity<CaringPesticide>().HasData(
                new CaringPesticide { Id = 1, PesticideId = 1, TaskId = 6, Unit = "Lít", Quantity = 2.5f },
                new CaringPesticide { Id = 2, PesticideId = 2, TaskId = 1, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 3, PesticideId = 4, TaskId = 2, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 4, PesticideId = 5, TaskId = 3, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 5, PesticideId = 4, TaskId = 4, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 6, PesticideId = 3, TaskId = 5, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 7, PesticideId = 2, TaskId = 7, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 8, PesticideId = 4, TaskId = 8, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 9, PesticideId = 5, TaskId = 9, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 10, PesticideId = 1, TaskId = 10, Unit = "Lít", Quantity = 3.0f },
                new CaringPesticide { Id = 11, PesticideId = 2, TaskId = 11, Unit = "Lít", Quantity = 3.0f }
            );

            modelBuilder.Entity<CaringFertilizer>().HasData(
                new CaringFertilizer { Id = 1, FertilizerId = 1, TaskId = 2, Unit = "Kg", Quantity = 5.0f },
                new CaringFertilizer { Id = 2, FertilizerId = 2, TaskId = 9, Unit = "Kg", Quantity = 4.0f },
                new CaringFertilizer { Id = 3, FertilizerId = 3, TaskId = 12, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 4, FertilizerId = 2, TaskId = 15, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 5, FertilizerId = 1, TaskId = 13, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 6, FertilizerId = 3, TaskId = 8, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 7, FertilizerId = 4, TaskId = 1, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 8, FertilizerId = 2, TaskId = 4, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 9, FertilizerId = 3, TaskId = 2, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 10, FertilizerId = 1, TaskId = 13, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 11, FertilizerId = 2, TaskId = 14, Unit = "Kg", Quantity = 6.0f },
                new CaringFertilizer { Id = 12, FertilizerId = 3, TaskId = 14, Unit = "Kg", Quantity = 6.0f }
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
                new HarvestingTask { Id = 1, PlanId = 2, TaskName = "Thu hoạch rau cải", Description = "Thu hoạch rau cải trước khi trời quá nắng", ResultContent = "Đã hủy vì cây không đạt chất lượng kiểm định", StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(-3), CompleteDate = DateTime.Now, HarvestedQuantity = 5000, CreatedAt = DateTime.Now.AddDays(-5), CreatedBy = "thangbinhbeo", Status = "Cancel" },
                new HarvestingTask { Id = 2, PlanId = 1, TaskName = "Thu hoạch cà chua", Description = "Thu hoạch cà chua chín đỏ", StartDate = DateTime.Now.AddDays(-1).AddDays(-6), EndDate = DateTime.Now.AddDays(-4), CompleteDate = DateTime.Now.AddDays(-1), HarvestedQuantity = 3000, CreatedAt = DateTime.Now.AddDays(-1).AddDays(-6), CreatedBy = "thangbinhbeo", Status = "Pending" },
                new HarvestingTask { Id = 3, PlanId = 3, TaskName = "Thu hoạch bắp cải", Description = "Thu hoạch bắp cải vào sáng sớm để giữ độ tươi", ResultContent = "Đã hủy vì cây chết hết rồi", StartDate = DateTime.Now.AddDays(-10).AddDays(-3), EndDate = DateTime.Now.AddDays(-2), CompleteDate = DateTime.Now.AddDays(-2), HarvestedQuantity = 4000, CreatedAt = DateTime.Now.AddDays(-2).AddDays(-7), CreatedBy = "thangbinhbeo", Status = "Cancel" },
                new HarvestingTask { Id = 4, PlanId = 4, TaskName = "Thu hoạch dưa leo", Description = "Thu hoạch dưa leo vào đúng thời điểm chín", StartDate = DateTime.Now.AddDays(-9), EndDate = DateTime.Now.AddDays(-1), CompleteDate = DateTime.Now, HarvestedQuantity = 2000, CreatedAt = DateTime.Now.AddDays(-4), CreatedBy = "thangbinhbeo", Status = "Pending" },
                new HarvestingTask { Id = 5, PlanId = 5, TaskName = "Thu hoạch bí đỏ", Description = "Thu hoạch bí đỏ khi vỏ cứng lại", StartDate = DateTime.Now.AddDays(-9), EndDate = DateTime.Now.AddDays(-2), CompleteDate = DateTime.Now.AddDays(-1), HarvestedQuantity = 1500, CreatedAt = DateTime.Now.AddDays(-8), CreatedBy = "thangbinhbeo", Status = "Pending" }
            );

            modelBuilder.Entity<FarmerHarvestingTask>().HasData(
                new FarmerHarvestingTask { FarmerId = 1, TaskId = 1, Description = "Thu hoạch rau cải theo tiêu chuẩn hữu cơ, cắt sạch gốc và đóng gói.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(-1) },
                new FarmerHarvestingTask { FarmerId = 2, TaskId = 2, Description = "Thu hoạch cà chua chín, phân loại quả chất lượng cao trước khi vận chuyển.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(3) },
                new FarmerHarvestingTask { FarmerId = 3, TaskId = 3, Description = "Thu hái rau muống, đảm bảo không lẫn tạp chất trong sản phẩm.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(-2) },
                new FarmerHarvestingTask { FarmerId = 4, TaskId = 4, Description = "Thu hoạch dưa leo khi đạt kích thước tiêu chuẩn, kiểm tra chất lượng từng quả.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(1) },
                new FarmerHarvestingTask { FarmerId = 5, TaskId = 5, Description = "Thu hoạch bắp cải non, đảm bảo không có sâu bệnh trước khi đóng gói.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(-3) },
                new FarmerHarvestingTask { FarmerId = 6, TaskId = 1, Description = "Thu hái rau dền đúng thời điểm để đảm bảo độ tươi ngon.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(-1) },
                new FarmerHarvestingTask { FarmerId = 1, TaskId = 3, Description = "Thu hoạch cải xanh và vận chuyển ngay sau khi thu hoạch.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(2) },
                new FarmerHarvestingTask { FarmerId = 2, TaskId = 5, Description = "Thu hoạch hành lá, buộc thành bó nhỏ trước khi phân phối.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(4) }
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
                    Description = "Đánh giá chất lượng rau cải trước khi thu hoạch",
                    StartDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(-4),
                    CompleteDate = DateTime.Now.AddDays(-4),
                    Status = "Complete",
                    CreatedAt = DateTime.Now.AddDays(-6),
                    CreatedBy = "thangbinhbeo"
                },

                new InspectingForm
                {
                    Id = 2,
                    PlanId = 2,
                    InspectorId = 1,
                    FormName = "Kiểm tra cà chua",
                    Description = "Đánh giá màu sắc và chất lượng cà chua",
                    StartDate = DateTime.Now.AddDays(-6),
                    EndDate = DateTime.Now.AddDays(-5),
                    CompleteDate = DateTime.Now.AddDays(-5),
                    Status = "Complete",
                    CreatedBy = "thangbinhbeo",
                    CreatedAt = DateTime.Now.AddDays(-7)
                },

                new InspectingForm
                {
                    Id = 3,
                    PlanId = 3,
                    InspectorId = 2,
                    FormName = "Kiểm tra bắp cải",
                    Description = "Kiểm tra độ ẩm và màu sắc bắp cải",
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now.AddDays(-6),
                    CompleteDate = DateTime.Now.AddDays(-6),
                    Status = "Pending",
                    CreatedBy = "thangbinhbeo",
                    CreatedAt = DateTime.Now.AddDays(-8)
                },

                new InspectingForm
                {
                    Id = 4,
                    PlanId = 4,
                    InspectorId = 1,
                    FormName = "Kiểm tra dưa leo",
                    Description = "Xác định độ chín và độ giòn của dưa leo",
                    StartDate = DateTime.Now.AddDays(-8),
                    EndDate = DateTime.Now.AddDays(-7),
                    CompleteDate = DateTime.Now.AddDays(-7),
                    Status = "Complete",
                    CreatedBy = "thangbinhbeo",
                    CreatedAt = DateTime.Now.AddDays(-9)
                },

                new InspectingForm
                {
                    Id = 5,
                    PlanId = 5,
                    InspectorId = 2,
                    FormName = "Kiểm tra bí đỏ",
                    Description = "Kiểm tra vỏ bí đỏ để xác định độ cứng",
                    StartDate = DateTime.Now.AddDays(-9),
                    EndDate = DateTime.Now.AddDays(-8),
                    CompleteDate = DateTime.Now.AddDays(-8),
                    Status = "Complete",
                    CreatedBy = "thangbinhbeo",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },

                new InspectingForm
                {
                    Id = 6,
                    PlanId = 6,
                    InspectorId = 1,
                    FormName = "Kiểm tra ớt chuông",
                    Description = "Đánh giá độ ngọt và màu sắc của ớt chuông",
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-9),
                    CompleteDate = DateTime.Now.AddDays(-9),
                    Status = "Complete",
                    CreatedBy = "thangbinhbeo",
                    CreatedAt = DateTime.Now.AddDays(-11)
                }
            );

            modelBuilder.Entity<PackagingTask>().HasData(
                new PackagingTask { Id = 1, PlanId = 1, TaskName = "Đóng gói gạo", ResultContent = "Đã đóng gói theo túi 5kg, thu được 1000 túi", TotalPackagedWeight = 1000, PackagedItemCount = 20, Description = "Đóng gói gạo vào túi 5kg", StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(2), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-6), CreatedBy = "thangbinhbeo", UpdatedAt = DateTime.Now },
                new PackagingTask { Id = 2, PlanId = 2, TaskName = "Đóng gói cà phê", ResultContent = "Đã đóng gói được 500 túi", TotalPackagedWeight = 500, PackagedItemCount = 20, Description = "Đóng gói cà phê bột vào túi 1kg", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(1), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-4), CreatedBy = "thangbinhbeo", UpdatedAt = DateTime.Now },
                new PackagingTask { Id = 3, PlanId = 3, TaskName = "Đóng gói trà xanh", ResultContent = "Đã đóng gói thành công 20000 gói", TotalPackagedWeight = 20000, PackagedItemCount = 10, Description = "Đóng gói trà xanh vào hộp 100g", StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(3), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-3), CreatedBy = "thangbinhbeo", UpdatedAt = DateTime.Now },
                new PackagingTask { Id = 4, PlanId = 4, TaskName = "Đóng gói hạt điều", ResultContent = "Đã đóng gói thành công", TotalPackagedWeight = 300, PackagedItemCount = 1, Description = "Đóng gói hạt điều vào túi 500g", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(4), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now.AddDays(-1), CreatedBy = "thangbinhbeo", UpdatedAt = DateTime.Now },
                new PackagingTask { Id = 5, PlanId = 5, TaskName = "Đóng gói xoài sấy", ResultContent = "Đã đóng gói 10000 gói", TotalPackagedWeight = 10000, PackagedItemCount = 2, Description = "Đóng gói xoài sấy vào túi 250g", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(5), CompleteDate = DateTime.Now.AddDays(3), Status = "Complete", CreatedAt = DateTime.Now, CreatedBy = "thangbinhbeo", UpdatedAt = DateTime.Now }
            );

            modelBuilder.Entity<FarmerPackagingTask>().HasData(
                new FarmerPackagingTask { FarmerId = 1, TaskId = 1, Description = "Đóng gói rau cải vào túi hút chân không, đảm bảo giữ độ tươi lâu.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(-1) },
                new FarmerPackagingTask { FarmerId = 2, TaskId = 2, Description = "Phân loại và đóng hộp cà chua theo kích cỡ, ghi nhãn nguồn gốc.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(3) },
                new FarmerPackagingTask { FarmerId = 3, TaskId = 3, Description = "Đóng gói rau muống vào túi lưới, tránh đè nén gây hư hỏng.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(-2) },
                new FarmerPackagingTask { FarmerId = 4, TaskId = 4, Description = "Đặt dưa leo vào khay nhựa, bọc màng co để bảo vệ độ tươi.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(1) },
                new FarmerPackagingTask { FarmerId = 5, TaskId = 5, Description = "Bọc bắp cải bằng giấy thực phẩm, bảo quản trong hộp carton.", Status = "Active", ExpiredDate = DateTime.UtcNow.AddDays(-3) },
                new FarmerPackagingTask { FarmerId = 6, TaskId = 1, Description = "Đóng túi rau dền theo định lượng 500g, kiểm tra độ sạch trước khi niêm phong.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(-1) },
                new FarmerPackagingTask { FarmerId = 1, TaskId = 3, Description = "Sắp xếp rau húng quế vào hộp nhựa, đảm bảo không dập nát.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(2) },
                new FarmerPackagingTask { FarmerId = 3, TaskId = 5, Description = "Niêm phong bao bì rau mồng tơi, gắn mã QR để truy xuất nguồn gốc.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(-4) },
                new FarmerPackagingTask { FarmerId = 5, TaskId = 2, Description = "Phân chia đậu bắp vào túi nhỏ 250g, đảm bảo đạt tiêu chuẩn an toàn.", Status = "Inactive", ExpiredDate = DateTime.UtcNow.AddDays(1) }
            );

            modelBuilder.Entity<PackagingImage>().HasData(
                new PackagingImage { Id = 1, TaskId = 1, Url = "https://maydonggoi.com.vn/wp-content/uploads/2021/12/quy-trinh-dong-goi-gao-6.jpg" },
                new PackagingImage { Id = 2, TaskId = 2, Url = "https://maygoi.vn/wp-content/uploads/2019/03/maxresdefault-1.jpg" },
                new PackagingImage { Id = 3, TaskId = 3, Url = "https://thainguyencity.gov.vn/upload/news/2013/11/532/image/img5751.jpg" },
                new PackagingImage { Id = 4, TaskId = 4, Url = "https://jraifarm.com/files/upload/files/h%E1%BA%A1t%20%C4%91i%E1%BB%81u/Quy%20c%C3%A1ch%20%C4%91%C3%B3ng%20g%C3%B3i%20h%E1%BA%A1t%20%C4%91i%E1%BB%81u%20xu%E1%BA%A5t%20kh%E1%BA%A9u%204.png" },
                new PackagingImage { Id = 5, TaskId = 5, Url = "https://nanufoods.vn/wp-content/uploads/2020/05/Soft-dried-mango-in-carton-579x400.jpg" }
            );

            modelBuilder.Entity<Problem>().HasData(
                new Problem { Id = 1, PlanId = 1, FarmerId = 6, ProblemName = "Sâu bệnh trên lá", Description = "Xuất hiện sâu ăn lá trên cây.", CreatedDate = DateTime.Parse("2025-02-01"), Status = "Pending", ResultContent = null },
                new Problem { Id = 2, PlanId = 2, FarmerId = 6, ProblemName = "Thiếu nước", Description = "Đất khô, cây có dấu hiệu héo.", CreatedDate = DateTime.Parse("2025-02-05"), Status = "Resolved", ResultContent = "Đã tưới nước bổ sung." },
                new Problem { Id = 3, PlanId = 3, FarmerId = 6, ProblemName = "Đất kém dinh dưỡng", Description = "Lá vàng, cây chậm phát triển.", CreatedDate = DateTime.Parse("2025-02-10"), Status = "Pending", ResultContent = null },
                new Problem { Id = 4, PlanId = 3, FarmerId = 6, ProblemName = "Cây bị nấm", Description = "Xuất hiện đốm trắng trên lá.", CreatedDate = DateTime.Parse("2025-02-12"), Status = "Resolved", ResultContent = "Đã phun thuốc chống nấm." },
                new Problem { Id = 5, PlanId = 3, FarmerId = 6, ProblemName = "Thiếu ánh sáng", Description = "Cây phát triển yếu do ánh sáng yếu.", CreatedDate = DateTime.Parse("2025-02-15"), Status = "Pending", ResultContent = null },
                new Problem { Id = 6, PlanId = 4, FarmerId = 6, ProblemName = "Sâu đục thân", Description = "Phát hiện dấu hiệu sâu đục thân cây.", CreatedDate = DateTime.Parse("2025-02-18"), Status = "Resolved", ResultContent = "Đã xử lý bằng thuốc trừ sâu." },
                new Problem { Id = 7, PlanId = 4, FarmerId = 6, ProblemName = "Mưa quá nhiều", Description = "Đất ẩm lâu, có nguy cơ úng rễ.", CreatedDate = DateTime.Parse("2025-02-20"), Status = "Pending", ResultContent = null },
                new Problem { Id = 8, PlanId = 5, FarmerId = 6, ProblemName = "Cây bị héo", Description = "Cây không đủ dinh dưỡng, lá rụng nhiều.", CreatedDate = DateTime.Parse("2025-02-22"), Status = "Resolved", ResultContent = "Đã bổ sung phân bón." },
                new Problem { Id = 9, PlanId = 6, FarmerId = 6, ProblemName = "Bọ trĩ tấn công", Description = "Bọ trĩ gây hại trên lá non.", CreatedDate = DateTime.Parse("2025-02-25"), Status = "Pending", ResultContent = null },
                new Problem { Id = 10, PlanId = 6, FarmerId = 6, ProblemName = "Nhiệt độ quá cao", Description = "Nắng nóng kéo dài gây stress cho cây.", CreatedDate = DateTime.Parse("2025-02-28"), Status = "Resolved", ResultContent = "Đã che bóng giảm nhiệt độ." }
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

            modelBuilder.Entity<InspectingResult>().HasData(
                new InspectingResult { Id = 1, Arsen = 0.01f, Plumbum = 0.02f, Cadmi = 0.005f, Hydrargyrum = 0.001f, Salmonella = 0, Coliforms = 10, Ecoli = 1, Glyphosate_Glufosinate = 0.02f, SulfurDioxide = 5, MethylBromide = 0.1f, HydrogenPhosphide = 0.05f, Dithiocarbamate = 0.02f, Nitrat = 50, NaNO3_KNO3 = 10, Chlorate = 0.1f, Perchlorate = 0.02f, EvaluatedResult = "Grade 1", ResultContent = "Rau cải đạt chuẩn" },
                new InspectingResult { Id = 2, Arsen = 0.02f, Plumbum = 0.03f, Cadmi = 0.007f, Hydrargyrum = 0.002f, Salmonella = 1, Coliforms = 20, Ecoli = 3, Glyphosate_Glufosinate = 0.03f, SulfurDioxide = 10, MethylBromide = 0.2f, HydrogenPhosphide = 0.06f, Dithiocarbamate = 0.03f, Nitrat = 60, NaNO3_KNO3 = 15, Chlorate = 0.2f, Perchlorate = 0.03f, EvaluatedResult = "Grade 3", ResultContent = "Cà chua đạt độ chín" },
                new InspectingResult { Id = 3, Arsen = 0.005f, Plumbum = 0.015f, Cadmi = 0.004f, Hydrargyrum = 0.0005f, Salmonella = 0, Coliforms = 5, Ecoli = 0, Glyphosate_Glufosinate = 0.015f, SulfurDioxide = 3, MethylBromide = 0.05f, HydrogenPhosphide = 0.03f, Dithiocarbamate = 0.015f, Nitrat = 40, NaNO3_KNO3 = 8, Chlorate = 0.08f, Perchlorate = 0.015f, EvaluatedResult = "Grade 2", ResultContent = "Ớt chuông có độ ngọt tốt" },
                new InspectingResult { Id = 4, Arsen = 0.03f, Plumbum = 0.04f, Cadmi = 0.01f, Hydrargyrum = 0.003f, Salmonella = 2, Coliforms = 25, Ecoli = 5, Glyphosate_Glufosinate = 0.04f, SulfurDioxide = 15, MethylBromide = 0.3f, HydrogenPhosphide = 0.08f, Dithiocarbamate = 0.04f, Nitrat = 70, NaNO3_KNO3 = 20, Chlorate = 0.3f, Perchlorate = 0.04f, EvaluatedResult = "Grade 2", ResultContent = "Dưa leo đạt chuẩn" },
                new InspectingResult { Id = 5, Arsen = 0.007f, Plumbum = 0.017f, Cadmi = 0.006f, Hydrargyrum = 0.0012f, Salmonella = 0, Coliforms = 8, Ecoli = 1, Glyphosate_Glufosinate = 0.018f, SulfurDioxide = 4, MethylBromide = 0.07f, HydrogenPhosphide = 0.04f, Dithiocarbamate = 0.018f, Nitrat = 45, NaNO3_KNO3 = 9, Chlorate = 0.09f, Perchlorate = 0.018f, EvaluatedResult = "Grade 3", ResultContent = "Bắp cải hơi thiếu nước" },
                new InspectingResult { Id = 6, Arsen = 0.011f, Plumbum = 0.021f, Cadmi = 0.006f, Hydrargyrum = 0.0011f, Salmonella = 0, Coliforms = 9, Ecoli = 1, Glyphosate_Glufosinate = 0.022f, SulfurDioxide = 6, MethylBromide = 0.12f, HydrogenPhosphide = 0.055f, Dithiocarbamate = 0.025f, Nitrat = 55, NaNO3_KNO3 = 12, Chlorate = 0.11f, Perchlorate = 0.022f, EvaluatedResult = "Grade 1", ResultContent = "Vỏ bí đỏ chưa đủ cứng" }
            );

            modelBuilder.Entity<Item>().HasData(
                // (Caring Task)
                new Item { Id = 1, Name = "Bình tưới cây", Description = "Bình tưới nước dung tích 5L", Quantity = 100, Unit = "Cái", Image = "https://product.hstatic.net/200000199113/product/7220965_7709e84c2e3f40cf8111c44225c96646_large.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 2, Name = "Kéo cắt tỉa", Description = "Kéo chuyên dụng để cắt tỉa cành", Quantity = 100, Unit = "Cái", Image = "https://fact-depot.com/tmp/cache/images/_thumbs/720x720/media/product/30542/Keo-cat-tia-cong-vien-cay-xanh-HM044-cat-tia-co-la.png", Status = "Active", Type = "Caring" },
                new Item { Id = 3, Name = "Bón phân hữu cơ", Description = "Dụng cụ bón phân dạng viên", Quantity = 100, Unit = "Hộp", Image = "https://vn-live-01.slatic.net/p/eab87be47ffa092ca1becbc00ff06ed2.jpg", Status = "In-stock", Type = "Caring" },
                new Item { Id = 4, Name = "Máy đo độ ẩm đất", Description = "Dụng cụ đo độ ẩm của đất", Quantity = 100, Unit = "Cái", Image = "https://thbvn.com/cdn/images/may-do-do-am/dung-cu-do-do-am-dat-tot-1.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 13, Name = "Cái cào", Description = "Chủ yếu dùng làm tơi đất hay san phẳng bề mặt đất, với loại cào này răng của nó thường được làm thưa và dài hơn cào nhiều răng nhưng độ chắc khỏe lại rất tốt. Để san phẳng đất bạn hãy làm như sau: Hai tay của bạn nắm chắc vào cán cào, một chân bước lên trước, lưng thẳng và hơi cúi người đồng thời bạn hạ thấp trọng tâm xuống dưới, tư thế giống như đứng cuốc đất là được, tiếp theo bạn dùng lực bổ nhẹ cào xuống đất để cho đất được tơi và bằng phẳng, bạn cứ làm như thế đến khi nào đạt được độ bằng phẳng như ý thì thôi. các bạn nên chú ý khi dùng Cào đất trồng cây  thì phải cào đất từ trên xuống dưới và từ bên này qua bên kia để không giẫm lên phần đất đã cào rồi.", Quantity = 100, Unit = "Cái", Image = "https://cayvahoa.net/wp-content/uploads/2016/11/C%C3%A0o-%C4%91%E1%BA%A5t-tr%E1%BB%93ng-c%C3%A2y-2.jpeg", Status = "Active", Type = "Caring" },
                new Item { Id = 14, Name = "Rổ đựng hạt", Description = "HƯỚNG DẪN SỬ DỤNG VÀ BẢO QUẢN SẢN PHẨM ❤️  \r\n\r\n  -   Để nơi khô ráo, tránh các chất tẩy rửa\r\n\r\n  -   Sử dụng khăn ẩm để lau các vết dơ trong quá trình sử dụng, không ngâm sản phẩm vào nước\r\n\r\n  -   Phơi nắng sau khi lau bằng khăn ẩm", Quantity = 100, Unit = "Cái", Image = "https://down-vn.img.susercontent.com/file/vn-11134207-7r98o-lzkfh6a6mr7x0d", Status = "Active", Type = "Harvesting" },
                new Item { Id = 15, Name = "Máy bơm nước", Description = "Lưu ý khi sử dụng máy bơm GP-350JA\r\nKhông sử dụng với nguồn nước không ổn định, lúc có nước, lúc không, gây nhanh mòn phớt, trục bơm gây rỉ nước buồng bơm.\r\nKhông sử dụng với nguồn nước có nhiệt độ cao hơn 45⁰C hoặc chứa nhiều cặn, sỏi, vì sẽ khiến máy bơm bị kẹt và giảm tuổi thọ.\r\nLắp đặt máy bơm GP-350JA gần nguồn nước đầu vào, hiệu quả đẩy nước sẽ được tăng cường đáng kể.", Quantity = 100, Unit = "Máy", Image = "https://thietbipanasonic.vn/wp-content/uploads/2023/04/may-bom-nuoc-day-cao-panasonic-gp-350ja.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 16, Name = "Bộ dụng cụ làm cỏ", Description = "Như vậy để nâng cao hiệu quả trong quá trình diệt cỏ dại, cần xem xét đến các yếu tố về tình trạng thảm cỏ, các loại cây trồng xung quanh, tác hại và cả lợi ích mà cỏ dại mang đến.\r\n\r\nSau đó bạn hoàn toàn có thể sử dụng một hoặc một số dụng cụ làm cỏ vườn, dụng cụ nhà nông và kể cả các loại dụng cụ trong sinh hoạt để diệt cỏ dại.\r\n\r\nNgoài ra, nên lựa chọn các dụng cụ hỗ trợ làm cỏ nhanh chóng, ít tốn công sức và sử dụng lại được nhiều lần như máy cắt cỏ, bình xịt điện… để kiểm soát cỏ dại liên tục, hiệu quả và tiết kiệm nhất.", Quantity = 100, Unit = "Bộ", Image = "https://file.hstatic.net/1000340024/file/dung-cu-lam-co-vuon-03_7ba808d11f1b43c999ee1ac2c2266ba7_grande.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 17, Name = "Bình xịt (phun) chuyên dụng DUDACO - 2 Lít", Description = "Bình tưới chuyên dụng Dudaco là sản phẩm được sản xuất từ nhựa bền, đẹp và tiện lợi. Bình tưới Dudaco có van điều chỉnh tia nước dạng phun sương hoặc dạng tia thẳng với khoảng cách phun lên đến 1,5m. Bình tưới Dudaco dùng để tưới các loại hoa như Hoa lan, Hoa hồng, Hoa cúc,.. và các loại rau trong giai đoạn cây con.", Quantity = 100, Unit = "Cái", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTkgsJxg7IorcEusLn9NMhlV-qTictfX_9Fwg&s", Status = "Active", Type = "Caring" },
                new Item { Id = 18, Name = "Thùng đựng nông sản Nikawa FWA-BA", Description = "Thùng đựng nông sản thường được làm từ carton để bảo quản và vận chuyển. Thùng carton giúp giữ tươi củ quả hay rau và có nhiều kích cỡ khác nhau. Để đảm bảo khả năng chịu lực, đặc biệt khi nông sản có trọng lượng lớn, thùng 5 lớp hoặc 7 lớp thường được sử dụng. Bên cạnh đó, hộp nhựa cũng là một lựa chọn khác để bảo vệ nông sản khỏi các tác động bên ngoài.", Quantity = 100, Unit = "Cái", Image = "https://www.ketnoitieudung.vn/data/bt7/thumb_350/gio-nhua-dung-hang-nikawa-fwa-ba-1689146278.jpg", Status = "Active", Type = "Harvesting" },
                new Item { Id = 19, Name = "Bộ phun sương mini", Description = "Bộ phun sương mini có nhiều loại, bao gồm máy bơm 12V áp lực cao với lưu lượng 8L/phút, thích hợp tưới cây hoặc rửa xe. Máy phun sương Daehan có cục nguồn rời, dễ sử dụng. Một bộ khác có kèm điều chỉnh lượng nước. Giá cả và mẫu mã đa dạng, từ vài chục nghìn đến vài trăm nghìn đồng.", Quantity = 50, Unit = "Cái", Image = "https://vn-live-01.slatic.net/p/0b9e8dea595558b5ab9dda1c73287527.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 20, Name = "Bộ cảm biến đất", Description = "Cảm biến có chức năng phát hiện độ ẩm trong đất hoặc phát hiện nước thường được sử dụng trong các mô hình tưới nước tự động, ngăn chặn tình trạng thiếu nước cho vật thể. Mạch được thiết kế với bề mặt cảm biến bằng kim loại và có độ bền cao, sử dụng đơn giản với ngõ ra cả tín hiệu analog và tín hiệu số tương thích với Arduino và các bộ vi điều khiển.", Quantity = 50, Unit = "Bộ", Image = "https://nshopvn.com/wp-content/uploads/2019/03/cam-bien-do-am-dat-m9bi-3.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 21, Name = "Đèn LED nông nghiệp", Description = "Đèn LED cho cây là một giải pháp chiếu sáng hiệu quả cho việc trồng trọt trong nhà và ngoài trời. Đèn LED giúp kích thích sự phát triển của cây bằng cách cung cấp quang phổ ánh sáng phù hợp cho quá trình quang hợp. Các sản phẩm đèn LED trồng cây phổ biến bao gồm đèn tuýp, đèn panel, và đèn có thể điều chỉnh cường độ ánh sáng. Một số thương hiệu như Rạng Đông cung cấp các loại đèn LED nông nghiệp tiết kiệm điện và dễ dàng lắp đặt. Giá cả của đèn LED trồng cây cũng rất đa dạng, tùy thuộc vào công suất và tính năng.", Quantity = 100, Unit = "Cái", Image = "https://heesun.com.vn/public/media/den_led-nong-nghiep-min.jpg", Status = "Active", Type = "Caring" },
                new Item { Id = 22, Name = "Bao rơm giữ ẩm", Description = "Công dụng trong làm vườn\r\n\r\n- Được dùng để làm ủ phân hữu cơ rất tốt cho cây.\r\n\r\n- Nguyên liệu quan trọng để nuôi trồng nấm rơm.\r\n\r\n- Bảo vệ những quả dâu tây chín khỏi bụi bẩn và dập úng. Và cũng được sử dụng để phủ lên cây trong mùa đông để ngăn cái lạnh.\r\n\r\n- Rơm cũng làm cho một lớp phủ tuyệt vời giữ ẩm cho cây vào mùa khô, chống văng hạt và đất ra khỏi chậu vào mùa mưa.\r\n\r\n- Lót 1 lớp rơm xung quanh cây con mới cấy giúp chống dập lá rau non.\r\n\r\n- Chống lấm bẩn bùn đất lên tường.", Quantity = 100, Unit = "Bao", Image = "https://vuonsaigon.vn/wp-content/uploads/2020/05/rom-cuon-vuon-sai-gon.png", Status = "Active", Type = "Caring" },

                // (Harvesting Task)
                new Item { Id = 5, Name = "Dao thu hoạch", Description = "Dao chuyên dụng để cắt trái cây", Quantity = 100, Unit = "Cái", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMlW2sl4uFDnr7wiHZo420jhTUDtPZmPQkqw&s", Status = "In-stock", Type = "Harvesting" },
                new Item { Id = 6, Name = "Giỏ đựng nông sản", Description = "Giỏ chứa nông sản sau thu hoạch", Quantity = 1000, Unit = "Giỏ", Image = "https://ecohub.vn/wp-content/uploads/2021/08/thung-go-luu-tru-nong-san-do-dung-1.jpg", Status = "In-stock", Type = "Harvesting" },
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

            modelBuilder.Entity<PackagingType>().HasData(
                new PackagingType { Id = 1, Name = "Bao 500g", Description = "Đóng gói theo túi nilon 500g", Status = "Active", QuantityPerPack = 0.5f, PricePerPack = 500f },
                new PackagingType { Id = 2, Name = "Bao 1kg", Description = "Đóng gói theo túi nilon 1kg", Status = "Active", QuantityPerPack = 1f, PricePerPack = 5000f },
                new PackagingType { Id = 3, Name = "Bao 2kg", Description = "Đóng gói theo túi lưới 2kg", Status = "Active", QuantityPerPack = 2f, PricePerPack = 2000f },
                new PackagingType { Id = 4, Name = "Bao 5kg", Description = "Đóng gói theo thùng giấy 5kg", Status = "Active", QuantityPerPack = 5f, PricePerPack = 2000f },
                new PackagingType { Id = 5, Name = "Bao 10kg", Description = "Đóng gói theo bao PP 10kg", Status = "Active", QuantityPerPack = 10f, PricePerPack = 100f },
                new PackagingType { Id = 6, Name = "Bao 15kg", Description = "Đóng gói theo bao PP 15kg", Status = "Active", QuantityPerPack = 15f, PricePerPack = 1000f },
                new PackagingType { Id = 7, Name = "Bao 20kg", Description = "Đóng gói theo thùng 20kg", Status = "Active", QuantityPerPack = 20f, PricePerPack = 500f }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, RetailerId = 1, PlantId = 1, PackagingTypeId = 1, DepositPrice = 500.0f, PreOrderQuantity = 200f, Status = "PendingConfirmation", Address = "123 Đường A, Quận 1, TP.HCM", Phone = "0901234567", EstimatedPickupDate = DateTime.Now.AddDays(3), CreatedAt = DateTime.Now.AddDays(-2) },
                new Order { Id = 2, RetailerId = 2, PlantId = 2, PackagingTypeId = 2, DepositPrice = 750.0f, PreOrderQuantity = 100f, Status = "Deposit", Address = "456 Đường B, Quận 2, TP.HCM", Phone = "0912345678", EstimatedPickupDate = DateTime.Now.AddDays(5), CreatedAt = DateTime.Now.AddDays(-4) },
                new Order { Id = 3, RetailerId = 3, PlantId = 3, PackagingTypeId = 3, DepositPrice = 1000.0f, PreOrderQuantity = 200f, Status = "Paid", Address = "789 Đường C, Quận 3, TP.HCM", Phone = "0923456789", EstimatedPickupDate = DateTime.Now.AddDays(2), CreatedAt = DateTime.Now.AddDays(-6) },
                new Order { Id = 4, RetailerId = 1, PlantId = 4, PackagingTypeId = 4, DepositPrice = 5000.0f, PreOrderQuantity = 200f, Status = "Pending", Address = "321 Đường D, Quận 4, TP.HCM", Phone = "0934567890", EstimatedPickupDate = DateTime.Now.AddDays(7), CreatedAt = DateTime.Now.AddDays(-3) },
                new Order { Id = 5, RetailerId = 2, PlantId = 2, PackagingTypeId = 5, DepositPrice = 7500.0f, PreOrderQuantity = 200f, Status = "Cancel", Address = "654 Đường E, Quận 5, TP.HCM", Phone = "0945678901", EstimatedPickupDate = DateTime.Now.AddDays(10), CreatedAt = DateTime.Now.AddDays(-1) },
                new Order { Id = 6, RetailerId = 1, PlantId = 2, PackagingTypeId = 5, DepositPrice = 7500.0f, PreOrderQuantity = 200f, Status = "Reject", Address = "623 Đường F, Quận 6, TP.HCM", Phone = "0923456789", EstimatedPickupDate = DateTime.Now.AddDays(5), CreatedAt = DateTime.Now.AddDays(-5) },
                new Order { Id = 7, RetailerId = 3, PlantId = 4, PackagingTypeId = 5, DepositPrice = 7500.0f, PreOrderQuantity = 200f, Status = "PendingConfirmation", Address = "6004 Đường G, Quận 7, TP.HCM", Phone = "0945678901", EstimatedPickupDate = DateTime.Now.AddDays(12), CreatedAt = DateTime.Now.AddDays(-2) },
                new Order { Id = 8, RetailerId = 1, PlantId = 3, PackagingTypeId = 5, DepositPrice = 7500.0f, PreOrderQuantity = 200f, Status = "Forfeit", Address = "1234 Đường H, Quận 9, TP.HCM", Phone = "0923456789", EstimatedPickupDate = DateTime.Now.AddDays(11), CreatedAt = DateTime.Now.AddDays(-3) },
                new Order { Id = 9, RetailerId = 2, PlantId = 1, PackagingTypeId = 2, DepositPrice = 4500.0f, PreOrderQuantity = 150f, Status = "Deposit", Address = "900 Đường I, Quận 10, TP.HCM", Phone = "0956789012", EstimatedPickupDate = DateTime.Now.AddDays(4), CreatedAt = DateTime.Now.AddDays(-2) },
                new Order { Id = 10, RetailerId = 3, PlantId = 4, PackagingTypeId = 1, DepositPrice = 8600.0f, PreOrderQuantity = 300f, Status = "Paid", Address = "567 Đường J, Quận Bình Thạnh, TP.HCM", Phone = "0967890123", EstimatedPickupDate = DateTime.Now.AddDays(8), CreatedAt = DateTime.Now.AddDays(-7) }
            );

            modelBuilder.Entity<PackagingProduct>().HasData(
                new PackagingProduct { Id = 1, PackagingTaskId = 1, HarvestingTaskId = 1, QRCode = "QR_001", Status = "Active", QuantityPerPack = 100, PackQuantity = 2 },
                new PackagingProduct { Id = 2, PackagingTaskId = 2, HarvestingTaskId = 2, QRCode = "QR_002", Status = "Active", QuantityPerPack = 10, PackQuantity = 5 },
                new PackagingProduct { Id = 3, PackagingTaskId = 3, HarvestingTaskId = 3, QRCode = "QR_003", Status = "Expired", QuantityPerPack = 20, PackQuantity = 1 },
                new PackagingProduct { Id = 4, PackagingTaskId = 4, HarvestingTaskId = 4, QRCode = "QR_004", Status = "Active", QuantityPerPack = 50, PackQuantity = 2 },
                new PackagingProduct { Id = 5, PackagingTaskId = 5, HarvestingTaskId = 5, QRCode = "QR_005", Status = "Active", QuantityPerPack = 20, PackQuantity = 3 }
            );

            modelBuilder.Entity<NotificationExpert>().HasData(
                new NotificationExpert
                {
                    Id = 1,
                    ExpertId = 1,
                    Title = "Cập nhật về dịch bệnh cây trồng",
                    Message = "Xuất hiện sâu keo mùa thu tại một số khu vực miền Tây, chuyên gia cần lưu ý.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationExpert
                {
                    Id = 2,
                    ExpertId = 2,
                    Title = "Thông tin thị trường nông sản",
                    Message = "Giá lúa gạo tăng mạnh do nhu cầu xuất khẩu tăng cao trong tháng này.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationExpert
                {
                    Id = 3,
                    ExpertId = 3,
                    Title = "Ứng dụng công nghệ mới trong nông nghiệp",
                    Message = "Hệ thống tưới nhỏ giọt giúp tiết kiệm 30% lượng nước và tăng năng suất 20%.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationExpert
                {
                    Id = 4,
                    ExpertId = 1,
                    Title = "Hội thảo trực tuyến về canh tác hữu cơ",
                    Message = "Mời chuyên gia tham gia hội thảo về nông nghiệp hữu cơ vào ngày 25/07/2024.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<NotificationOwner>().HasData(
                new NotificationOwner
                {
                    Id = 1,
                    OwnerId = 201,
                    Title = "Cảnh báo thời tiết cho trang trại",
                    Message = "Dự báo có mưa lớn trong tuần này, chủ trang trại cần có biện pháp bảo vệ cây trồng.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationOwner
                {
                    Id = 2,
                    OwnerId = 202,
                    Title = "Giá cả phân bón cập nhật",
                    Message = "Giá phân bón hữu cơ đang giảm, đây là thời điểm tốt để mua dự trữ.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationOwner
                {
                    Id = 3,
                    OwnerId = 203,
                    Title = "Chương trình hỗ trợ vay vốn",
                    Message = "Ngân hàng vừa triển khai gói hỗ trợ vay vốn dành cho chủ trang trại với lãi suất ưu đãi.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationOwner
                {
                    Id = 4,
                    OwnerId = 204,
                    Title = "Hội nghị nông nghiệp bền vững",
                    Message = "Mời chủ trang trại tham gia hội nghị về mô hình canh tác bền vững vào ngày 10/08/2024.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<NotificationFarmer>().HasData(
                new NotificationFarmer
                {
                    Id = 1,
                    FarmerId = 1,
                    Title = "Cảnh báo sâu bệnh hại lúa",
                    Message = "Phát hiện sâu cuốn lá tại một số khu vực, nông dân cần kiểm tra ruộng lúa.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationFarmer
                {
                    Id = 2,
                    FarmerId = 1,
                    Title = "Hỗ trợ giống cây trồng",
                    Message = "Chương trình hỗ trợ giống lúa năng suất cao đang mở đăng ký.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationFarmer
                {
                    Id = 3,
                    FarmerId = 1,
                    Title = "Kỹ thuật bón phân hiệu quả",
                    Message = "Áp dụng đúng kỹ thuật bón phân giúp tăng năng suất và giảm chi phí.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationFarmer
                {
                    Id = 4,
                    FarmerId = 2,
                    Title = "Hội thảo trồng trọt hữu cơ",
                    Message = "Mời nông dân tham gia hội thảo về phương pháp canh tác hữu cơ vào ngày 15/08/2024.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<NotificationRetailer>().HasData(
                new NotificationRetailer
                {
                    Id = 1,
                    RetailerId = 1,
                    Title = "Cập nhật giá nông sản hôm nay",
                    Message = "Giá rau xanh đang tăng nhẹ do ảnh hưởng của thời tiết. Xem chi tiết tại bảng giá.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationRetailer
                {
                    Id = 2,
                    RetailerId = 2,
                    Title = "Chương trình khuyến mãi dành cho nhà bán lẻ",
                    Message = "Nhận chiết khấu 10% khi đặt hàng số lượng lớn trong tháng này.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationRetailer
                {
                    Id = 3,
                    RetailerId = 3,
                    Title = "Nguồn hàng mới từ các trang trại sạch",
                    Message = "Chúng tôi vừa nhập thêm lô hàng rau củ hữu cơ từ các trang trại đạt chuẩn VietGAP.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                },
                new NotificationRetailer
                {
                    Id = 4,
                    RetailerId = 1,
                    Title = "Cảnh báo vận chuyển do thời tiết",
                    Message = "Một số tuyến đường giao hàng có thể bị chậm do ảnh hưởng của mưa bão.",
                    IsRead = false,
                    CreatedDate = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<NotificationInspector>().HasData(
                new NotificationInspector
                {
                    Id = 1,
                    InspectorId = 1,
                    Title = "Yêu cầu kiểm định mới",
                    Message = "Bạn có một yêu cầu kiểm định mới cho mẫu nông sản: Xoài Cát Chu - Long An.",
                    IsRead = false,
                    CreatedDate = DateTime.Now.AddDays(-2)
                },
                new NotificationInspector
                {
                    Id = 2,
                    InspectorId = 2,
                    Title = "Cập nhật hệ thống",
                    Message = "Hệ thống kiểm định đã được cập nhật phiên bản mới với nhiều tính năng hỗ trợ phân tích mẫu.",
                    IsRead = false,
                    CreatedDate = DateTime.Now.AddDays(-5)
                },
                new NotificationInspector
                {
                    Id = 3,
                    InspectorId = 1,
                    Title = "Mẫu kiểm định cần bổ sung thông tin",
                    Message = "Mẫu kiểm định Lúa ST25 cần bạn bổ sung ghi chú về độ ẩm.",
                    IsRead = false,
                    CreatedDate = DateTime.Now.AddDays(-1)
                },
                new NotificationInspector
                {
                    Id = 4,
                    InspectorId = 2,
                    Title = "Gửi kết quả kiểm định thành công",
                    Message = "Bạn đã gửi kết quả kiểm định cho mẫu Cà phê Arabica - Lâm Đồng thành công.",
                    IsRead = false,
                    CreatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Specialization>().HasData(
                // Caring Tasks
                new Specialization { Id = 1, Name = "Trồng rau sạch" },
                new Specialization { Id = 2, Name = "Trồng cây ăn quả" },
                new Specialization { Id = 3, Name = "Trồng lúa hữu cơ" },
                new Specialization { Id = 4, Name = "Trồng hoa màu" },
                new Specialization { Id = 5, Name = "Sử dụng phân bón hữu cơ" },
                new Specialization { Id = 6, Name = "Ứng dụng IoT trong nông nghiệp" },
                new Specialization { Id = 7, Name = "Tưới tiêu tự động" },

                // Harvesting tasks
                new Specialization { Id = 8, Name = "Thu hoạch thủ công" },
                new Specialization { Id = 9, Name = "Thu hoạch bằng máy móc" },
                new Specialization { Id = 10, Name = "Phân loại sản phẩm sau thu hoạch" },
                new Specialization { Id = 11, Name = "Bảo quản sản phẩm sau thu hoạch" },

                // Packaging Tasks
                new Specialization { Id = 12, Name = "Đóng gói theo tiêu chuẩn VietGAP" },
                new Specialization { Id = 13, Name = "Đóng gói xuất khẩu" },
                new Specialization { Id = 14, Name = "Gắn nhãn truy xuất nguồn gốc" },
                new Specialization { Id = 15, Name = "Xử lý sản phẩm trước khi đóng gói" },
                new Specialization { Id = 16, Name = "Bảo quản lạnh" }
            );

            modelBuilder.Entity<FarmerSpecialization>().HasData(
                new FarmerSpecialization { FarmerId = 1, SpecializationId = 1 },
                new FarmerSpecialization { FarmerId = 1, SpecializationId = 4 },
                new FarmerSpecialization { FarmerId = 1, SpecializationId = 7 },
                new FarmerSpecialization { FarmerId = 1, SpecializationId = 8 },
            
                new FarmerSpecialization { FarmerId = 2, SpecializationId = 2 },
                new FarmerSpecialization { FarmerId = 2, SpecializationId = 5 },
                new FarmerSpecialization { FarmerId = 2, SpecializationId = 12 },
                new FarmerSpecialization { FarmerId = 2, SpecializationId = 14 },
            
                new FarmerSpecialization { FarmerId = 3, SpecializationId = 3 },
                new FarmerSpecialization { FarmerId = 3, SpecializationId = 9 },
                new FarmerSpecialization { FarmerId = 3, SpecializationId = 10 },
                new FarmerSpecialization { FarmerId = 3, SpecializationId = 11 },
            
                new FarmerSpecialization { FarmerId = 4, SpecializationId = 1 },
                new FarmerSpecialization { FarmerId = 4, SpecializationId = 6 },
                new FarmerSpecialization { FarmerId = 4, SpecializationId = 13 },
                new FarmerSpecialization { FarmerId = 4, SpecializationId = 15 },
            
                new FarmerSpecialization { FarmerId = 5, SpecializationId = 2 },
                new FarmerSpecialization { FarmerId = 5, SpecializationId = 10 },
                new FarmerSpecialization { FarmerId = 5, SpecializationId = 11 },
                new FarmerSpecialization { FarmerId = 5, SpecializationId = 16 },
            
                new FarmerSpecialization { FarmerId = 6, SpecializationId = 6 },
                new FarmerSpecialization { FarmerId = 6, SpecializationId = 7 },
                new FarmerSpecialization { FarmerId = 6, SpecializationId = 14 },
                new FarmerSpecialization { FarmerId = 6, SpecializationId = 15 }
            );

            modelBuilder.Entity<PlantYield>().HasData(
                new PlantYield { PlantId = 1, YieldId = 1 },
                new PlantYield { PlantId = 1, YieldId = 2 },
                new PlantYield { PlantId = 1, YieldId = 3 },
                new PlantYield { PlantId = 2, YieldId = 1 },
                new PlantYield { PlantId = 2, YieldId = 4 },
                new PlantYield { PlantId = 2, YieldId = 5 },
                new PlantYield { PlantId = 3, YieldId = 6 },
                new PlantYield { PlantId = 3, YieldId = 1 },
                new PlantYield { PlantId = 3, YieldId = 5 },
                new PlantYield { PlantId = 3, YieldId = 7 },
                new PlantYield { PlantId = 4, YieldId = 4 },
                new PlantYield { PlantId = 4, YieldId = 2 },
                new PlantYield { PlantId = 4, YieldId = 7 },
                new PlantYield { PlantId = 5, YieldId = 5 },
                new PlantYield { PlantId = 6, YieldId = 6 },
                new PlantYield { PlantId = 7, YieldId = 7 },
                new PlantYield { PlantId = 8, YieldId = 3 },
                new PlantYield { PlantId = 9, YieldId = 1 },
                new PlantYield { PlantId = 10, YieldId = 2 }
            );

            modelBuilder.Entity<PlanTransaction>().HasData(
                new PlanTransaction { Id = 2, UrlAddress = "0xea28dee1c7cf4dd2fd216ff9cda37a0aae86da13" }    
            );
        }
    }
}