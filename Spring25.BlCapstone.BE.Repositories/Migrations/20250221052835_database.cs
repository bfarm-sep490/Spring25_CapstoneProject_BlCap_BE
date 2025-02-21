using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spring25.BlCapstone.BE.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableQuantity = table.Column<float>(type: "real", nullable: false),
                    TotalQuantity = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
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
                    AvailableQuantity = table.Column<float>(type: "real", nullable: false),
                    TotalQuantity = table.Column<float>(type: "real", nullable: false),
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
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    MinTemp = table.Column<double>(type: "float", nullable: false),
                    MaxTemp = table.Column<double>(type: "float", nullable: false),
                    MinHumid = table.Column<double>(type: "float", nullable: false),
                    MaxHumid = table.Column<double>(type: "float", nullable: false),
                    MinMoisture = table.Column<double>(type: "float", nullable: false),
                    MaxMoisture = table.Column<double>(type: "float", nullable: false),
                    MinFertilizer = table.Column<double>(type: "float", nullable: false),
                    MaxFertilizer = table.Column<double>(type: "float", nullable: false),
                    FertilizerUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinPesticide = table.Column<double>(type: "float", nullable: false),
                    MaxPesticide = table.Column<double>(type: "float", nullable: false),
                    PesticideUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinBrixPoint = table.Column<double>(type: "float", nullable: false),
                    MaxBrixPoint = table.Column<double>(type: "float", nullable: false),
                    GTTestKitColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Area = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
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
                    LongxLat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "FertilizerRange",
                columns: table => new
                {
                    FertilizerId = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maximum = table.Column<float>(type: "real", nullable: false),
                    Minimum = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerRange", x => new { x.PlantId, x.FertilizerId });
                    table.ForeignKey(
                        name: "FK_FertilizerRange_Fertilizer_FertilizerId",
                        column: x => x.FertilizerId,
                        principalTable: "Fertilizer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FertilizerRange_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PesticideRange",
                columns: table => new
                {
                    PesticideId = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maximum = table.Column<float>(type: "real", nullable: false),
                    Minimum = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PesticideRange", x => new { x.PlantId, x.PesticideId });
                    table.ForeignKey(
                        name: "FK_PesticideRange_Pesticide_PesticideId",
                        column: x => x.PesticideId,
                        principalTable: "Pesticide",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PesticideRange_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YieldId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    YieldId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedProduct = table.Column<float>(type: "real", nullable: false),
                    EstimatedUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailablePackagingQuantity = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetailerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Retailer_RetailerId",
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
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
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HarvestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HarvestedQuantity = table.Column<float>(type: "real", nullable: true),
                    HarvestedUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HarvestingTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HarvestingTask_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
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
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrixPoint = table.Column<float>(type: "real", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Humidity = table.Column<float>(type: "real", nullable: false),
                    Moisture = table.Column<float>(type: "real", nullable: false),
                    ShellColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestGTKitColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectingQuantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuePercent = table.Column<float>(type: "real", nullable: true),
                    CanHarvest = table.Column<bool>(type: "bit", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "Problem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    ProblemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProblemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Problem_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderPlan",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPlan", x => new { x.PlanId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderPlan_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderPlan_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderPlant",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPlant", x => new { x.PlantId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderPlant_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderPlant_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
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
                name: "InspectingImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectingImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectingImage_InspectingForm_TaskId",
                        column: x => x.TaskId,
                        principalTable: "InspectingForm",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InspectingItem",
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
                    table.PrimaryKey("PK_InspectingItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectingItem_InspectingForm_TaskId",
                        column: x => x.TaskId,
                        principalTable: "InspectingForm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InspectingItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CaringTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    YieldId = table.Column<int>(type: "int", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    ProblemId = table.Column<int>(type: "int", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaringTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaringTask_Farmer_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmer",
                        principalColumn: "Id");
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
                    table.ForeignKey(
                        name: "FK_CaringTask_Yield_YieldId",
                        column: x => x.YieldId,
                        principalTable: "Yield",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    IssueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_Problem_ProblemId",
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
                name: "SampleSolution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleSolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleSolution_Issue_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issue",
                        principalColumn: "Id");
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
                name: "IX_CaringTask_FarmerId",
                table: "CaringTask",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringTask_PlanId",
                table: "CaringTask",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringTask_ProblemId",
                table: "CaringTask",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_CaringTask_YieldId",
                table: "CaringTask",
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
                name: "IX_FarmerPermission_FarmerId",
                table: "FarmerPermission",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerRange_FertilizerId",
                table: "FertilizerRange",
                column: "FertilizerId");

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
                name: "IX_HarvestingTask_FarmerId",
                table: "HarvestingTask",
                column: "FarmerId");

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
                name: "IX_InspectingImage_TaskId",
                table: "InspectingImage",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectingItem_ItemId",
                table: "InspectingItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectingItem_TaskId",
                table: "InspectingItem",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspector_AccountId",
                table: "Inspector",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProblemId",
                table: "Issue",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_RetailerId",
                table: "Order",
                column: "RetailerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlan_OrderId",
                table: "OrderPlan",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPlant_OrderId",
                table: "OrderPlant",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PesticideRange_PesticideId",
                table: "PesticideRange",
                column: "PesticideId");

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
                name: "IX_SampleSolution_IssueId",
                table: "SampleSolution",
                column: "IssueId");

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
                name: "Device");

            migrationBuilder.DropTable(
                name: "FarmerPermission");

            migrationBuilder.DropTable(
                name: "FertilizerRange");

            migrationBuilder.DropTable(
                name: "HarvestingImage");

            migrationBuilder.DropTable(
                name: "HarvestingItem");

            migrationBuilder.DropTable(
                name: "InspectingImage");

            migrationBuilder.DropTable(
                name: "InspectingItem");

            migrationBuilder.DropTable(
                name: "OrderPlan");

            migrationBuilder.DropTable(
                name: "OrderPlant");

            migrationBuilder.DropTable(
                name: "PesticideRange");

            migrationBuilder.DropTable(
                name: "ProblemImage");

            migrationBuilder.DropTable(
                name: "SampleSolution");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "CaringTask");

            migrationBuilder.DropTable(
                name: "Fertilizer");

            migrationBuilder.DropTable(
                name: "HarvestingTask");

            migrationBuilder.DropTable(
                name: "InspectingForm");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Pesticide");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Farmer");

            migrationBuilder.DropTable(
                name: "Inspector");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "Retailer");

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
