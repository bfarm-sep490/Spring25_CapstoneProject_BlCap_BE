using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spring25.BlCapstone.BE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BackgroundServices.BackgroundServices
{
    public class TaskChangeStatusPlanByOrder : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TaskChangeStatusPlanByOrder> _logger;

        public TaskChangeStatusPlanByOrder(IServiceScopeFactory serviceScopeFactory, ILogger<TaskChangeStatusPlanByOrder> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Check expired Order status started !");
            _timer = new System.Timers.Timer(TimeSpan.FromDays(1).TotalMilliseconds);
            _timer.Elapsed += async (sender, args) => await ProcessExpireOrder();
            _timer.AutoReset = true;
            _timer.Enabled = true;
            return Task.CompletedTask;
        }

        private async Task ProcessExpireOrder()
        {
            _logger.LogInformation("Checking plan do not have order...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                try
                {
                    var orders = await unitOfWork.OrderRepository.GetAllOrderPendingHasNoOrder();
                    var forfeitOrders = await unitOfWork.OrderRepository.GetAllOrderReachPickupDate();

                    foreach (var order in orders)
                    {
                        var plant = await unitOfWork.PlantRepository.GetByIdAsync(order.PlantId);
                        if (DateTime.Now.AddDays(plant.AverageDurationDate) < order.EstimatedPickupDate)
                        {
                            order.Status = "Cancel";
                            unitOfWork.OrderRepository.PrepareUpdate(order);
                        }
                    }

                    foreach (var order in forfeitOrders)
                    {
                        order.Status = "Forfeit";
                        unitOfWork.OrderRepository.PrepareUpdate(order);
                    }

                    await unitOfWork.OrderRepository.SaveAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Order update fail: {ex.Message}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");
            _timer?.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
