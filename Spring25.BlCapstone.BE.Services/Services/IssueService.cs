using AutoMapper;
using Spring25.BlCapstone.BE.Repositories;
using Spring25.BlCapstone.BE.Services.Base;
using Spring25.BlCapstone.BE.Services.BusinessModels.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Services.Services
{
    public interface IIssueService
    {
        Task<IBusinessResult> GetAll();
    } 

    public class IssueService : IIssueService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IssueService(IMapper mapper)
        {
            _unitOfWork ??= new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var issues = await _unitOfWork.IssueRepository.GetAllAsync();

                var res = _mapper.Map<List<IssueModel>>(issues);
                if (issues.Any())
                {
                    return new BusinessResult { Status = 200, Message = "List of Issues! ", Data = res };
                }

                return new BusinessResult { Status = 404, Message = "Not found any Issues ?", Data = null };
            }
            catch (Exception ex)
            {
                return new BusinessResult { Status = 500, Message = ex.Message, Data = null };
            }
        }
    }
}
