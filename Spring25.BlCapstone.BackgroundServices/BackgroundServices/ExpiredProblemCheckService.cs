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
    public class ExpiredProblemCheckService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ExpiredProblemCheckService> _logger;

        public ExpiredProblemCheckService(IServiceScopeFactory serviceScopeFactory, ILogger<ExpiredProblemCheckService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Check expired product status started !");

            _timer = new System.Timers.Timer(TimeSpan.FromMinutes(30).TotalMilliseconds);
            _timer.Elapsed += async (sender, args) => await ProcessExpiredProblems();
            _timer.AutoReset = true;
            _timer.Enabled = true;

            return Task.CompletedTask;
        }

        private async Task ProcessExpiredProblems()
        {
            _logger.LogInformation("Checking ...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                try
                {
                    var expiredProblems = await unitOfWork.ProblemRepository.GetAllProblemsExpired();

                    try
                    {
                        if (expiredProblems.Any())
                        {
                            foreach (var problem in expiredProblems)
                            {
                                problem.Status = "Cancel";
                                problem.ResultContent = "Đã vượt quá ngày giải quyết vấn đề, hãy kiểm tra lại vấn đề tại khu đất của kế hoạch !";
                                unitOfWork.ProblemRepository.PrepareUpdate(problem);
                            }

                            await unitOfWork.ProblemRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Problem update expired error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Error in updating problem!");
                }
            }
        }
        public Task StopAsync(CancellationToken cancelToken)
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
