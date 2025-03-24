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

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Check expired Task status started !");
            _timer = new System.Timers.Timer(TimeSpan.FromDays(1).TotalMilliseconds);
            _timer.Elapsed += async (sender, args) => await ProcessExpirePlan();
            _timer.AutoReset = true;
            _timer.Enabled = true;
            return Task.CompletedTask;
        }

        private async Task ProcessExpirePlan()
        {
            _logger.LogInformation("Checking plan do not have order...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                try
                {
                    var expirePlan = await unitOfWork.PlanRepository.GetPlanNotHaveOrderOrHaveOnlyOrdersCancle();
                    if (expirePlan != 0)
                    {
                        _logger.LogInformation($"Change {expirePlan} plans to Cancel");
                    }
                    else
                    {
                        _logger.LogInformation($"Do not have plans to change");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Plan update fail: {ex.Message}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");
            _timer?.Stop();
            return Task.CompletedTask;
        }
    }
}
