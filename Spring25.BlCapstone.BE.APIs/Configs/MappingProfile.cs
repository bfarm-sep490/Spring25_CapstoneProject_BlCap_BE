﻿using AutoMapper;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
using Spring25.BlCapstone.BE.APIs.RequestModels.Plant;
using Spring25.BlCapstone.BE.APIs.RequestModels.Yield;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Auth;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.BusinessModels.Issue;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Care;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Harvest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Package;
using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;
using System.Xml.Serialization;

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
            IssueProfile();
            PackagingProfile();
        }

        private void InspectingProfile()
        {
            CreateMap<InspectingForm, InspectingFormModel>()
                .ForMember(x => x.InspectingImages, otp => otp.MapFrom(x => x.InspectingImages))
                .ForMember(dest => dest.InspectorName, otp => otp.MapFrom(src => src.Inspector.Account.Name))
                .ReverseMap();
            CreateMap<InspectingImage, InspectingImageModel>()
                .ReverseMap();
            CreateMap<InspectingForm, CreateInspectingPlan>()
                .ReverseMap();
            CreateMap<InspectingForm, UpdateInspectingForm>()
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
            CreateMap<Farmer, FarmerModel>()
                .ReverseMap();
            CreateMap<Farmer, FarmerPlan>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
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
        }

        private void ProblemProfile()
        {
            CreateMap<Problem, ProblemModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProblemImages))
                .ReverseMap();
            CreateMap<ProblemImage, Images>()
                .ReverseMap();
            CreateMap<Problem, ProblemPlan>()
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
        }

        private void CaringProfile()
        {
            CreateMap<CaringTask,CaringTaskModel>()
                .ForMember(dest=>dest.CarePesticides,otp=>otp.MapFrom(src=>src.CaringPesticides))
                .ForMember(dest=>dest.CareImages,otp=>otp.MapFrom(src=>src.CaringImages))
                .ForMember(dest=>dest.CareFertilizers,otp=>otp.MapFrom(src=>src.CaringFertilizers))
                .ForMember(dest => dest.CareItems, opt => opt.MapFrom(src => src.CaringItems))
                .ForMember(dest => dest.FarmerName,otp => otp.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<CareFertilizerModel, CaringFertilizer>()
                .ReverseMap();
            CreateMap<CarePesticideModel, CaringPesticide>()
                .ReverseMap();
            CreateMap<CaringImage, CaringImageModel>()
                .ReverseMap();
            CreateMap<CaringTask, CreateCaringPlan>()
                .ReverseMap();
            CreateMap<CaringItem, CareItemsModel>()
                .ReverseMap();
            CreateMap<CaringTask, UpdateCaringTask>()
                .ReverseMap();
            CreateMap<CaringTask, CaringTaskReport>()
                .ReverseMap();
        }

        private void HarvestingProfile()
        {
            CreateMap<HarvestingTask, HarvestingTaskModel>()
                .ForMember(dest => dest.HarvestImages, otp => otp.MapFrom(src => src.HarvestingImages))
                .ForMember(dest => dest.HarvestingItems, otp => otp.MapFrom(src => src.HarvestingItems))
                .ForMember(dest => dest.FarmerName, otp => otp.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<HarvestingImage, HarvestingImageModel>()
                .ReverseMap();
            CreateMap<HarvestingItem, HarvestingItemModel>()
                .ReverseMap();     
            CreateMap<HarvestingTask, HarvestingTaskReport>()
                .ReverseMap();
            CreateMap<HarvestingTask, UpdateHarvestingTask>()
                .ReverseMap();
            CreateMap<HarvestingTask, CreateHarvestingPlan>()
                .ReverseMap();
        }

        private void PackagingProfile()
        {
            CreateMap<PackagingTask, PackagingTaskModel>()
                .ForMember(dest => dest.PackageImages, opt => opt.MapFrom(src => src.PackagingImages))
                .ForMember(dest => dest.PackageItems, opt => opt.MapFrom(src => src.PackagingItems))
                .ForMember(dest => dest.FarmerName, opt => opt.MapFrom(src => src.Farmer.Account.Name))
                .ReverseMap();
            CreateMap<PackagingImage, PackagingImageModel>()
                .ReverseMap();
            CreateMap<PackagingItem, PackagingItemModel>()
                .ReverseMap();
            CreateMap<PackagingTask, PackagingReport>()
                .ReverseMap();
            CreateMap<PackagingTask, UpdatePackaging>()
                .ReverseMap();
            CreateMap<PackagingTask, CreatePackagingPlan>()
                .ReverseMap();
        }

        private void IssueProfile()
        {
            CreateMap<Issue, IssueModel>()
                .ReverseMap();
        }
    }
}