using Microsoft.Extensions.Configuration;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IFarmerService
    {

    }

    public class FarmerService : IFarmerService
    {
        private UnitOfWork _unitOfWork;
        public FarmerService()
        {
            _unitOfWork ??= new UnitOfWork();
        }
    }
}
