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
    public class ExpiredProductCheckStatusService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ExpiredProductCheckStatusService> _logger;

        public ExpiredProductCheckStatusService(IServiceScopeFactory serviceScopeFactory, ILogger<ExpiredProductCheckStatusService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Check expired product status started !");

            _timer = new System.Timers.Timer(TimeSpan.FromMinutes(30).TotalMilliseconds);
            _timer.Elapsed += async (sender, args) => await ProcessExpiredProducts();
            _timer.AutoReset = true;
            _timer.Enabled = true;

            return Task.CompletedTask;
        }

        private async Task ProcessExpiredProducts()
        {
            _logger.LogInformation("Checking ...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                try
                {
                    var expiredProducts = await unitOfWork.PackagingProductRepository.GetExpiredProducts();
                    var outstockProducts = await unitOfWork.PackagingProductRepository.GetOutStockProducts();

                    try
                    {
                        if (expiredProducts.Any())
                        {
                            foreach (var product in expiredProducts)
                            {
                                product.Status = "Expired";
                                unitOfWork.PackagingProductRepository.PrepareUpdate(product);
                            }

                            await unitOfWork.PackagingProductRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Product update expired error: {ex.Message}");
                    }

                    try
                    {
                        if (outstockProducts.Any())
                        {
                            foreach (var product in outstockProducts)
                            {
                                product.Status = "OutOfStock";
                                unitOfWork.PackagingProductRepository.PrepareUpdate(product);
                            }

                            await unitOfWork.PackagingProductRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Product update out stock error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Error in updating product!");
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
