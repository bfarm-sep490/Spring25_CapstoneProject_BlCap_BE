﻿using Newtonsoft.Json.Linq;
using Spring25.BlCapstone.BE.Repositories.Helper;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IAblyService
    {
        Task<IBusinessResult> SendNotification(string title, string body);
        Task<IBusinessResult> SendMessageWithTopic(string title, string body, string topic);
        Task<IBusinessResult> SendMessageToDeviceToken(NotificationDeviceRequest model, string deviceId);
    }

    public class AblyService : IAblyService
    {
        private readonly AblyHelper _ably;
        public AblyService()
        {
            _ably ??= new AblyHelper();
        }

        public async Task<IBusinessResult> SendNotification(string title, string body)
        {
            try
            {
                var res = await _ably.SendNotificationAsync(title, body);
                return new BusinessResult
                {
                    Status = 200,
                    Message = res,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = "Have bug?",
                    Data = ex.Message
                };
            }
        }

        public async Task<IBusinessResult> SendMessageWithTopic(string title, string body, string topic)
        {
            try
            {
                var res = await _ably.SendMessageWithTopic(title, body, topic);
                return new BusinessResult
                {
                    Status = 200,
                    Message = res,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BusinessResult
                {
                    Status = 500,
                    Message = "Have bug?",
                    Data = ex.Message
                };
            }
        }

        public async Task<IBusinessResult> SendMessageToDeviceToken(NotificationDeviceRequest model, string deviceId)
        {
            try
            {
                string res;
                if (model.Data == null)
                {
                    res = await _ably.SendMessageToDevice(model.Title, model.Body, deviceId);
                }
                else
                {
                    res = await _ably.SendMessageToDevice(model.Title, model.Body, deviceId, model.Data);
                }

                return new BusinessResult(200, res, null);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, "Have bugs?", ex.Message);
            }
        }
    }
}