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
        Task<IBusinessResult> GetConfig();
        Task<IBusinessResult> CreateNewConfig(ConfigSystemCreate model);
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

        public async Task<IBusinessResult> GetConfig()
        {
            try
            {
                var configs = await _unitOfWork.ConfigurationSystemRepository.GetConfig();
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
                var config = await _unitOfWork.ConfigurationSystemRepository.GetConfig();
                if (config == null)
                {
                    var cc = _mapper.Map<ConfigurationSystem>(model);

                    await _unitOfWork.ConfigurationSystemRepository.CreateAsync(cc);
                }
                else
                {
                    _mapper.Map(model, config);

                    await _unitOfWork.ConfigurationSystemRepository.UpdateAsync(config);
                }
                
                return new BusinessResult(200, "New configuration: ", model);
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
