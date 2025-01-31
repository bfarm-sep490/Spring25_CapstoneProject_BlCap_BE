﻿using AutoMapper;
using Spring25.BlCapstone.BE.APIs.RequestModels.Fertilizer;
using Spring25.BlCapstone.BE.APIs.RequestModels.Pesticide;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.BusinessModels.Farmer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Fertilizer;
using Spring25.BlCapstone.BE.Services.BusinessModels.Pesticide;
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
       }
       void PesticideProfie()
       {
            CreateMap<PesticideModel,CreatedPesticide>()
                .ReverseMap();
            CreateMap<PesticideModel,UpdatedPesticide>()
                .ReverseMap();
            /*CreateMap<PesticideModel, Pesticide>()
                .ReverseMap();*/
       }
       void FertilizerProfile()
       {
            CreateMap<FertilizerModel, CreatedFertilizer>()
                .ReverseMap();
            CreateMap<FertilizerModel, UpdatedFertilizer>()
                .ReverseMap();
            /*CreateMap<FertilizerModel,Fertilizer>()
                .ReverseMap();*/
       }
        void FarmerProfile()
        {
            CreateMap<Farmer, FarmerModel>()
                .ReverseMap();
        }
    }
}
