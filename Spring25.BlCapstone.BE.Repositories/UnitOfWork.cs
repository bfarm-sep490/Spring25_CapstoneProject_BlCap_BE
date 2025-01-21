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
        private AccountRepository _accountRepository;
        private FarmerRepository _farmerRepository;
        private ExpertRepository _expertRepository;
        private ItemRepository _itemRepository;

        public UnitOfWork()
        {
            _accountRepository ??= new AccountRepository();
            _farmerRepository ??= new FarmerRepository();
            _expertRepository ??= new ExpertRepository();
            _itemRepository ??= new ItemRepository();
        }

        public UnitOfWork(AccountRepository accountRepository, FarmerRepository farmerRepository,
            ExpertRepository expertRepository, ItemRepository itemRepository)
        {
            _accountRepository = accountRepository;
            _farmerRepository = farmerRepository;
            _expertRepository = expertRepository;
            _itemRepository = itemRepository;
        }

        public AccountRepository AccountRepository { get { return _accountRepository; } }
        public FarmerRepository FarmerRepository { get { return _farmerRepository; } }
        public ExpertRepository ExpertRepository { get { return _expertRepository; } }
        public ItemRepository ItemRepository { get { return _itemRepository; } }
    }
}
