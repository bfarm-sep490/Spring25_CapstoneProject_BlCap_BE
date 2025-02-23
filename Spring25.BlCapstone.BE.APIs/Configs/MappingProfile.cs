using AutoMapper;
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
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Havest;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
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
       }

        private void InspectingProfile()
        {
            CreateMap<InspectingForm, InspectingFormModel>()
                .ForMember(x => x.InspectingItems, otp => otp.MapFrom(x => x.InspectingItems))
                .ForMember(x => x.InspectingImages, otp => otp.MapFrom(x => x.InspectingImages))
                .ReverseMap();
            CreateMap<InspectingImage, InspectingImageModel>()
                .ReverseMap();
            CreateMap <InspectingItem, InspectingItemModel>()
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
                .ForMember(dest => dest.YieldInfor, opt => opt.MapFrom(src => src.Yield))
                .ForMember(dest => dest.CaringTaskInfor, opt => opt.MapFrom(src => src.CaringTasks))
                .ForMember(dest => dest.InspectingInfors, opt => opt.MapFrom(src => src.InspectingForms))
                .ForMember(dest => dest.HarvestingInfors, opt => opt.MapFrom(src => src.HarvestingTasks))
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
            CreateMap<Problem, ProblemInfor>()
                .ReverseMap();
            CreateMap<Plan, PlanForList>()
                .ReverseMap();
        }
        void CaringProfile()
        {
            CreateMap<CaringTask,CaringTaskModel>()
                .ForMember(dest=>dest.CarePesticides,otp=>otp.MapFrom(src=>src.CaringPesticides))
                .ForMember(dest=>dest.CareImages,otp=>otp.MapFrom(src=>src.CaringImages))
                .ForMember(dest=>dest.CareFertilizers,otp=>otp.MapFrom(src=>src.CaringFertilizers))
                .ReverseMap();
            CreateMap<CareFertilizerModel, CaringFertilizer>()
                .ReverseMap();
            CreateMap<CarePesticideModel, CaringPesticide>()
                .ReverseMap();
            CreateMap<CaringImage, CaringImageModel>()
                .ReverseMap();
        }
        void HarvestingProfile()
        {
            CreateMap<HarvestingTask, HarvestingTaskModel>()
                .ForMember(dest => dest.HarvestImages, otp => otp.MapFrom(src => src.HarvestingImages))
                .ForMember(dest => dest.HarvestingItems, otp => otp.MapFrom(src => src.HarvestingItems))
                .ReverseMap();
            CreateMap<HarvestingImage, HarvestingImageModel>()
                .ReverseMap();
            CreateMap<HarvestingItem, HarvestingItemModel>()
                .ReverseMap();              
        }

        void IssueProfile()
        {
            CreateMap<Issue, IssueModel>()
                .ReverseMap();
        }
    }
}
