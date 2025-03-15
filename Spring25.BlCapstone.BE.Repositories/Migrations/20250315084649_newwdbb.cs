using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spring25.BlCapstone.BE.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class newwdbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fertilizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fertilizer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationOwner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackagingType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pesticide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesticide", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasePrice = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeltaOne = table.Column<float>(type: "real", nullable: false),
                    DeltaTwo = table.Column<float>(type: "real", nullable: false),
                    DeltaThree = table.Column<float>(type: "real", nullable: false),
                    PreservationDay = table.Column<int>(type: "int", nullable: false),
                    EstimatedPerOne = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yield",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yield", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expert_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Farmer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farmer_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inspector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hotline = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspector_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Retailer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: true),
                    Latitude = table.Column<float>(type: "real", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retailer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retailer_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DataEnvironment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YieldId = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    YieldHumidity = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataEnvironment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataEnvironment_Yield_YieldId",
                        column: x => x.YieldId,
                        principalTable: "Yield",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YieldId = table.Column<int>(type: "int", nullable: true),
                    DeviceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_Yield_YieldId",
                        column: x => x.YieldId,
                        principalTable: "Yield",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlantYield",
                columns: table => new
                {
                    YieldId = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantYield", x => new { x.PlantId, x.YieldId });
                    table.ForeignKey(
                        name: "FK_PlantYield_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlantYield_Yield_YieldId",
                        column: x => x.YieldId,
                        principalTable: "Yield",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationExpert",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationExpert", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationExpert_Expert_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Expert",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    YieldId = table.Column<int>(type: "int", nullable: true),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedProduct = table.Column<float>(type: "real", nullable: true),
                    EstimatedUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeedQuantity = table.Column<int>(type: "int", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plan_Expert_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Expert",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plan_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plan_Yield_YieldId",
                        column: x => x.YieldId,
                        principalTable: "Yield",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationFarmer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationFarmer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationFarmer_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationRetailer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailerId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRetailer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationRetailer_Retailer_RetailerId",
                        column: x => x.RetailerId,
                        principalTable: "Retailer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FarmerPermission",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerPermission", x => new { x.PlanId, x.FarmerId });
                    table.ForeignKey(
                        name: "FK_FarmerPermission_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FarmerPermission_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HarvestingTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HarvestedQuantity = table.Column<float>(type: "real", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailQuantity = table.Column<float>(type: "real", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestingTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarvestingTask_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectingForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    InspectorId = table.Column<int>(type: "int", nullable: true),
                    FormName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfSample = table.Column<int>(type: "int", nullable: true),
                    SampleWeight = table.Column<float>(type: "real", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CanHarvest = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectingForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectingForm_Inspector_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspector",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectingForm_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailerId = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PackagingTypeId = table.Column<int>(type: "int", nullable: false),
                    DepositPrice = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedPickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_PackagingType_PackagingTypeId",
                        column: x => x.PackagingTypeId,
                        principalTable: "PackagingType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Retailer_RetailerId",
                        column: x => x.RetailerId,
                        principalTable: "Retailer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackagingTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PackagingTypeId = table.Column<int>(type: "int", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackedQuantity = table.Column<int>(type: "int", nullable: true),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagingTask_PackagingType_PackagingTypeId",
                        column: x => x.PackagingTypeId,
                        principalTable: "PackagingType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackagingTask_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    ProblemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problem_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Problem_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FarmerHarvestingTask",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerHarvestingTask", x => new { x.FarmerId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_FarmerHarvestingTask_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FarmerHarvestingTask_HarvestingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "HarvestingTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HarvestingImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarvestingImage_HarvestingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "HarvestingTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HarvestingItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarvestingItem_HarvestingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "HarvestingTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HarvestingItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectingResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    Arsen = table.Column<float>(type: "real", nullable: false),
                    Plumbum = table.Column<float>(type: "real", nullable: false),
                    Cadmi = table.Column<float>(type: "real", nullable: false),
                    Hydrargyrum = table.Column<float>(type: "real", nullable: false),
                    Salmonella = table.Column<float>(type: "real", nullable: false),
                    Coliforms = table.Column<float>(type: "real", nullable: false),
                    Ecoli = table.Column<float>(type: "real", nullable: false),
                    Glyphosate_Glufosinate = table.Column<float>(type: "real", nullable: false),
                    SulfurDioxide = table.Column<float>(type: "real", nullable: false),
                    MethylBromide = table.Column<float>(type: "real", nullable: false),
                    HydrogenPhosphide = table.Column<float>(type: "real", nullable: false),
                    Dithiocarbamate = table.Column<float>(type: "real", nullable: false),
                    Nitrat = table.Column<float>(type: "real", nullable: false),
                    NaNO3_KNO3 = table.Column<float>(type: "real", nullable: false),
                    Chlorate = table.Column<float>(type: "real", nullable: false),
                    Perchlorate = table.Column<float>(type: "real", nullable: false),
                    EvaluatedResult = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectingResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectingResult_InspectingForm_FormId",
                        column: x => x.FormId,
                        principalTable: "InspectingForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FarmerPackagingTask",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerPackagingTask", x => new { x.FarmerId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_FarmerPackagingTask_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FarmerPackagingTask_PackagingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "PackagingTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackagingImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagingImage_PackagingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "PackagingTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackagingItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagingItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackagingItem_PackagingTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "PackagingTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackagingProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackagingTaskId = table.Column<int>(type: "int", nullable: false),
                    HarvestingTaskId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PackagingQuantity = table.Column<int>(type: "int", nullable: false),
                    PackagingUnit = table.Column<int>(type: "int", nullable: false),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackagingProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackagingProduct_HarvestingTask_HarvestingTaskId",
                        column: x => x.HarvestingTaskId,
                        principalTable: "HarvestingTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackagingProduct_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackagingProduct_PackagingTask_PackagingTaskId",
                        column: x => x.PackagingTaskId,
                        principalTable: "PackagingTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaringTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaringTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaringTask_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaringTask_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProblemImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemImage_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectingImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectingImage_InspectingResult_ResultId",
                        column: x => x.ResultId,
                        principalTable: "InspectingResult",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaringFertilizer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FertilizerId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaringFertilizer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaringFertilizer_CaringTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CaringTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaringFertilizer_Fertilizer_FertilizerId",
                        column: x => x.FertilizerId,
                        principalTable: "Fertilizer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaringImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaringImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaringImage_CaringTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CaringTask",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaringItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaringItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaringItem_CaringTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CaringTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaringItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaringPesticide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PesticideId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaringPesticide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaringPesticide_CaringTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CaringTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaringPesticide_Pesticide_PesticideId",
                        column: x => x.PesticideId,
                        principalTable: "Pesticide",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FarmerCaringTask",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerCaringTask", x => new { x.FarmerId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_FarmerCaringTask_CaringTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "CaringTask",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FarmerCaringTask_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "Name", "Password", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6949), "nguyenvana@gmail.com", true, "Eurofins Scientific", "123", "Inspector", null },
                    { 2, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6952), "farmer@gmail.com", true, "Trần Thị B", "1@", "Farmer", null },
                    { 3, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6954), "lequangc@gmail.com", true, "Lê Quang C", "123", "Expert", null },
                    { 4, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6955), "phaminhd@gmail.com", true, "Phạm Minh D", "123", "Farmer", null },
                    { 5, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6957), "tramnbp@gmail.com", true, "Nguyễn Bình Phương Trâm", "123", "Farmer", null },
                    { 6, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6958), "nguyenthienf@gmail.com", true, "Nguyễn Thiện F", "123", "Farmer", null },
                    { 7, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6959), "tranbichg@gmail.com", true, "Trần Bích G", "1234", "Farmer", null },
                    { 8, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6960), "inspector@gmail.com", true, "atvstp - TCCL VIETNAM", "1@", "Inspector", null },
                    { 9, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6962), "phamtuani@gmail.com", true, "Phạm Tuan I", "123", "Expert", null },
                    { 10, new DateTime(2025, 3, 15, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(6963), "expert@gmail.com", true, "Hoàng Quỳnh J", "1@", "Expert", null },
                    { 11, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(6971), "farmowner@gmail.com", true, "Trịnh Xuân Admin", "1@", "Farm Owner", null },
                    { 12, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(6982), "retailer@gmail.com", true, "Trịnh Hữu Tuấn", "1@", "Retailer", null },
                    { 13, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(6983), "khanhvhd@gmail.com", true, "Vũ Hoàng Duy Khánh", "1@", "Retailer", null },
                    { 14, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(6984), "khanhlq@gmail.com", true, "Lê Quốc Khánh", "1@", "Retailer", null },
                    { 15, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(6985), "binhbeopro1122@gmail.com", true, "Xoo Binf", "1@", "Farmer", null }
                });

            migrationBuilder.InsertData(
                table: "Fertilizer",
                columns: new[] { "Id", "Description", "Image", "Name", "Quantity", "Status", "Type", "Unit" },
                values: new object[,]
                {
                    { 1, "Apply 100g per square meter every 2 weeks. Suitable for vegetables.", "https://happyagri.com.vn/storage/jf/u8/jfu8y6304pvhe0zuxf3o3dwegwna_phan-bon-la-abaxton-hieu-ocenum-organic-plus.webp", "Organic Plus", 50f, "Available", "Organic", "kg" },
                    { 2, "Use 50g per plant every month. Mix with water before applying.", "https://jvf.com.vn/vnt_upload/product/05_2019/Hinhbaobi/g1a_mat_truoc.jpg", "NPK 16-16-8", 100f, "Available", "Chemical", "kg" },
                    { 3, "Dissolve 5ml in 1 liter of water. Spray on leaves weekly.", "https://nongnghiephoangphuc.com/thumbs/1600x1600x2/upload/product/hm-99-moi-2058.png", "Humic Acid", 30f, "Out of Stock", "Organic", "liters" },
                    { 4, "Apply 20g per square meter before planting. Improves root growth.", "https://cdn.mos.cms.futurecdn.net/rRaQV8Td8U78mUXoeaA2j7.jpg", "Super Phosphate", 80f, "Available", "Mineral", "kg" },
                    { 5, "Use 30g per tree during flowering season. Helps in fruit development.", "https://hoachatthinghiem.org/wp-content/uploads/2022/10/Potassium-Sulphate-DUKSAN.jpg", "Potassium Sulfate", 60f, "Limited Stock", "Chemical", "kg" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Description", "Image", "Name", "Quantity", "Status", "Type", "Unit" },
                values: new object[,]
                {
                    { 1, "Bình tưới nước dung tích 5L", "https://product.hstatic.net/200000199113/product/7220965_7709e84c2e3f40cf8111c44225c96646_large.jpg", "Bình tưới cây", 100, "Active", "Caring", "Cái" },
                    { 2, "Kéo chuyên dụng để cắt tỉa cành", "https://fact-depot.com/tmp/cache/images/_thumbs/720x720/media/product/30542/Keo-cat-tia-cong-vien-cay-xanh-HM044-cat-tia-co-la.png", "Kéo cắt tỉa", 100, "Active", "Caring", "Cái" },
                    { 3, "Dụng cụ bón phân dạng viên", "https://vn-live-01.slatic.net/p/eab87be47ffa092ca1becbc00ff06ed2.jpg", "Bón phân hữu cơ", 100, "In-stock", "Caring", "Hộp" },
                    { 4, "Dụng cụ đo độ ẩm của đất", "https://thbvn.com/cdn/images/may-do-do-am/dung-cu-do-do-am-dat-tot-1.jpg", "Máy đo độ ẩm đất", 100, "Active", "Caring", "Cái" },
                    { 5, "Dao chuyên dụng để cắt trái cây", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMlW2sl4uFDnr7wiHZo420jhTUDtPZmPQkqw&s", "Dao thu hoạch", 100, "In-stock", "Harvesting", "Cái" },
                    { 6, "Giỏ chứa nông sản sau thu hoạch", "https://ecohub.vn/wp-content/uploads/2021/08/thung-go-luu-tru-nong-san-do-dung-1.jpg", "Giỏ đựng nông sản", 100, "In-stock", "Harvesting", "Giỏ" },
                    { 7, "Máy gặt đập liên hợp mini", "https://mayxaydungmlk.vn/wp-content/uploads/2022/07/may-gat-DC60.jpg", "Máy cắt lúa", 100, "In-stock", "Harvesting", "Máy" },
                    { 8, "Máy đóng gói tốc độ cao cho nông sản.", "https://dienmayviteko.com/pic/Product/VPM-BZJ600-4_1029_HasThumb.webp", "Máy đóng gói tự động", 5, "Active", "Packaging", "Máy" },
                    { 9, "Thiết bị bảo quản sản phẩm bằng cách hút chân không.", "https://dbk.vn/uploads/ckfinder/images/may-hut-chan-khong/may-hut-chan-khong-cong-nghiep-Magic-Air-MZ600.jpg", "Máy hút chân không", 8, "Active", "Packaging", "Máy" },
                    { 10, "Cân chính xác dùng trong quy trình đóng gói.", "https://cokhitanminh.com/may-dong-goi/wp-content/uploads/2021/11/may-dong-goi-can-dien-tu-3-bien-tmdg-2f14-ckmdg-1.jpg", "Cân điện tử đóng gói", 10, "Active", "Packaging", "Máy" },
                    { 11, "Máy đóng gói túi ni lông hoặc túi giấy cho sản phẩm nông nghiệp.", "https://cnva.vn/wp-content/uploads/2024/02/may-dong-goi-tui-roi-tui-zip.jpg", "Máy đóng gói túi", 7, "In-stock", "Packaging", "Máy" },
                    { 12, "Dây chuyền đóng gói tự động hỗ trợ quy trình sản xuất.", "https://mayduoctiendat.com/upload/filemanager/files/day-chuyen-dong-goi-bot-hop-thiec-tu-dong.jpg", "Dây chuyền đóng gói", 2, "Out-stock", "Packaging", "Dây" }
                });

            migrationBuilder.InsertData(
                table: "Pesticide",
                columns: new[] { "Id", "Description", "Image", "Name", "Quantity", "Status", "Type", "Unit" },
                values: new object[,]
                {
                    { 1, "Mix 5ml with 1 liter of water. Spray on plants every 7 days.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCFbvgQnnXLUDetHBrkklU5PV1fsamB8Yt2Q&s", "Neem Oil", 20f, "Available", "Organic", "liters" },
                    { 2, "Dilute 10ml in 1 liter of water. Use in the evening for best results.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyaA3fAHWe-2ncRTLEDdF5yQMCeXNlLWGW0w&s", "Pyrethrin", 15f, "Limited Stock", "Chemical", "liters" },
                    { 3, "Apply 5g per square meter. Helps prevent fungal diseases.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSly88nvIfyDWzSM0aNglb4NYlimIHfZQ7KDA&s", "Sulfur Dust", 50f, "Out of Stock", "Mineral", "kg" },
                    { 4, "Use 2ml per liter of water. Effective against caterpillars and thrips.", "https://image.made-in-china.com/2f0j00rvSoQMsPbDbU/Spinosad-45-Sc-Agriculture-Insecticide-Agro-Chemicals.webp", "Spinosad", 10f, "Available", "Biological", "liters" },
                    { 5, "Dissolve 1g in 1 liter of water. Spray on leaves to prevent blight.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTg9gWe6ZUxCxCQu_p89Run1nw2zS6PyjafCw&s", "Copper Fungicide", 25f, "Available", "Mineral", "kg" }
                });

            migrationBuilder.InsertData(
                table: "Plant",
                columns: new[] { "Id", "BasePrice", "DeltaOne", "DeltaThree", "DeltaTwo", "Description", "EstimatedPerOne", "ImageUrl", "PlantName", "PreservationDay", "Quantity", "Status", "Type" },
                values: new object[,]
                {
                    { 1, 15000f, 1.2f, 1.1f, 0.8f, "Loại rau dễ trồng, phát triển nhanh, giàu dinh dưỡng.", 1.5f, "https://thanhnien.mediacdn.vn/Uploaded/camlt/2022_09_08/anh-chup-man-hinh-2022-09-08-luc-155118-6476.png", "Rau muống", 5, 100f, "Available", "Rau lá" },
                    { 2, 18000f, 1.3f, 1f, 0.7f, "Loại rau cải phổ biến, dễ trồng, thu hoạch nhanh.", 1.2f, "https://fresco.vn/public/upload/product/cai-ngot-thuy-canh-hsxNzHwGZn.jpg", "Cải ngọt", 7, 80f, "Available", "Rau lá" },
                    { 3, 20000f, 1.5f, 1.2f, 0.9f, "Rau ăn sống, dễ trồng, nhanh thu hoạch.", 1.3f, "https://www.cleanipedia.com/images/5iwkm8ckyw6v/6fcJ3CHTOLA35rCtQEQixF/fb1c276fe0c96d6922838248949b96a4/eGEtbGFjaC5qcGVn/1200w/rau-di%E1%BA%BFp-c%C3%A1-%C4%91%E1%BA%B7t-trong-gi%E1%BB%8F-m%C3%A2y%2C-n%E1%BB%81n-tr%E1%BA%AFng..jpg", "Xà lách", 6, 90f, "Available", "Rau lá" },
                    { 4, 25000f, 1.4f, 1.3f, 0.9f, "Gia vị phổ biến, dễ trồng, thu hoạch nhanh.", 1f, "https://www.cleanipedia.com/images/5iwkm8ckyw6v/6fcJ3CHTOLA35rCtQEQixF/fb1c276fe0c96d6922838248949b96a4/eGEtbGFjaC5qcGVn/1200w/rau-di%E1%BA%BFp-c%C3%A1-%C4%91%E1%BA%B7t-trong-gi%E1%BB%8F-m%C3%A2y%2C-n%E1%BB%81n-tr%E1%BA%AFng..jpg", "Hành lá", 10, 70f, "Available", "Gia vị" },
                    { 5, 16000f, 1.3f, 1.1f, 0.8f, "Rau leo, phát triển nhanh, thích hợp trồng mùa hè.", 1.4f, "https://hatgiongphuongnam.com/asset/upload/image/hat-giong-rau-mong-toi-1.8_.png?v=20190410", "Mồng tơi", 5, 85f, "Available", "Rau lá" },
                    { 6, 22000f, 1.6f, 1.2f, 1f, "Rau giàu dinh dưỡng, tốt cho sức khỏe.", 1.1f, "https://product.hstatic.net/200000423303/product/cai-bo-xoi-huu-co_dcef0c0e1fc1491599583cc06a19b830.jpg", "Cải bó xôi", 6, 75f, "Available", "Rau lá" },
                    { 7, 20000f, 1.4f, 1.3f, 0.9f, "Loại củ phát triển nhanh, giàu dinh dưỡng.", 1.5f, "https://dalafood.vn/wp-content/uploads/2022/06/cu-cai-trang.jpg", "Củ cải trắng", 12, 60f, "Available", "Củ" },
                    { 8, 19000f, 1.2f, 1f, 0.8f, "Rau quả dễ trồng, thu hoạch nhanh.", 1.2f, "https://bizweb.dktcdn.net/100/390/808/products/dau-bap-huu-co-500x500.jpg?v=1600504946570", "Đậu bắp", 8, 65f, "Available", "Quả" },
                    { 9, 18000f, 1.5f, 1.2f, 0.9f, "Rau quả dễ trồng, thu hoạch nhanh, giàu nước.", 1.3f, "https://hoayeuthuong.com/hinh-hoa-tuoi/moingay/11896_dua-leo-lon-kg.jpg", "Dưa leo", 7, 78f, "Available", "Quả" },
                    { 10, 25000f, 1.6f, 1.3f, 1f, "Loại quả nhiều vitamin, dễ trồng, nhanh thu hoạch.", 1.4f, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSQHrOrbNd1JxfpvcHVUqe5bklaBHYxon-Qew&s", "Cà chua", 10, 72f, "Available", "Quả" }
                });

            migrationBuilder.InsertData(
                table: "Yield",
                columns: new[] { "Id", "Area", "AreaUnit", "Description", "Status", "Type", "YieldName" },
                values: new object[,]
                {
                    { 1, 500f, "m2", "Đất hữu cơ màu mỡ", "Available", "Đất hữu cơ", "Trang trại A" },
                    { 2, 300f, "m2", "Đất chua cần cải tạo", "Maintenance", "Đất chua", "Nông trại B" },
                    { 3, 800f, "m2", "Đất phèn nhẹ, thích hợp trồng lúa", "Available", "Đất phèn", "Ruộng C" },
                    { 4, 450f, "m2", "Đất đen màu mỡ", "Available", "Đất đen", "Nông trại D" },
                    { 5, 600f, "m2", "Đất xám, thoát nước tốt", "Available", "Đất xám", "Trang trại E" },
                    { 6, 350f, "m2", "Đất cát pha, cần giữ ẩm tốt", "In-Use", "Đất cát", "Khu vực F" },
                    { 7, 400f, "m2", "Đất đỏ bazan giàu dinh dưỡng", "Available", "Đất đỏ", "Vườn G" }
                });

            migrationBuilder.InsertData(
                table: "Device",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeviceCode", "Name", "Status", "UpdatedAt", "UpdatedBy", "YieldId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8365), "Farm Owner", "TEMP-001", "Cảm biến nhiệt độ", "Active", null, null, 1 },
                    { 2, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8368), "Farm Owner", "MOIST-002", "Cảm biến độ ẩm đất", "Active", null, null, 1 },
                    { 3, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8369), "Farm Owner", "LIGHT-003", "Cảm biến ánh sáng", "Active", null, null, 2 },
                    { 4, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8371), "Farm Owner", "PH-004", "Cảm biến pH đất", "Inactive", null, null, 2 },
                    { 5, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8372), "Farm Owner", "HUM-005", "Cảm biến độ ẩm không khí", "Active", null, null, 3 },
                    { 6, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8374), "Farm Owner", "CO2-006", "Cảm biến CO2", "Active", null, null, 3 },
                    { 7, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8375), "Farm Owner", "SALIN-007", "Cảm biến độ mặn", "Inactive", null, null, 4 },
                    { 8, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8376), "Farm Owner", "WIND-008", "Cảm biến gió", "Active", null, null, 5 },
                    { 9, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8378), "Farm Owner", "RAIN-009", "Cảm biến lượng mưa", "Active", null, null, 6 },
                    { 10, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8379), "Farm Owner", "NITRO-010", "Cảm biến Nitrogen", "Error", null, null, 7 }
                });

            migrationBuilder.InsertData(
                table: "Expert",
                columns: new[] { "Id", "AccountId", "Avatar", "DOB", "Phone" },
                values: new object[,]
                {
                    { 1, 3, "https://images.unsplash.com/photo-1531384441138-2736e62e0919?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1985, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678" },
                    { 2, 9, "https://images.unsplash.com/photo-1531901599143-df5010ab9438?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1990, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321" },
                    { 3, 10, "https://images.unsplash.com/photo-1531123897727-8f129e1688ce?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1995, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0971122334" }
                });

            migrationBuilder.InsertData(
                table: "Farmer",
                columns: new[] { "Id", "AccountId", "Avatar", "DOB", "Phone" },
                values: new object[,]
                {
                    { 1, 2, "https://plus.unsplash.com/premium_photo-1686269460470-a44c06f16e0a?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1980, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0901234567" },
                    { 2, 4, "https://images.unsplash.com/photo-1589923188900-85dae523342b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1985, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678" },
                    { 3, 5, "https://images.unsplash.com/photo-1593011951342-8426e949371f?q=80&w=1944&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1990, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "0923456789" },
                    { 4, 6, "https://images.unsplash.com/photo-1545830790-68595959c491?q=80&w=1935&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(1995, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0934567890" },
                    { 5, 7, "https://plus.unsplash.com/premium_photo-1661411325413-98a5ff88e8e4?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "0945678901" },
                    { 6, 15, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRGdR1EiC3BMaU8EUeRTp7Vo8oqhfLkySpTsw&s", new DateTime(2003, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "0838097512" }
                });

            migrationBuilder.InsertData(
                table: "Inspector",
                columns: new[] { "Id", "AccountId", "Address", "Description", "Hotline", "ImageUrl" },
                values: new object[,]
                {
                    { 1, 1, "123 Green Farm Road, Hanoi", "Experienced agricultural inspector with 10 years in the field.", "0123456789", "https://static.ybox.vn/2024/6/0/1719750238636-eurofins_1200x628.jpg" },
                    { 2, 8, "456 Eco Farm Lane, Ho Chi Minh City", "Expert in organic certification and food safety.", "0987654321", "https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/dataimages/202304/original/images2525186_35b.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Retailer",
                columns: new[] { "Id", "AccountId", "Address", "Avatar", "DOB", "Latitude", "Longitude", "Phone" },
                values: new object[,]
                {
                    { 1, 12, "123 Đường Lê Lợi, Quận 1, TP.HCM", "https://nationaltoday.com/wp-content/uploads/2022/05/91-Retailer.jpg", new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 106.7009f, 10.7769f, "0901234567" },
                    { 2, 13, "456 Đường Nguyễn Huệ, Quận 1, TP.HCM", "https://thumbs.dreamstime.com/b/hardware-store-worker-smiling-african-standing-fasteners-aisle-41251157.jpg", new DateTime(1990, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 106.6822f, 10.7627f, "0912345678" },
                    { 3, 14, "789 Đường Phạm Văn Đồng, Quận Thủ Đức, TP.HCM", "https://www.kofastudy.com/kike_content/uploads/2021/01/e-Commerce-Today.jpg", new DateTime(1995, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 106.6297f, 10.8231f, "0923456789" }
                });

            migrationBuilder.InsertData(
                table: "Plan",
                columns: new[] { "Id", "CompleteDate", "CreatedAt", "CreatedBy", "Description", "EndDate", "EstimatedProduct", "EstimatedUnit", "ExpertId", "IsApproved", "PlanName", "PlantId", "QRCode", "SeedQuantity", "StartDate", "Status", "UpdatedAt", "UpdatedBy", "YieldId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7521), "Admin", "Kế hoạch trồng cà chua vào mùa đông", new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 500f, "kg", 1, true, "Trồng cà chua vụ đông", 1, null, null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete", null, null, 1 },
                    { 2, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7525), "Admin", "Kế hoạch trồng dưa lưới trong nhà kính", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 300f, "kg", 2, false, "Trồng dưa lưới", 2, null, null, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete", null, null, 2 },
                    { 3, new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7528), "Admin", "Kế hoạch trồng bắp cải sạch", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 400f, "kg", 3, true, "Trồng bắp cải", 3, null, null, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete", null, null, 3 },
                    { 4, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7531), "Admin", "Kế hoạch trồng rau muống ngắn ngày", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 200f, "kg", 1, true, "Trồng rau muống", 4, null, null, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete", null, null, 4 },
                    { 5, new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7534), "Admin", "Kế hoạch trồng cà rốt hữu cơ", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 350f, "kg", 2, false, "Trồng cà rốt", 5, null, null, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete", null, null, 5 },
                    { 6, new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7537), "Admin", "Kế hoạch trồng hành lá sạch", new DateTime(2024, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 250f, "kg", 3, true, "Trồng hành lá", 6, null, null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Complete", null, null, 6 },
                    { 7, null, new DateTime(2025, 3, 5, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7547), "Admin", "Bản kế hoạch chi tiết trồng củ cải trắng ngắn hạn trong vòng 30 ngày", new DateTime(2025, 4, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7540), 500f, "kg", 2, false, "Mùa vụ trồng củ cải trắng", 10, null, null, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7539), "Draft", null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "CaringTask",
                columns: new[] { "Id", "CompleteDate", "CreatedAt", "CreatedBy", "Description", "EndDate", "PlanId", "ProblemId", "ResultContent", "StartDate", "Status", "TaskName", "TaskType", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7612), "thangbinhbeo", "Quan sát lá, thân và quả dưa lưới để phát hiện dấu hiệu sâu bệnh, sử dụng biện pháp phòng trừ phù hợp.", new DateTime(2025, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, null, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Kiểm tra sâu bệnh trên dưa lưới", "Inspecting", null, null },
                    { 6, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7620), "thangbinhbeo", "Sử dụng thuốc sinh học phòng trừ bệnh nấm và sâu hại trên rau muống, đảm bảo an toàn thực phẩm.", new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, null, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Phun thuốc phòng bệnh cho rau muống", "Pesticide", null, null },
                    { 10, new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7629), "thangbinhbeo", "Quan sát kỹ các lá non và bông mướp để phát hiện dấu hiệu sâu bệnh sớm.", new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null, new DateTime(2024, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Kiểm tra côn trùng gây hại trên mướp hương", "Inspecting", null, null },
                    { 14, new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7669), "thangbinhbeo", "Loại bỏ cỏ dại thủ công để tránh ảnh hưởng đến xà lách non.", new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, null, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ongoing", "Bón phân NPK cho cải ngọt", "Fertilizing", null, null }
                });

            migrationBuilder.InsertData(
                table: "FarmerPermission",
                columns: new[] { "FarmerId", "PlanId", "CreatedAt", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 1, 19, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7578), "Complete" },
                    { 2, 1, new DateTime(2025, 3, 5, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7576), "Cancel" },
                    { 3, 1, new DateTime(2025, 2, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7579), "Pending" },
                    { 5, 1, new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7580), "Pending" },
                    { 1, 2, new DateTime(2025, 2, 11, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7574), "Cancel" },
                    { 3, 2, new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7577), "Pending" },
                    { 4, 2, new DateTime(2025, 2, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7572), "Complete" },
                    { 5, 2, new DateTime(2025, 2, 12, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7575), "Complete" }
                });

            migrationBuilder.InsertData(
                table: "HarvestingTask",
                columns: new[] { "Id", "CompleteDate", "CreatedAt", "CreatedBy", "Description", "EndDate", "FailQuantity", "HarvestedQuantity", "PlanId", "ProductExpiredDate", "ResultContent", "StartDate", "Status", "TaskName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7834), new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7835), "thangbinhbeo", "Thu hoạch rau cải trước khi trời quá nắng", new DateTime(2025, 3, 12, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7833), null, 50f, 2, null, "Đã hủy vì cây không đạt chất lượng kiểm định", new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7831), "Cancel", "Thu hoạch rau cải", null, null },
                    { 2, new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7838), new DateTime(2025, 3, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7839), "thangbinhbeo", "Thu hoạch cà chua chín đỏ", new DateTime(2025, 3, 11, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7838), null, 30f, 1, null, null, new DateTime(2025, 3, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7837), "Pending", "Thu hoạch cà chua", null, null },
                    { 3, new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7842), new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7843), "thangbinhbeo", "Thu hoạch bắp cải vào sáng sớm để giữ độ tươi", new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7842), null, 40f, 3, null, "Đã hủy vì cây chết hết rồi", new DateTime(2025, 3, 2, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7841), "Cancel", "Thu hoạch bắp cải", null, null },
                    { 4, new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7846), new DateTime(2025, 3, 11, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7846), "thangbinhbeo", "Thu hoạch dưa leo vào đúng thời điểm chín", new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7845), null, 20f, 4, null, null, new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7845), "Pending", "Thu hoạch dưa leo", null, null },
                    { 5, new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7849), new DateTime(2025, 3, 7, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7850), "thangbinhbeo", "Thu hoạch bí đỏ khi vỏ cứng lại", new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7848), null, 15f, 5, null, null, new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7848), "Pending", "Thu hoạch bí đỏ", null, null }
                });

            migrationBuilder.InsertData(
                table: "InspectingForm",
                columns: new[] { "Id", "CanHarvest", "CompleteDate", "CreatedAt", "CreatedBy", "Description", "EndDate", "FormName", "InspectorId", "NumberOfSample", "PlanId", "ResultContent", "SampleWeight", "StartDate", "Status", "Type", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 3, 11, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7935), new DateTime(2025, 3, 9, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7936), "thangbinhbeo", "Đánh giá chất lượng rau cải trước khi thu hoạch", new DateTime(2025, 3, 11, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7933), "Kiểm tra rau cải", 2, null, 1, "Rau cải đạt chuẩn", null, new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7933), "Completed", "Kiểm tra chất lượng", null, null },
                    { 2, true, new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7940), new DateTime(2025, 3, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7941), "thangbinhbeo", "Đánh giá màu sắc và chất lượng cà chua", new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7939), "Kiểm tra cà chua", 1, null, 2, "Cà chua đạt độ chín", null, new DateTime(2025, 3, 9, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7939), "Completed", "Kiểm tra độ chín", null, null },
                    { 3, false, new DateTime(2025, 3, 9, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7975), new DateTime(2025, 3, 7, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7976), "thangbinhbeo", "Kiểm tra độ ẩm và màu sắc bắp cải", new DateTime(2025, 3, 9, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7974), "Kiểm tra bắp cải", 2, null, 3, "Bắp cải hơi thiếu nước", null, new DateTime(2025, 3, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7974), "Pending", "Kiểm tra độ ẩm", null, null },
                    { 4, true, new DateTime(2025, 3, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7979), new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7980), "thangbinhbeo", "Xác định độ chín và độ giòn của dưa leo", new DateTime(2025, 3, 8, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7978), "Kiểm tra dưa leo", 1, null, 4, "Dưa leo đạt chuẩn", null, new DateTime(2025, 3, 7, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7978), "Completed", "Kiểm tra độ chín", null, null },
                    { 5, false, new DateTime(2025, 3, 7, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7983), new DateTime(2025, 3, 5, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7983), "thangbinhbeo", "Kiểm tra vỏ bí đỏ để xác định độ cứng", new DateTime(2025, 3, 7, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7982), "Kiểm tra bí đỏ", 2, null, 5, "Vỏ bí đỏ chưa đủ cứng", null, new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7981), "Completed", "Kiểm tra độ cứng", null, null },
                    { 6, true, new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7986), new DateTime(2025, 3, 4, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7987), "thangbinhbeo", "Đánh giá độ ngọt và màu sắc của ớt chuông", new DateTime(2025, 3, 6, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7985), "Kiểm tra ớt chuông", 1, null, 6, "Ớt chuông có độ ngọt tốt", null, new DateTime(2025, 3, 5, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7985), "Completed", "Kiểm tra độ ngọt", null, null }
                });

            migrationBuilder.InsertData(
                table: "PackagingTask",
                columns: new[] { "Id", "CompleteDate", "CreatedAt", "CreatedBy", "Description", "EndDate", "PackagingTypeId", "PackedQuantity", "PlanId", "ResultContent", "StartDate", "Status", "TaskName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8011), new DateTime(2025, 3, 9, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8012), "thangbinhbeo", "Đóng gói gạo vào túi 5kg", new DateTime(2025, 3, 17, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8011), null, 1000, 1, "Đã đóng gói theo túi 5kg, thu được 1000 túi", new DateTime(2025, 3, 10, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8010), "Complete", "Đóng gói gạo", new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8013), null },
                    { 2, new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8016), new DateTime(2025, 3, 11, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8017), "thangbinhbeo", "Đóng gói cà phê bột vào túi 1kg", new DateTime(2025, 3, 16, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8016), null, 500, 2, "Đã đóng gói được 500 túi", new DateTime(2025, 3, 12, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8015), "Complete", "Đóng gói cà phê", new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8018), null },
                    { 3, new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8021), new DateTime(2025, 3, 12, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8021), "thangbinhbeo", "Đóng gói trà xanh vào hộp 100g", new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8020), null, 20000, 3, "Đã đóng gói thành công 20000 gói", new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8020), "Complete", "Đóng gói trà xanh", new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8022), null },
                    { 4, new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8025), new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8026), "thangbinhbeo", "Đóng gói hạt điều vào túi 500g", new DateTime(2025, 3, 19, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8025), null, 300, 4, "Đã đóng gói thành công", new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8024), "Complete", "Đóng gói hạt điều", new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8027), null },
                    { 5, new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8029), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8030), "thangbinhbeo", "Đóng gói xoài sấy vào túi 250g", new DateTime(2025, 3, 20, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8029), null, 10000, 5, "Đã đóng gói 10000 gói", new DateTime(2025, 3, 16, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8028), "Complete", "Đóng gói xoài sấy", new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(8030), null }
                });

            migrationBuilder.InsertData(
                table: "Problem",
                columns: new[] { "Id", "CreatedDate", "Description", "FarmerId", "PlanId", "ProblemName", "ResultContent", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xuất hiện sâu ăn lá trên cây.", 6, 1, "Sâu bệnh trên lá", null, "Pending" },
                    { 2, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đất khô, cây có dấu hiệu héo.", 6, 2, "Thiếu nước", "Đã tưới nước bổ sung.", "Resolved" },
                    { 3, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lá vàng, cây chậm phát triển.", 6, 3, "Đất kém dinh dưỡng", null, "Pending" },
                    { 4, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Xuất hiện đốm trắng trên lá.", 6, 3, "Cây bị nấm", "Đã phun thuốc chống nấm.", "Resolved" },
                    { 5, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cây phát triển yếu do ánh sáng yếu.", 6, 3, "Thiếu ánh sáng", null, "Pending" },
                    { 6, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phát hiện dấu hiệu sâu đục thân cây.", 6, 4, "Sâu đục thân", "Đã xử lý bằng thuốc trừ sâu.", "Resolved" },
                    { 7, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đất ẩm lâu, có nguy cơ úng rễ.", 6, 4, "Mưa quá nhiều", null, "Pending" },
                    { 8, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cây không đủ dinh dưỡng, lá rụng nhiều.", 6, 5, "Cây bị héo", "Đã bổ sung phân bón.", "Resolved" },
                    { 9, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bọ trĩ gây hại trên lá non.", 6, 6, "Bọ trĩ tấn công", null, "Pending" },
                    { 10, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nắng nóng kéo dài gây stress cho cây.", 6, 6, "Nhiệt độ quá cao", "Đã che bóng giảm nhiệt độ.", "Resolved" }
                });

            migrationBuilder.InsertData(
                table: "CaringFertilizer",
                columns: new[] { "Id", "FertilizerId", "Quantity", "TaskId", "Unit" },
                values: new object[] { 3, 3, 6f, 14, "Kg" });

            migrationBuilder.InsertData(
                table: "CaringImage",
                columns: new[] { "Id", "TaskId", "Url" },
                values: new object[,]
                {
                    { 4, 3, "https://vaas.vn/sites/default/files/inline-images/z4410949507075_34eafecfa0fd04dc99cedfea94f519bb.jpg" },
                    { 7, 6, "https://danviet.mediacdn.vn/upload/3-2015/images/2015-07-16/1437018395-rau_muong_1211113-035347.jpg" }
                });

            migrationBuilder.InsertData(
                table: "CaringItem",
                columns: new[] { "Id", "ItemId", "Quantity", "TaskId", "Unit" },
                values: new object[,]
                {
                    { 4, 2, 1, 6, "Cái" },
                    { 5, 3, 10, 3, "Kg" },
                    { 10, 3, 12, 10, "Kg" },
                    { 14, 2, 1, 14, "Cái" }
                });

            migrationBuilder.InsertData(
                table: "CaringPesticide",
                columns: new[] { "Id", "PesticideId", "Quantity", "TaskId", "Unit" },
                values: new object[] { 1, 1, 2.5f, 6, "Lít" });

            migrationBuilder.InsertData(
                table: "CaringTask",
                columns: new[] { "Id", "CompleteDate", "CreatedAt", "CreatedBy", "Description", "EndDate", "PlanId", "ProblemId", "ResultContent", "StartDate", "Status", "TaskName", "TaskType", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7606), "thangbinhbeo", "Tưới nước đều đặn vào sáng sớm và chiều tối để giữ ẩm cho cây cà chua, tránh tưới quá nhiều gây ngập úng.", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Tưới nước cho cà chua", "Watering", null, null },
                    { 2, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7610), "thangbinhbeo", "Sử dụng phân hữu cơ để cung cấp dưỡng chất cho cây cà chua, bón vào gốc cây tránh tiếp xúc trực tiếp với lá.", new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, null, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Bón phân hữu cơ cho cà chua", "Fertilizing", null, null },
                    { 4, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7614), "thangbinhbeo", "Thiết lập hệ thống tưới nhỏ giọt giúp cây nhận đủ nước mà không gây lãng phí.", new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, null, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ongoing", "Lắp hệ thống tưới tự động", "Setup", null, null },
                    { 5, new DateTime(2024, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7617), "thangbinhbeo", "Loại bỏ cỏ dại xung quanh bắp cải để tránh cạnh tranh dinh dưỡng và ngăn ngừa sâu bệnh.", new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5, null, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Nhổ cỏ dại quanh bắp cải", "Weeding", null, null },
                    { 7, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7622), "thangbinhbeo", "Dọn sạch nilon, chai lọ, bao bì thuốc bảo vệ thực vật để giữ gìn môi trường sạch sẽ.", new DateTime(2024, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 7, null, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", "Thu gom rác nông nghiệp", "Cleaning", null, null },
                    { 8, new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7624), "thangbinhbeo", "Tưới nước vừa đủ giúp cà rốt phát triển đều, tránh tình trạng úng rễ hoặc khô hạn.", new DateTime(2024, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 8, null, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Tưới nước cho cà rốt", "Watering", null, null },
                    { 9, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7627), "thangbinhbeo", "Phun phân bón lá để thúc đẩy sự phát triển của hành lá, đảm bảo đủ dưỡng chất.", new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 9, null, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ongoing", "Bón phân lá cho hành lá", "Fertilizing", null, null },
                    { 11, new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7631), "thangbinhbeo", "Loại bỏ cành không cần thiết để tập trung dinh dưỡng cho quả ớt phát triển.", new DateTime(2024, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, null, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Cắt tỉa cành ớt chuông", "Pruning", null, null },
                    { 12, new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7633), "thangbinhbeo", "Dùng hệ thống phun sương để tưới nước cho rau xà lách, giúp lá luôn tươi tốt.", new DateTime(2024, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, null, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Tưới phun sương cho rau xà lách", "Watering", null, null },
                    { 13, new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7667), "thangbinhbeo", "Loại bỏ cỏ dại thủ công để tránh ảnh hưởng đến xà lách non.", new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, null, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", "Nhổ cỏ dại trong vườn xà lách", "Weeding", null, null },
                    { 15, new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7671), "thangbinhbeo", "Loại bỏ cỏ dại thủ công để tránh ảnh hưởng đến xà lách non.", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 5, null, new DateTime(2024, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", "Phun thuốc sinh học phòng bệnh cho cải ngọt", "Pesticide", null, null }
                });

            migrationBuilder.InsertData(
                table: "FarmerCaringTask",
                columns: new[] { "FarmerId", "TaskId", "Description", "ExpiredDate", "Status" },
                values: new object[,]
                {
                    { 2, 3, "Weeding completed successfully.", new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7706), "Completed" },
                    { 2, 14, "Scheduled pest control task delayed.", new DateTime(2025, 3, 19, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7721), "Pending" },
                    { 4, 6, "Plant disease detected, applying treatment.", new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7716), "In Progress" },
                    { 6, 10, "Installing new irrigation pipes.", new DateTime(2025, 3, 20, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7718), "In Progress" }
                });

            migrationBuilder.InsertData(
                table: "FarmerHarvestingTask",
                columns: new[] { "FarmerId", "TaskId", "Description", "ExpiredDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Thu hoạch rau cải theo tiêu chuẩn hữu cơ, cắt sạch gốc và đóng gói.", new DateTime(2025, 3, 14, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7872), "Completed" },
                    { 1, 3, "Thu hoạch cải xanh và vận chuyển ngay sau khi thu hoạch.", new DateTime(2025, 3, 17, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7882), "Pending" },
                    { 2, 2, "Thu hoạch cà chua chín, phân loại quả chất lượng cao trước khi vận chuyển.", new DateTime(2025, 3, 18, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7874), "Pending" },
                    { 2, 5, "Thu hoạch hành lá, buộc thành bó nhỏ trước khi phân phối.", new DateTime(2025, 3, 19, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7883), "In Progress" },
                    { 3, 3, "Thu hái rau muống, đảm bảo không lẫn tạp chất trong sản phẩm.", new DateTime(2025, 3, 13, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7875), "Completed" },
                    { 4, 4, "Thu hoạch dưa leo khi đạt kích thước tiêu chuẩn, kiểm tra chất lượng từng quả.", new DateTime(2025, 3, 16, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7876), "In Progress" },
                    { 5, 5, "Thu hoạch bắp cải non, đảm bảo không có sâu bệnh trước khi đóng gói.", new DateTime(2025, 3, 12, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7877), "Failed" },
                    { 6, 1, "Thu hái rau dền đúng thời điểm để đảm bảo độ tươi ngon.", new DateTime(2025, 3, 14, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(7881), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "FarmerPackagingTask",
                columns: new[] { "FarmerId", "TaskId", "Description", "ExpiredDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Đóng gói rau cải vào túi hút chân không, đảm bảo giữ độ tươi lâu.", new DateTime(2025, 3, 14, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8053), "Completed" },
                    { 1, 3, "Sắp xếp rau húng quế vào hộp nhựa, đảm bảo không dập nát.", new DateTime(2025, 3, 17, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8060), "Pending" },
                    { 2, 2, "Phân loại và đóng hộp cà chua theo kích cỡ, ghi nhãn nguồn gốc.", new DateTime(2025, 3, 18, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8054), "Pending" },
                    { 3, 3, "Đóng gói rau muống vào túi lưới, tránh đè nén gây hư hỏng.", new DateTime(2025, 3, 13, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8055), "Completed" },
                    { 3, 5, "Niêm phong bao bì rau mồng tơi, gắn mã QR để truy xuất nguồn gốc.", new DateTime(2025, 3, 11, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8061), "Completed" },
                    { 4, 4, "Đặt dưa leo vào khay nhựa, bọc màng co để bảo vệ độ tươi.", new DateTime(2025, 3, 16, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8057), "In Progress" },
                    { 5, 2, "Phân chia đậu bắp vào túi nhỏ 250g, đảm bảo đạt tiêu chuẩn an toàn.", new DateTime(2025, 3, 16, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8063), "In Progress" },
                    { 5, 5, "Bọc bắp cải bằng giấy thực phẩm, bảo quản trong hộp carton.", new DateTime(2025, 3, 12, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8058), "Failed" },
                    { 6, 1, "Đóng túi rau dền theo định lượng 500g, kiểm tra độ sạch trước khi niêm phong.", new DateTime(2025, 3, 14, 8, 46, 46, 805, DateTimeKind.Utc).AddTicks(8059), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "HarvestingImage",
                columns: new[] { "Id", "TaskId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2022/1/4/991490/Thu-Hoach-Rau-Cai-Th.jpg" },
                    { 2, 2, "https://kholanhbachkhoa.net/wp-content/uploads/2024/05/vuon-ca-chua-da-lat-3.jpg" },
                    { 3, 3, "https://fatechme.com/uploads/bapcai12.jpg" },
                    { 4, 4, "https://danviet.mediacdn.vn/upload/3-2018/images/2018-07-02/Bi-quyet-trong-dua-leo-moi-vu-moi-trung-cua-nguoi-phu-nu-miet-vuon-anh-trong-2-1530512281-width680height490.jpg" },
                    { 5, 5, "https://tttt.ninhbinh.gov.vn/uploads/images/DAN%20TOC%20MN%202022/DTMN%202024/QUA%20BI%20DO.jpg" }
                });

            migrationBuilder.InsertData(
                table: "HarvestingItem",
                columns: new[] { "Id", "ItemId", "Quantity", "TaskId", "Unit" },
                values: new object[,]
                {
                    { 1, 5, 2, 1, "Cái" },
                    { 2, 6, 1, 1, "Cái" },
                    { 3, 7, 1, 2, "Cái" },
                    { 4, 5, 1, 2, "Cái" },
                    { 5, 5, 1, 3, "Cái" },
                    { 6, 6, 2, 3, "Cái" },
                    { 7, 7, 1, 4, "Cái" },
                    { 8, 6, 1, 4, "Cái" },
                    { 9, 6, 1, 5, "Cái" },
                    { 10, 5, 2, 5, "Cái" }
                });

            migrationBuilder.InsertData(
                table: "InspectingResult",
                columns: new[] { "Id", "Arsen", "Cadmi", "Chlorate", "Coliforms", "Dithiocarbamate", "Ecoli", "EvaluatedResult", "FormId", "Glyphosate_Glufosinate", "Hydrargyrum", "HydrogenPhosphide", "MethylBromide", "NaNO3_KNO3", "Nitrat", "Perchlorate", "Plumbum", "Salmonella", "SulfurDioxide" },
                values: new object[,]
                {
                    { 1, 0.01f, 0.005f, 0.1f, 10f, 0.02f, 1f, "Pass", 1, 0.02f, 0.001f, 0.05f, 0.1f, 10f, 50f, 0.02f, 0.02f, 0f, 5f },
                    { 2, 0.02f, 0.007f, 0.2f, 20f, 0.03f, 3f, "Fail", 2, 0.03f, 0.002f, 0.06f, 0.2f, 15f, 60f, 0.03f, 0.03f, 1f, 10f },
                    { 3, 0.005f, 0.004f, 0.08f, 5f, 0.015f, 0f, "Pass", 3, 0.015f, 0.0005f, 0.03f, 0.05f, 8f, 40f, 0.015f, 0.015f, 0f, 3f },
                    { 4, 0.03f, 0.01f, 0.3f, 25f, 0.04f, 5f, "Fail", 4, 0.04f, 0.003f, 0.08f, 0.3f, 20f, 70f, 0.04f, 0.04f, 2f, 15f },
                    { 5, 0.007f, 0.006f, 0.09f, 8f, 0.018f, 1f, "Pass", 5, 0.018f, 0.0012f, 0.04f, 0.07f, 9f, 45f, 0.018f, 0.017f, 0f, 4f },
                    { 6, 0.011f, 0.006f, 0.11f, 9f, 0.025f, 1f, "Pass", 6, 0.022f, 0.0011f, 0.055f, 0.12f, 12f, 55f, 0.022f, 0.021f, 0f, 6f },
                    { 7, 0.025f, 0.009f, 0.22f, 18f, 0.035f, 4f, "Fail", 1, 0.035f, 0.0022f, 0.07f, 0.25f, 18f, 65f, 0.035f, 0.035f, 1f, 12f },
                    { 8, 0.008f, 0.005f, 0.085f, 7f, 0.017f, 1f, "Pass", 2, 0.017f, 0.0009f, 0.035f, 0.06f, 10f, 42f, 0.017f, 0.018f, 0f, 4f },
                    { 9, 0.028f, 0.012f, 0.35f, 30f, 0.045f, 6f, "Fail", 3, 0.045f, 0.0028f, 0.09f, 0.35f, 25f, 75f, 0.045f, 0.038f, 2f, 18f },
                    { 10, 0.006f, 0.003f, 0.07f, 4f, 0.013f, 0f, "Pass", 4, 0.013f, 0.0004f, 0.025f, 0.04f, 7f, 35f, 0.013f, 0.014f, 0f, 2f }
                });

            migrationBuilder.InsertData(
                table: "PackagingImage",
                columns: new[] { "Id", "TaskId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://maydonggoi.com.vn/wp-content/uploads/2021/12/quy-trinh-dong-goi-gao-6.jpg" },
                    { 2, 2, "https://maygoi.vn/wp-content/uploads/2019/03/maxresdefault-1.jpg" },
                    { 3, 3, "https://thainguyencity.gov.vn/upload/news/2013/11/532/image/img5751.jpg" },
                    { 4, 4, "https://jraifarm.com/files/upload/files/h%E1%BA%A1t%20%C4%91i%E1%BB%81u/Quy%20c%C3%A1ch%20%C4%91%C3%B3ng%20g%C3%B3i%20h%E1%BA%A1t%20%C4%91i%E1%BB%81u%20xu%E1%BA%A5t%20kh%E1%BA%A9u%204.png" },
                    { 5, 5, "https://nanufoods.vn/wp-content/uploads/2020/05/Soft-dried-mango-in-carton-579x400.jpg" }
                });

            migrationBuilder.InsertData(
                table: "PackagingItem",
                columns: new[] { "Id", "ItemId", "Quantity", "TaskId", "Unit" },
                values: new object[,]
                {
                    { 1, 8, 2, 1, "machine" },
                    { 2, 9, 4, 2, "unit" },
                    { 3, 10, 6, 1, "unit" },
                    { 4, 11, 3, 4, "machine" },
                    { 5, 12, 1, 5, "line" }
                });

            migrationBuilder.InsertData(
                table: "ProblemImage",
                columns: new[] { "Id", "ProblemId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://vnmedia.vn/file/8a10a0d36ccebc89016ce0c6fa3e1b83/8a10a0d3761897b0017665518e9b6a91/072022/2.sau_to.sau_bap_cai_20220714112122.jpeg" },
                    { 2, 1, "https://lh6.googleusercontent.com/proxy/izOE5Anqbg4twC9njJa03WFsLuRu1J46zvsAGTocWFn5jcMDko9HXi_8TujVy5rDMKXI2NSfy4cot2z3H4-9PBwzBA" },
                    { 3, 2, "https://dongthanhcong.vn/wp-content/uploads/2024/07/dau-hieu-cua-cay-bi-thieu-nuoc-1200x720.jpg" },
                    { 4, 2, "https://thmh.vn/wp-content/uploads/2024/10/cay-bi-heo.png" },
                    { 5, 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJnmU_pF5Xx89ht6XZItdpbtLKm6Xk_e_xaw&s" },
                    { 6, 3, "https://biovina.com.vn/wp-content/uploads/2016/05/thieudinhduong.jpg" },
                    { 7, 4, "https://bachnong.vn/upload/tin-tuc/phong-tri-benh-cay-trong/benh-nam-o-cay-trong_1.jpg" },
                    { 8, 4, "https://nhaluoiviet.vn/images/tin-tuc/trong-rau/cac-loai-nam-thuong-gap-o-cay-trong.jpg" },
                    { 9, 5, "https://camnangnhanong.wordpress.com/wp-content/uploads/2023/11/image-1.png?w=1024" },
                    { 10, 5, "https://bizweb.dktcdn.net/100/521/346/files/cay-thieu-anh-sang-co-bieu-hien-gi.jpg?v=1735714018430" },
                    { 11, 6, "https://bvtvthienbinh.com/files/upload/TIN-TUC/can-canh-sau-duc-than.jpg" },
                    { 12, 6, "https://kimnonggoldstar.vn/wp-content/uploads/2022/12/sau-duc-than-hai-sau-rieng-kimnonggoldstar-vn-1.jpg" },
                    { 13, 7, "https://file1.dangcongsan.vn/data/0/images/2022/09/16/vanphong/imager-8-64713-700.jpg" }
                });

            migrationBuilder.InsertData(
                table: "CaringFertilizer",
                columns: new[] { "Id", "FertilizerId", "Quantity", "TaskId", "Unit" },
                values: new object[,]
                {
                    { 1, 1, 5f, 2, "Kg" },
                    { 2, 2, 4f, 9, "Kg" }
                });

            migrationBuilder.InsertData(
                table: "CaringImage",
                columns: new[] { "Id", "TaskId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://thumb.photo-ac.com/33/331a66bc564e083cb1c81babfba42d41_t.jpeg" },
                    { 2, 1, "https://www.vigecam.vn/Portals/27059/10%20quanganh/cachtrongcachuasachvasaiquaquymonongtrai%201.jpg" },
                    { 3, 2, "https://danviet.ex-cdn.com/files/f1/2017/5/images/10c4864b-trang-trai-ca-chua-nhat-2.jpg" },
                    { 5, 4, "https://danviet.ex-cdn.com/files/f1/296231569849192448/2022/5/13/edit-z3411936151630efaace430e503df8e6a548a064ff5839-1652436512592542646364-1652440184006468269746.jpeg" },
                    { 6, 5, "https://i.ex-cdn.com/nongnghiep.vn/files/bao_in/2020/08/11/hb-mh-trong-bap-cai-trai-vu-1123_20200811_966-135034.jpeg" },
                    { 8, 7, "https://baothainguyen.vn/file/oldimage/baothainguyen/UserFiles/image/tru-sau.jpg" },
                    { 9, 7, "https://media.quangninh.gov.vn/f5733364-2623-4af8-8267-09c9a345f144/Libraries/hinhanhbaiviet/%E1%BA%A3nh%20%C4%91%C4%83ng%20web/n%C4%83m%202021/cc%20tt-bvtv/bo%20xit/bo%20xit%20gay%20hai.jpg" },
                    { 10, 7, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSfgHM1ONv6CYGP2H7afMf3Y34z7W6yIY_Erw&s" }
                });

            migrationBuilder.InsertData(
                table: "CaringItem",
                columns: new[] { "Id", "ItemId", "Quantity", "TaskId", "Unit" },
                values: new object[,]
                {
                    { 1, 1, 2, 1, "Cái" },
                    { 2, 1, 1, 5, "Cái" },
                    { 3, 2, 1, 2, "Cái" },
                    { 6, 3, 15, 7, "Kg" },
                    { 7, 4, 1, 4, "Cái" },
                    { 8, 4, 1, 8, "Cái" },
                    { 9, 1, 1, 9, "Cái" },
                    { 11, 2, 1, 11, "Cái" },
                    { 12, 4, 1, 12, "Cái" },
                    { 13, 3, 8, 13, "Kg" },
                    { 15, 1, 2, 15, "Cái" }
                });

            migrationBuilder.InsertData(
                table: "CaringPesticide",
                columns: new[] { "Id", "PesticideId", "Quantity", "TaskId", "Unit" },
                values: new object[] { 2, 2, 3f, 15, "Lít" });

            migrationBuilder.InsertData(
                table: "FarmerCaringTask",
                columns: new[] { "FarmerId", "TaskId", "Description", "ExpiredDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Soil preparation delayed due to unexpected rain.", new DateTime(2025, 3, 18, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7704), "Pending" },
                    { 1, 12, "Harvesting completed for lettuce field.", new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7719), "Completed" },
                    { 1, 13, "Crop monitoring performed with drone imaging.", new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7712), "Completed" },
                    { 2, 2, "Seed sowing delayed due to broken equipment.", new DateTime(2025, 3, 21, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7713), "Pending" },
                    { 3, 4, "Applying compost to improve soil fertility.", new DateTime(2025, 3, 12, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7715), "Completed" },
                    { 3, 5, "Fertilizer application postponed due to supply shortage.", new DateTime(2025, 3, 20, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7708), "Pending" },
                    { 3, 15, "Monitoring crop growth using sensors.", new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7722), "Completed" },
                    { 4, 7, "Irrigation system maintenance completed.", new DateTime(2025, 3, 13, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7709), "Completed" },
                    { 5, 8, "Weed removal completed successfully.", new DateTime(2025, 3, 14, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7717), "Completed" },
                    { 5, 9, "Pest control activity in progress.", new DateTime(2025, 3, 17, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7710), "In Progress" },
                    { 6, 11, "Harvest preparation started.", new DateTime(2025, 3, 19, 15, 46, 46, 805, DateTimeKind.Local).AddTicks(7711), "In Progress" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaringFertilizer_FertilizerId",
                table: "CaringFertilizer",
                column: "FertilizerId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringFertilizer_TaskId",
                table: "CaringFertilizer",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringImage_TaskId",
                table: "CaringImage",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringItem_ItemId",
                table: "CaringItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringItem_TaskId",
                table: "CaringItem",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringPesticide_PesticideId",
                table: "CaringPesticide",
                column: "PesticideId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringPesticide_TaskId",
                table: "CaringPesticide",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringTask_PlanId",
                table: "CaringTask",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringTask_ProblemId",
                table: "CaringTask",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_DataEnvironment_YieldId",
                table: "DataEnvironment",
                column: "YieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_YieldId",
                table: "Device",
                column: "YieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Expert_AccountId",
                table: "Expert",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Farmer_AccountId",
                table: "Farmer",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerCaringTask_TaskId",
                table: "FarmerCaringTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerHarvestingTask_TaskId",
                table: "FarmerHarvestingTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerPackagingTask_TaskId",
                table: "FarmerPackagingTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerPermission_FarmerId",
                table: "FarmerPermission",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestingImage_TaskId",
                table: "HarvestingImage",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestingItem_ItemId",
                table: "HarvestingItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestingItem_TaskId",
                table: "HarvestingItem",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HarvestingTask_PlanId",
                table: "HarvestingTask",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectingForm_InspectorId",
                table: "InspectingForm",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectingForm_PlanId",
                table: "InspectingForm",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectingImage_ResultId",
                table: "InspectingImage",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectingResult_FormId",
                table: "InspectingResult",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspector_AccountId",
                table: "Inspector",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationExpert_ExpertId",
                table: "NotificationExpert",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationFarmer_FarmerId",
                table: "NotificationFarmer",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRetailer_RetailerId",
                table: "NotificationRetailer",
                column: "RetailerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PackagingTypeId",
                table: "Order",
                column: "PackagingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PlanId",
                table: "Order",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_RetailerId",
                table: "Order",
                column: "RetailerId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingImage_TaskId",
                table: "PackagingImage",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingItem_ItemId",
                table: "PackagingItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingItem_TaskId",
                table: "PackagingItem",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingProduct_HarvestingTaskId",
                table: "PackagingProduct",
                column: "HarvestingTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingProduct_OrderId",
                table: "PackagingProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingProduct_PackagingTaskId",
                table: "PackagingProduct",
                column: "PackagingTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingTask_PackagingTypeId",
                table: "PackagingTask",
                column: "PackagingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PackagingTask_PlanId",
                table: "PackagingTask",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_ExpertId",
                table: "Plan",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlantId",
                table: "Plan",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_YieldId",
                table: "Plan",
                column: "YieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantYield_YieldId",
                table: "PlantYield",
                column: "YieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_FarmerId",
                table: "Problem",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Problem_PlanId",
                table: "Problem",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemImage_ProblemId",
                table: "ProblemImage",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Retailer_AccountId",
                table: "Retailer",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderId",
                table: "Transaction",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaringFertilizer");

            migrationBuilder.DropTable(
                name: "CaringImage");

            migrationBuilder.DropTable(
                name: "CaringItem");

            migrationBuilder.DropTable(
                name: "CaringPesticide");

            migrationBuilder.DropTable(
                name: "DataEnvironment");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "FarmerCaringTask");

            migrationBuilder.DropTable(
                name: "FarmerHarvestingTask");

            migrationBuilder.DropTable(
                name: "FarmerPackagingTask");

            migrationBuilder.DropTable(
                name: "FarmerPermission");

            migrationBuilder.DropTable(
                name: "HarvestingImage");

            migrationBuilder.DropTable(
                name: "HarvestingItem");

            migrationBuilder.DropTable(
                name: "InspectingImage");

            migrationBuilder.DropTable(
                name: "NotificationExpert");

            migrationBuilder.DropTable(
                name: "NotificationFarmer");

            migrationBuilder.DropTable(
                name: "NotificationOwner");

            migrationBuilder.DropTable(
                name: "NotificationRetailer");

            migrationBuilder.DropTable(
                name: "PackagingImage");

            migrationBuilder.DropTable(
                name: "PackagingItem");

            migrationBuilder.DropTable(
                name: "PackagingProduct");

            migrationBuilder.DropTable(
                name: "PlantYield");

            migrationBuilder.DropTable(
                name: "ProblemImage");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Fertilizer");

            migrationBuilder.DropTable(
                name: "Pesticide");

            migrationBuilder.DropTable(
                name: "CaringTask");

            migrationBuilder.DropTable(
                name: "InspectingResult");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "HarvestingTask");

            migrationBuilder.DropTable(
                name: "PackagingTask");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "InspectingForm");

            migrationBuilder.DropTable(
                name: "PackagingType");

            migrationBuilder.DropTable(
                name: "Retailer");

            migrationBuilder.DropTable(
                name: "Farmer");

            migrationBuilder.DropTable(
                name: "Inspector");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "Expert");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "Yield");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
