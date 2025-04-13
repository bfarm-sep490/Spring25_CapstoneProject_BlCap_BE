using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IConfigurationSystemService
    {
        Task<IBusinessResult> GetAll(string? status);
        Task<IBusinessResult> CreateNewConfig(ConfigSystemCreate model);
        Task<IBusinessResult> SwitchStatus(int id);
    }

    public class ConfigurationSystemService : IConfigurationSystemService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConfigurationSystemService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll(string? status)
        {
            try
            {
                var configs = await _unitOfWork.ConfigurationSystemRepository.GetAllConfigs(status);

                if (!configs.Any())
                {
                    return new BusinessResult(404, "Not found any config !");
                }

                return new BusinessResult(200, "List of configuration: ", configs);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
        
        public async Task<IBusinessResult> CreateNewConfig(ConfigSystemCreate model)
        {
            try
            {
                var cc = _mapper.Map<ConfigurationSystem>(model);
                cc.Status = "Inactive";

                await _unitOfWork.ConfigurationSystemRepository.CreateAsync(cc);

                return new BusinessResult(200, "New configuration: ", cc);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> SwitchStatus(int id)
        {
            try
            {
                var config = await _unitOfWork.ConfigurationSystemRepository.GetByIdAsync(id);
                if (config == null)
                {
                    return new BusinessResult(400, "Not found any config !");
                }

                config.Status = !config.Status.ToLower().Trim().Equals("active") ? "Active" : "Inactive";
                await _unitOfWork.ConfigurationSystemRepository.UpdateAsync(config);
                return new BusinessResult(200, "Switch success", config);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
