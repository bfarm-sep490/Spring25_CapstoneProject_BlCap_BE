﻿using Microsoft.Extensions.DependencyInjection;
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
    public class TaskCheckStatusService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TaskCheckStatusService> _logger;

        public TaskCheckStatusService(IServiceScopeFactory serviceScopeFactory,
            ILogger<TaskCheckStatusService> logger
            )
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Check expired Task status started !");
            
            _timer = new System.Timers.Timer(TimeSpan.FromMinutes(30).TotalMilliseconds);
            _timer.Elapsed += async (sender, args) => await ProcessExpiredCaringTasks();
            _timer.AutoReset = true;
            _timer.Enabled = true;

            return Task.CompletedTask;
        }

        private async Task ProcessExpiredCaringTasks()
        {
            _logger.LogInformation("Checking ...");
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
                try
                {
                    var expiredCaringTasks = await unitOfWork.CaringTaskRepository.GetExpiredCaringTasks();
                    var expiredHarvestingTasks = await unitOfWork.HarvestingTaskRepository.GetExpiredHarvestingTasks();
                    var expiredPackagingTasks = await unitOfWork.PackagingTaskRepository.GetExpiredPackagingTasks();
                    var expiredInspectingForms = await unitOfWork.InspectingFormRepository.GetExpiredInspectingForms();

                    try
                    {
                        if (expiredCaringTasks.Any())
                        {
                            foreach (var task in expiredCaringTasks)
                            {
                                task.Status = "Incomplete";
                                unitOfWork.CaringTaskRepository.PrepareUpdate(task);

                                var farmer = await unitOfWork.FarmerPerformanceRepository.GetFarmerByTaskId(caringTaskId: task.Id);
                                farmer.IncompleteTasks += 1;
                                farmer.PerformanceScore = Math.Round((((farmer.CompletedTasks * 1.0) / ((farmer.CompletedTasks * 1.0) + (farmer.IncompleteTasks * 1.0))) * 100), 2);

                                unitOfWork.FarmerPerformanceRepository.PrepareUpdate(farmer);
                            }

                            await unitOfWork.CaringTaskRepository.SaveAsync();
                            await unitOfWork.FarmerPerformanceRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Caring Task update error: {ex.Message}");
                    }
                    
                    try
                    {
                        if (expiredHarvestingTasks.Any())
                        {
                            foreach (var task in expiredHarvestingTasks)
                            {
                                task.Status = "Incomplete";
                                unitOfWork.HarvestingTaskRepository.PrepareUpdate(task);

                                var farmer = await unitOfWork.FarmerPerformanceRepository.GetFarmerByTaskId(harvestingTaskId: task.Id);
                                farmer.IncompleteTasks += 1;
                                farmer.PerformanceScore = Math.Round((((farmer.CompletedTasks * 1.0) / ((farmer.CompletedTasks * 1.0) + (farmer.IncompleteTasks * 1.0))) * 100), 2);

                                unitOfWork.FarmerPerformanceRepository.PrepareUpdate(farmer);
                            }

                            await unitOfWork.HarvestingTaskRepository.SaveAsync();
                            await unitOfWork.FarmerPerformanceRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Harvesting Task update error: {ex.Message}");
                    }
                    
                    try
                    {
                        if (expiredPackagingTasks.Any())
                        {
                            foreach (var task in expiredPackagingTasks)
                            {
                                task.Status = "Incomplete";
                                unitOfWork.PackagingTaskRepository.PrepareUpdate(task);

                                var farmer = await unitOfWork.FarmerPerformanceRepository.GetFarmerByTaskId(packagingTaskId: task.Id);
                                farmer.IncompleteTasks += 1;
                                farmer.PerformanceScore = Math.Round((((farmer.CompletedTasks * 1.0) / ((farmer.CompletedTasks * 1.0) + (farmer.IncompleteTasks * 1.0))) * 100), 2);

                                unitOfWork.FarmerPerformanceRepository.PrepareUpdate(farmer);
                            }

                            await unitOfWork.PackagingTaskRepository.SaveAsync();
                            await unitOfWork.FarmerPerformanceRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Packaging Task update error: {ex.Message}");
                    }
                    
                    try
                    {
                        if (expiredInspectingForms.Any())
                        {
                            foreach (var task in expiredInspectingForms)
                            {
                                task.Status = "Incomplete";
                                unitOfWork.InspectingFormRepository.PrepareUpdate(task);
                            }

                            await unitOfWork.InspectingFormRepository.SaveAsync();
                            _logger.LogInformation("complete !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Inspecting Form update error: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, "Error in Task update!");
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