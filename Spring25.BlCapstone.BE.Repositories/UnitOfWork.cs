using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories
{
    public class UnitOfWork
    {
        private FarmOwnerRepository _farmOwnerRepository;
        private PesticideRepository _pesticideRepository;
        private FertilizerRepository _ferilizerRepository;
        public UnitOfWork()
        {
            _farmOwnerRepository ??= new FarmOwnerRepository();
            _pesticideRepository ??=new PesticideRepository();
            _ferilizerRepository ??= new FertilizerRepository(); 
        }
        public UnitOfWork(FarmOwnerRepository farmOwnerRepository,PesticideRepository pesticideRepository,FertilizerRepository fertilizerRepository)
        {
            _farmOwnerRepository = farmOwnerRepository;
            _pesticideRepository = pesticideRepository;
            _ferilizerRepository = fertilizerRepository;
        }
        public PesticideRepository PesticideRepository { get { return _pesticideRepository; } }
        public FarmOwnerRepository FarmOwnerRepository { get { return _farmOwnerRepository; } }
        public FertilizerRepository FertilizerRepository { get { return _ferilizerRepository; } }
    }
}
