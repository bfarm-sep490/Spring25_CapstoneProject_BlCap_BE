﻿using AutoMapper;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
using Spring25.BlCapstone.BE.APIs.RequestModels.Plant;
using Spring25.BlCapstone.BE.APIs.RequestModels.Yield;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Inspector;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plan;
using Spring25.BlCapstone.BE.Services.BusinessModels.Plant;
using Spring25.BlCapstone.BE.Services.BusinessModels.Problem;
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
       }

       void PesticideProfie()
       {
            CreateMap<PesticideModel,CreatedPesticide>()
                .ReverseMap();
            CreateMap<PesticideModel,UpdatedPesticide>()
                .ReverseMap();
            CreateMap<PesticideModel, Pesticide>()
                .ReverseMap();
       }

       void FertilizerProfile()
       {
            CreateMap<FertilizerModel, CreatedFertilizer>()
                .ReverseMap();
            CreateMap<FertilizerModel, UpdatedFertilizer>()
                .ReverseMap();
            CreateMap<FertilizerModel, Fertilizer>()
                .ReverseMap();
       }

        void FarmerProfile()
        {
            CreateMap<Farmer, FarmerModel>()
                .ReverseMap();
        }

        void SeedProfile()
        {
            CreateMap<Plant, PlantModel>()
                .ReverseMap();
            CreateMap<PlantModel, CreatedPlant>()
                .ReverseMap();
            CreateMap<PlantModel,UpdatedPlant>()
                .ReverseMap();
        }

        void YieldProfile()
        {
            CreateMap<YieldModel, Yield>()
                .ReverseMap();
            CreateMap<YieldModel, CreatedYield>()
                .ReverseMap();
            CreateMap<YieldModel, UpdatedYield>()
                .ReverseMap();
        }

        void InspectorProfile()
        {
            CreateMap<Inspector, InspectorModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Account.Password))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Account.IsActive))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.Account.UpdatedAt))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Account.CreatedAt))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Account.Name))
                .ReverseMap();
        }

        void ProblemProfile()
        {
            CreateMap<Problem, ProblemModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProblemImages))
                .ForMember(dest => dest.ProblemIssues, opt => opt.MapFrom(src => src.Issues))
                .ReverseMap();
            CreateMap<ProblemImage, Images>()
                .ReverseMap();
            CreateMap<Issue, ProblemIssues>()
                .ReverseMap();
        }

        void PlanProfile()
        {
            CreateMap<Plan, PlanModel>()
                .ForMember(dest => dest.PlantInfor, opt => opt.MapFrom(src => src.Plant))
                .ForMember(dest => dest.YieldInfor, opt => opt.MapFrom(src => src.YieldPlans))
                .ForMember(dest => dest.CaringTaskInfor, opt => opt.MapFrom(src => src.CaringTasks))
                .ForMember(dest => dest.InspectingInfors, opt => opt.MapFrom(src => src.InspectingForms))
                .ForMember(dest => dest.HarvestingInfors, opt => opt.MapFrom(src => src.HarvestingTasks))
                .ForMember(dest => dest.ProblemInfors, opt => opt.MapFrom(src => src.Problems))
                .ReverseMap();
            CreateMap<Plant, PlantInfor>()
                .ReverseMap();
            CreateMap<YieldPlan, YieldInfor>()
                .ForMember(dest => dest.YieldName, opt => opt.MapFrom(src => src.Yield.YieldName))
                .ForMember(dest => dest.AreaUnit, opt => opt.MapFrom(src => src.Yield.AreaUnit))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Yield.Area))
                .ReverseMap();
            CreateMap<CaringTask, PlanCaringInfor>()
                .ReverseMap();
            CreateMap<InspectingForm, PlanInspectingInfor>()
                .ReverseMap();
            CreateMap<HarvestingTask, PlanHarvestingInfor>()
                .ReverseMap();
            CreateMap<Problem, ProblemInfor>()
                .ReverseMap();
            CreateMap<Plan, PlanForList>()
                .ReverseMap();
        }
    }
}
