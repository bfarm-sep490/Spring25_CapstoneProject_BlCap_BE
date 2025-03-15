using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Tasks.Inspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IInspectingResultService
    {
        Task<IBusinessResult> GetAllResults(string? evaluatedResult);
        Task<IBusinessResult> GetResultById(int id);
    }

    public class InspectingResultService : IInspectingResultService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InspectingResultService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAllResults(string? evaluatedResult)
        {
            try
            {
                var results = await _unitOfWork.InspectingResultRepository.GetInspectingResults(evaluatedResult);
                var rs = _mapper.Map<List<InspectingResultModel>>(results);

                if (results.Count > 0)
                {
                    return new BusinessResult(200, "List of inspecting results: ", rs);
                }
                else
                {
                    return new BusinessResult(404, "Not found any inspecting results");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }

        public async Task<IBusinessResult> GetResultById(int id)
        {
            try
            {
                var results = await _unitOfWork.InspectingResultRepository.GetInspectingResults(resultId: id);
                var rs = _mapper.Map<List<InspectingResultModel>>(results);

                if (results.Count > 0)
                {
                    return new BusinessResult(200, "List of inspecting results: ", rs);
                }
                else
                {
                    return new BusinessResult(404, "Not found any inspecting results");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(500, ex.Message);
            }
        }
    }
}
