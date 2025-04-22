using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IOwnerService
    {
        Task<IBusinessResult> GetListNotifications();
    }

    public class OwnerService : IOwnerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OwnerService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetListNotifications()
        {
            try
            {
                var notis = await _unitOfWork.NotificationOwnerRepository.GetAllAsync();
                if (!notis.Any())
                {
                    return new BusinessResult(404, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<OwnerNotificationsModel>>(notis);
                return new BusinessResult(200, "List notifications :", res);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
