using AutoMapper;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
using Spring25.BlCapstone.BE.APIs.RequestModels.Plant;
using Spring25.BlCapstone.BE.APIs.RequestModels.Yield;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Config;
using Spring25.BlCapstone.BE.Services.BusinessModels.Device;
using Spring25.BlCapstone.BE.Services.BusinessModels.Expert;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.BusinessModels.Item;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
using Spring25.BlCapstone.BE.Services.BusinessModels.Order;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using Spring25.BlCapstone.BE.Services.BusinessModels.Retailer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using Spring25.BlCapstone.BE.Services.BusinessModels.Transaction;
using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;

namespace Spring25.BlCapstone.BE.APIs.Configs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            PesticideProfie();
            FertilizerProfile();
            FarmerProfile();
            SeedProfile();
            YieldProfile();
            InspectorProfile();
            ProblemProfile();
            PlanProfile();
            CaringProfile();
            AuthProfile();
            HarvestingProfile();
            InspectingProfile();
            PackagingProfile();
            DeviceProfile();
            ExpertProfile();
            RetailerProfile();
            InspectingResultProfile();
            HistoryFarmerProfile();
            ItemProfile();
            OrderProfile();
            NotificationProfile();
            TransactionProfile();
            ProductionProfile();
            ConfigSystemProfile();
            BatchProfile();
        }

        private void ConfigSystemProfile()
        {
            CreateMap<ConfigurationSystem, ConfigSystemCreate>()
                .ReverseMap();
        }

        private void BatchProfile()
        {
            CreateMap<ProductPickupBatch, BatchModel>()
                .ReverseMap();
            CreateMap<ProductPickupBatch, BatchCreateModel>()
                .ReverseMap();
        }

        private void OrderProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.transactionOrders, opt => opt.MapFrom(src => src.Transactions))
                .ForMember(dest => dest.RetailerName, opt => opt.MapFrom(src => src.Retailer.Account.Name))
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.Plant.PlantName))
                .ForMember(dest => dest.PlanInfors, opt => opt.MapFrom(src => src.OrderPlans))
                .ForMember(dest => dest.PackagingTypeName, opt => opt.MapFrom(src => src.PackagingType.Name))
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.PackagingTasks
                                                                                    .SelectMany(task => task.PackagingProducts
                                                                                                            .Select(ptp => new ProOr
                                                                                                                {
                                                                                                                    ProductId = ptp.Id,
                                                                                                                    QuantityOfPacks = ptp.PackQuantity,
                                                                                                                    Status = ptp.Status,
                                                                                                                    EvaluatedResult = task.Plan.InspectingForms
                                                                                                                    .OrderByDescending(f => f.CompleteDate)
                                                                                                                    .FirstOrDefault().InspectingResult.EvaluatedResult
                                                                                                                })).ToList()
                                                                                                                ))
                .ReverseMap();
            CreateMap<OrderPlan, PlanInfor>()
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ReverseMap();
            CreateMap<Transaction, TransactionOrder>()
                .ReverseMap();
            CreateMap<Order, CreateOrderModel>()
                .ReverseMap();
        }

        private void ExpertProfile()
        {
            CreateMap<Expert, ExpertModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Account.UpdatedAt))
                .ReverseMap();
            CreateMap<Expert, CreateFarmer>()
                .ReverseMap();
        }

        private void RetailerProfile()
        {
            CreateMap<Retailer, RetailerModels>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Account.UpdatedAt))
                .ReverseMap();
            CreateMap<Retailer, CreateRetailer>()
                .ReverseMap();
        }

        private void InspectingProfile()
        {
            CreateMap<InspectingForm, InspectingFormModel>()
                .ForMember(dest => dest.InspectorName, otp => otp.MapFrom(src => src.Inspector.Account.Name))
                .ForMember(dest => dest.InspectingResults, opt => opt.MapFrom(src => src.InspectingResult))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ReverseMap();
            CreateMap<InspectingResult, InspectingResultLess>()
                .ForMember(dest => dest.InspectingImageModels, opt => opt.MapFrom(src => src.InspectingImages))
                .ReverseMap();
            CreateMap<InspectingImage, InspectingResultModel>()
                .ReverseMap();
            CreateMap<InspectingForm, CreateInspectingPlan>()
                .ReverseMap();
            CreateMap<InspectingForm, UpdateInspectingForm>()
                .ReverseMap();
            CreateMap<InspectingResult, InspectingResultModel>()
                .ForMember(dest => dest.InspectingImageModels, opt => opt.MapFrom(src => src.InspectingImages))
                .ReverseMap();
            CreateMap<InspectingImage, InspectingImageModel>()
                .ReverseMap();
            CreateMap<InspectingResult, CreateInspectingResult>()
                .ReverseMap();
        }

        private void DeviceProfile()
        {
            CreateMap<Device, DeviceModels>()
                .ReverseMap();
            CreateMap<Device, CreateDevice>()
                .ReverseMap();
            CreateMap<Device, UpdateDevice>()
                .ReverseMap();
        }

        private void AuthProfile()
        {
            CreateMap<AccountModel, Account>()
                .ReverseMap();
            CreateMap<InfomationModel, Farmer>()
                .ReverseMap(); 
            CreateMap<InfomationModel, Retailer>()
                .ReverseMap();
            CreateMap<InfomationModel, Inspector>()
                .ReverseMap();
            CreateMap<InfomationModel, Expert>()
                .ReverseMap();
            CreateMap<Account, CreateFarmer>()
                .ReverseMap();
            CreateMap<Account, CreateRetailer>()
                .ReverseMap();
            CreateMap<Account, CreateInspector>()
                .ReverseMap();
        }

        private void PesticideProfie()
        {
            CreateMap<PesticideModel,CreatedPesticide>()
                .ReverseMap();
            CreateMap<PesticideModel,UpdatedPesticide>()
                .ReverseMap();
            CreateMap<PesticideModel, Pesticide>()
                .ReverseMap();
        }

        private void FertilizerProfile()
        {
            CreateMap<FertilizerModel, CreatedFertilizer>()
                .ReverseMap();
            CreateMap<FertilizerModel, UpdatedFertilizer>()
                .ReverseMap();
            CreateMap<FertilizerModel, Fertilizer>()
                .ReverseMap();
        }

        private void FarmerProfile()
        {
            CreateMap<FarmerPermission, FarmerPlan>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FarmerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Farmer.Avatar))
                .ReverseMap();
            CreateMap<FarmerPermission, PlanListFarmerAssignTo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Plan.Id))
                .ForMember(dest => dest.PlantId, opt => opt.MapFrom(src => src.Plan.PlantId))
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.Plan.Plant.PlantName))
                .ForMember(dest => dest.YieldId, opt => opt.MapFrom(src => src.Plan.YieldId))
                .ForMember(dest => dest.YieldName, opt => opt.MapFrom(src => src.Plan.Yield.YieldName))
                .ForMember(dest => dest.ExpertId, opt => opt.MapFrom(src => src.Plan.ExpertId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Plan.Expert.Account.Name))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Plan.Description))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Plan.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Plan.EndDate))
                .ForMember(dest => dest.PlanStatus, opt => opt.MapFrom(src => src.Plan.Status))
                .ForMember(dest => dest.EstimatedProduct, opt => opt.MapFrom(src => src.Plan.EstimatedProduct))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Plan.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Plan.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Plan.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.Plan.UpdatedBy))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.Plan.IsApproved))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToLower().Trim().Equals("active")))
                .ReverseMap();
            CreateMap<Farmer, FarmerModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Account.UpdatedAt))
                .ForMember(dest => dest.FarmerSpecials, opt => opt.MapFrom(src => src.FarmerSpecializations.Select(f => f.Specialization)))
                .ForMember(dest => dest.CompletedTasks, opt => opt.MapFrom(src => src.FarmerPerformance.CompletedTasks))
                .ForMember(dest => dest.IncompleteTasks, opt => opt.MapFrom(src => src.FarmerPerformance.IncompleteTasks))
                .ForMember(dest => dest.PerformanceScore, opt => opt.MapFrom(src => src.FarmerPerformance.PerformanceScore))
                .ReverseMap();
            CreateMap<Farmer, CreateFarmer>()
                .ReverseMap();
            CreateMap<Specialization, Special>()
                .ReverseMap();
        }

        private void SeedProfile()
        {
            CreateMap<Plant, PlantModel>()
                .ReverseMap();
            CreateMap<PlantModel, CreatedPlant>()
                .ReverseMap();
            CreateMap<PlantModel,UpdatedPlant>()
                .ReverseMap();
        }

        private void YieldProfile()
        {
            CreateMap<YieldModel, Yield>()
                .ReverseMap();
            CreateMap<YieldModel, CreatedYield>()
                .ReverseMap();
            CreateMap<YieldModel, UpdatedYield>()
                .ReverseMap();
        }

        private void InspectorProfile()
        {
            CreateMap<Inspector, InspectorModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Account.UpdatedAt))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
                .ReverseMap();
            CreateMap<Inspector, CreateInspector>()
                .ReverseMap();
        }

        private void ProblemProfile()
        {
            CreateMap<Problem, ProblemModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProblemImages))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.FarmerName, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<ProblemImage, Images>()
                .ReverseMap();
            CreateMap<Problem, ProblemPlan>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<Problem, CreateProblem>()
                .ReverseMap();
            CreateMap<Problem, UpdateProblem>()
                .ReverseMap();
        }

        private void PlanProfile()
        {
            CreateMap<Plan, PlanModel>()
                .ForMember(dest => dest.PlantInfor, opt => opt.MapFrom(src => src.Plant))
                .ForMember(dest => dest.YieldInfor, opt => opt.MapFrom(src => src.Yield))
                .ForMember(dest => dest.CaringTaskInfor, opt => opt.MapFrom(src => src.CaringTasks))
                .ForMember(dest => dest.InspectingInfors, opt => opt.MapFrom(src => src.InspectingForms))
                .ForMember(dest => dest.HarvestingInfors, opt => opt.MapFrom(src => src.HarvestingTasks))
                .ForMember(dest => dest.PackagingInfors, opt => opt.MapFrom(src => src.PackagingTasks))
                .ForMember(dest => dest.ProblemInfors, opt => opt.MapFrom(src => src.Problems))
                .ForMember(dest => dest.OrderInfor, opt => opt.MapFrom(src => src.OrderPlans))
                .ForMember(dest => dest.UrlAddress, opt => opt.MapFrom(src => src.PlanTransaction.UrlAddress))
                .ReverseMap();
            CreateMap<OrderPlan, OrderInfor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.PreOrderQuantity, opt => opt.MapFrom(src => src.Order.PreOrderQuantity))
                .ForMember(dest => dest.OrderPlanQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ReverseMap();
            CreateMap<Yield, YieldInfor>()
                .ReverseMap();
            CreateMap<Plant, PlantInfor>()
                .ReverseMap();
            CreateMap<CaringTask, PlanCaringInfor>()
                .ReverseMap();
            CreateMap<InspectingForm, PlanInspectingInfor>()
                .ReverseMap();
            CreateMap<HarvestingTask, PlanHarvestingInfor>()
                .ReverseMap();
            CreateMap<PackagingTask, PlanPackagingInfor>()
                .ReverseMap();
            CreateMap<Problem, ProblemInfor>()
                .ReverseMap();
            CreateMap<Plan, PlanForList>()
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.Plant.PlantName))
                .ForMember(dest => dest.YieldName, opt => opt.MapFrom(src => src.Yield.YieldName))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Expert.Account.Name))
                .ForMember(dest => dest.UrlAddress, opt => opt.MapFrom(src => src.PlanTransaction.UrlAddress))
                .ReverseMap();
            CreateMap<Plan, PlanGeneral>()
                .ForMember(dest => dest.PlantInformation, opt => opt.MapFrom(src => src.Plant))
                .ForMember(dest => dest.YieldInformation, opt => opt.MapFrom(src => src.Yield))
                .ForMember(dest => dest.ExpertInformation, opt => opt.MapFrom(src => src.Expert))
                .ReverseMap();
            CreateMap<Expert, ExpertInformation>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
                .ReverseMap();
            CreateMap<Plant, PlantInformation>()
                .ReverseMap();
            CreateMap<Yield, YieldInformation>()
                .ReverseMap();
            CreateMap<Plan, AssigningPlan>()
                .ReverseMap();
            CreateMap<Plan, CreatePlan>()
                .ReverseMap();
            CreateMap<Plan, CreatePlanTemplate>()
                .ReverseMap();
            CreateMap<CaringTask, PlanCare>()
                .ForMember(dest => dest.Fertilizers, opt => opt.MapFrom(src => src.CaringFertilizers))
                .ForMember(dest => dest.Pesticides, opt => opt.MapFrom(src => src.CaringPesticides))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CaringItems))
                .ReverseMap();
            CreateMap<HarvestingTask, PlanHar>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.HarvestingItems))
                .ReverseMap();
            CreateMap<InspectingForm, PlanForm>()
                .ReverseMap();
            CreateMap<PackagingTask, PlanPack>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.PackagingItems))
                .ReverseMap();
            CreateMap<Plan, PlanOrderModel>()
                .ForMember(dest => dest.OrderPlas, opt => opt.MapFrom(src => src.OrderPlans))
                .ReverseMap();
            CreateMap<OrderPlan, OrPl>()
                .ForMember(dest => dest.RetailerId, opt => opt.MapFrom(src => src.Order.RetailerId))
                .ForMember(dest => dest.RetailerName, opt => opt.MapFrom(src => src.Order.Retailer.Account.Name))
                .ForMember(dest => dest.PreOrderQuantity, opt => opt.MapFrom(src => src.Order.PreOrderQuantity))
                .ForMember(dest => dest.EstimatedPickupDate, opt => opt.MapFrom(src => src.Order.EstimatedPickupDate))
                .ForMember(dest => dest.PackagingTypeId, opt => opt.MapFrom(src => src.Order.PackagingTypeId))
                .ForMember(dest => dest.PackagingTypeName, opt => opt.MapFrom(src => src.Order.PackagingType.Name))
                .ReverseMap();
        }

        private void CaringProfile()
        {
            CreateMap<CaringTask, CaringTaskModel>()
                .ForMember(dest => dest.CarePesticides,otp=>otp.MapFrom(src=>src.CaringPesticides))
                .ForMember(dest => dest.CareImages,otp=>otp.MapFrom(src=>src.CaringImages))
                .ForMember(dest => dest.CareFertilizers,otp=>otp.MapFrom(src=>src.CaringFertilizers))
                .ForMember(dest => dest.CareItems, opt => opt.MapFrom(src => src.CaringItems))
                .ForMember(dest => dest.FarmerInfor, opt => opt.MapFrom(src => src.FarmerCaringTasks))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.ProblemName, opt => opt.MapFrom(src => src.Problem.ProblemName))
                .ForMember(dest => dest.FarmerId, opt => opt.MapFrom(src => src.FarmerCaringTasks.FirstOrDefault(fc => fc.Status.ToLower().Trim().Equals("active")).FarmerId))
                .ReverseMap();
            CreateMap<FarmerCaringTask, FarmerInfor>()
                .ForMember(dest => dest.FarmerId, opt => opt.MapFrom(src => src.FarmerId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<CaringFertilizer, CareFertilizerModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Fertilizer.Name))
                .ReverseMap();
            CreateMap<CaringPesticide, CarePesticideModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Pesticide.Name))
                .ReverseMap();
            CreateMap<CaringImage, CaringImageModel>()
                .ReverseMap();
            CreateMap<CaringTask, CreateCaringPlan>()
                .ReverseMap();
            CreateMap<CaringItem, CareItemsModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
                .ReverseMap();
            CreateMap<CaringTask, UpdateCaringTask>()
                .ReverseMap();
            CreateMap<CaringTask, CaringTaskReport>()
                .ReverseMap();
            CreateMap<ItemCare, CaringItem>()
                .ReverseMap();
            CreateMap<FerCare, CaringFertilizer>()
                .ReverseMap();
            CreateMap<PesCare, CaringPesticide>()
                .ReverseMap();
        }

        private void HarvestingProfile()
        {
            CreateMap<HarvestingTask, HarvestingTaskModel>()
                .ForMember(dest => dest.HarvestImages, otp => otp.MapFrom(src => src.HarvestingImages))
                .ForMember(dest => dest.HarvestingItems, otp => otp.MapFrom(src => src.HarvestingItems))
                .ForMember(dest => dest.FarmerInfor, opt => opt.MapFrom(src => src.FarmerHarvestingTasks))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.FarmerId, opt => opt.MapFrom(src => src.FarmerHarvestingTasks.FirstOrDefault(x => x.Status.ToLower().Trim().Equals("active")).FarmerId))
                .ReverseMap();
            CreateMap<FarmerHarvestingTask, FarmerInfor>()
                .ForMember(dest => dest.FarmerId, opt => opt.MapFrom(src => src.FarmerId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<HarvestingImage, HarvestingImageModel>()
                .ReverseMap();
            CreateMap<HarvestingItem, HarvestingItemModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
                .ReverseMap();     
            CreateMap<HarvestingTask, HarvestingTaskReport>()
                .ReverseMap();
            CreateMap<HarvestingTask, UpdateHarvestingTask>()
                .ReverseMap();
            CreateMap<HarvestingTask, CreateHarvestingPlan>()
                .ReverseMap();
            CreateMap<HarvestingTask, HarvestingProductionModel>()
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.PlantId, opt => opt.MapFrom(src => src.Plan.Plant.Id))
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.Plan.Plant.PlantName))
                .ForMember(dest => dest.EvaluatedResult, opt => opt.MapFrom(src => src.Plan.InspectingForms.OrderByDescending(f => f.CompleteDate).FirstOrDefault().InspectingResult.EvaluatedResult))
                .ReverseMap();
            CreateMap<HarvestItem, HarvestingItem>()
                .ReverseMap();
        }

        private void PackagingProfile()
        {
            CreateMap<PackagingTask, PackagingTaskModel>()
                .ForMember(dest => dest.PackageImages, opt => opt.MapFrom(src => src.PackagingImages))
                .ForMember(dest => dest.PackageItems, opt => opt.MapFrom(src => src.PackagingItems))
                .ForMember(dest => dest.FarmerInfor, opt => opt.MapFrom(src => src.FarmerPackagingTasks))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.PlanName))
                .ForMember(dest => dest.FarmerId, opt => opt.MapFrom(src => src.FarmerPackagingTasks.FirstOrDefault(x => x.Status.Trim().ToLower().Equals("active")).FarmerId))
                .ForMember(dest => dest.RetailerId, opt => opt.MapFrom(src => src.Order.RetailerId))
                .ForMember(dest => dest.RetailerName, opt => opt.MapFrom(src => src.Order.Retailer.Account.Name))
                .ForMember(dest => dest.PreOrderQuantity, opt => opt.MapFrom(src => src.Order.PreOrderQuantity))
                .ReverseMap();
            CreateMap<FarmerPackagingTask, FarmerInfor>()
                .ForMember(dest => dest.FarmerId, opt => opt.MapFrom(src => src.FarmerId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<PackagingImage, PackagingImageModel>()
                .ReverseMap();
            CreateMap<PackagingItem, PackagingItemModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item.Name))
                .ReverseMap();
            CreateMap<PackagingTask, PackagingReport>()
                .ReverseMap();
            CreateMap<PackagingTask, UpdatePackaging>()
                .ReverseMap();
            CreateMap<PackagingTask, CreatePackagingPlan>()
                .ReverseMap();
            CreateMap<PackagingType, PackagingTypeModel>()
                .ReverseMap();
            CreateMap<PackageItem, PackagingItem>()
                .ReverseMap();
        }

        private void InspectingResultProfile()
        {
            CreateMap<InspectingResult, InspectingResultModel>()
                .ForMember(dest => dest.InspectingImageModels, opt => opt.MapFrom(src => src.InspectingImages))
                .ReverseMap();
            CreateMap<InspectingResult, CreateInspectingResult>()
                .ReverseMap();
        }

        private void HistoryFarmerProfile()
        {
            CreateMap<FarmerCaringTask, HistoryFarmersTask>()
                .ForMember(dest => dest.FarmerName, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<FarmerHarvestingTask, HistoryFarmersTask>()
                .ForMember(dest => dest.FarmerName, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<FarmerPackagingTask, HistoryFarmersTask>()
                .ForMember(dest => dest.FarmerName, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
        }

        private void ItemProfile()
        {
            CreateMap<Item, ItemModel>()
                .ReverseMap();
        }

        private void NotificationProfile()
        {
            CreateMap<NotificationFarmer, FarmerNotificationsModel>()
                .ReverseMap();
        }

        private void TransactionProfile()
        {
            CreateMap<Transaction, TransactionModel>()
                .ReverseMap();
        }

        private void ProductionProfile()
        {
            CreateMap<PackagingProduct, PackagingProductionModel>()
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PackagingTask.PlanId))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.PackagingTask.Plan.PlanName))
                .ForMember(dest => dest.PlantId, opt => opt.MapFrom(src => src.PackagingTask.Plan.PlantId))
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.PackagingTask.Plan.Plant.PlantName))
                .ForMember(dest => dest.CompleteDate, opt => opt.MapFrom(src => src.PackagingTask.CompleteDate))
                .ForMember(dest => dest.ProductExpiredDate, opt => opt.MapFrom(src => src.HarvestingTask.ProductExpiredDate))
                .ForMember(dest => dest.EvaluatedResult, opt => opt.MapFrom(src => src.PackagingTask.Plan.InspectingForms.OrderByDescending(f => f.CompleteDate).FirstOrDefault().InspectingResult.EvaluatedResult))
                .ForMember(dest => dest.AvailablePacks, opt => opt.MapFrom(src => src.PackQuantity - src.ProductPickupBatches.Sum(c => c.Quantity)))
                .ForMember(dest => dest.PackagingTypeId, opt => opt.MapFrom(src => src.PackagingTask.PackagingTypeId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.PackagingTask.OrderId))
                .ForMember(dest => dest.RetailerId, opt => opt.MapFrom(src => src.PackagingTask.Order.RetailerId))
                .ForMember(dest => dest.RetailerName, opt => opt.MapFrom(src => src.PackagingTask.Order.Retailer.Account.Name))
                .ForMember(dest => dest.QuantityOfPacks, opt => opt.MapFrom(src => src.ProductPickupBatches.Sum(c => c.Quantity)))
                .ReverseMap();
        }
    }
}