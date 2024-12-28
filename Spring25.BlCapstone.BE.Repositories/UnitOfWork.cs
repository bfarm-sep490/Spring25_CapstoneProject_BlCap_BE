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

        public UnitOfWork()
        {
            _farmOwnerRepository ??= new FarmOwnerRepository();
        }
        public UnitOfWork(FarmOwnerRepository farmOwnerRepository)
        {
            _farmOwnerRepository = farmOwnerRepository;
        }

        public FarmOwnerRepository FarmOwnerRepository { get { return _farmOwnerRepository; } }
    }
}
