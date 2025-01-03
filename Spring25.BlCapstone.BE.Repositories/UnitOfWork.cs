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
        private FieldRepository _fieldRepository;
        private ImageFieldRepository _imageFieldRepository;
        private PesticideRepository _pesticideRepository;
        private FertilizerRepository _ferilizerRepository;
        public UnitOfWork()
        {
            _farmOwnerRepository ??= new FarmOwnerRepository();
            _pesticideRepository ??=new PesticideRepository();
            _ferilizerRepository ??= new FertilizerRepository();
            _fieldRepository ??= new FieldRepository();
            _imageFieldRepository ??= new ImageFieldRepository();
          
        }
        public UnitOfWork(
          FarmOwnerRepository farmOwnerRepository,
          PesticideRepository pesticideRepository,
          FertilizerRepository fertilizerRepository,
          FieldRepository fieldRepository,
          ImageFieldRepository imageFieldRepository)
        {
            _farmOwnerRepository = farmOwnerRepository;
            _pesticideRepository = pesticideRepository;
            _ferilizerRepository = fertilizerRepository;
            _fieldRepository = fieldRepository;
            _imageFieldRepository = imageFieldRepository;
        }
        public PesticideRepository PesticideRepository { get { return _pesticideRepository; } }
        public FarmOwnerRepository FarmOwnerRepository { get { return _farmOwnerRepository; } }
        public FieldRepository FieldRepository { get { return _fieldRepository; } }
        public ImageFieldRepository ImageFieldRepository { get { return _imageFieldRepository; } }
        public FertilizerRepository FertilizerRepository { get { return _ferilizerRepository; } }
    }
}
