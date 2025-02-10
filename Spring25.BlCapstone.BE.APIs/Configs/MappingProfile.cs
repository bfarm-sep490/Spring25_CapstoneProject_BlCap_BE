using AutoMapper;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
using Spring25.BlCapstone.BE.APIs.RequestModels.Seed;
using Spring25.BlCapstone.BE.APIs.RequestModels.Yield;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
using Spring25.BlCapstone.BE.Services.BusinessModels.Seed;
using Spring25.BlCapstone.BE.Services.BusinessModels.Yield;
using System.Xml.Serialization;

namespace Spring25.BlCapstone.BE.APIs.Configs
{
    public class MappingProfile: Profile
    {
       public MappingProfile()
       {
            PesticideProfie();
            FertilizerProfile();
            FarmerProfile();
            SeedProfile();
            YieldProfile();
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
            CreateMap<Seed, SeedModel>()
                .ReverseMap();
            CreateMap<SeedModel,CreatedSeed>()
                .ReverseMap();
            CreateMap<SeedModel,UpdatedSeed>()
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
    }
}
