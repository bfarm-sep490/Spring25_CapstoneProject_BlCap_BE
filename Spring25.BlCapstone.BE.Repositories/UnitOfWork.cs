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

        public UnitOfWork()
        {
            _farmOwnerRepository ??= new FarmOwnerRepository();
            _fieldRepository ??= new FieldRepository();
            _imageFieldRepository ??= new ImageFieldRepository();
        }
        public UnitOfWork(FarmOwnerRepository farmOwnerRepository, FieldRepository fieldRepository, ImageFieldRepository imageFieldRepository)
        {
            _farmOwnerRepository = farmOwnerRepository;
            _fieldRepository = fieldRepository;
            _imageFieldRepository = imageFieldRepository;
        }

        public FarmOwnerRepository FarmOwnerRepository { get { return _farmOwnerRepository; } }
        public FieldRepository FieldRepository { get { return _fieldRepository; } }
        public ImageFieldRepository ImageFieldRepository { get { return _imageFieldRepository; } }
    }
}
