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
        Task<IBusinessResult> MarkAsRead(int id);
        Task<IBusinessResult> MarkAllAsRead();
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
                    return new BusinessResult(400, "There aren't any notifications !");
                }

                var res = _mapper.Map<List<OwnerNotificationsModel>>(notis);
                return new BusinessResult(200, "List notifications :", res.OrderByDescending(c => c.Id));
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAsRead(int id)
        {
            try
            {
                var noti = await _unitOfWork.NotificationOwnerRepository.GetByIdAsync(id);
                if (noti == null)
                {
                    return new BusinessResult(400, "Not found this notifications");
                }

                noti.IsRead = true;
                await _unitOfWork.NotificationOwnerRepository.UpdateAsync(noti);
                return new BusinessResult(200, "Mark as read successfully!");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> MarkAllAsRead()
        {
            try
            {
                var notis = await _unitOfWork.NotificationOwnerRepository.GetAllAsync();
                notis.ForEach(n =>
                {
                    n.IsRead = true;
                    _unitOfWork.NotificationOwnerRepository.PrepareUpdate(n);
                });

                await _unitOfWork.NotificationOwnerRepository.SaveAsync();

                return new BusinessResult(200, "Mark all as read successfully !");
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
