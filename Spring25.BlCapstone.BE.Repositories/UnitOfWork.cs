using Spring25.BlCapstone.BE.Repositories.Models;
using Spring25.BlCapstone.BE.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring25.BlCapstone.BE.Repositories
{
    public class UnitOfWork
    {
        private FarmOwnerRepository _farmOwnerRepository;
        private FieldRepository _fieldRepository;
        private ImageFieldRepository _imageFieldRepository;
        private PesticideRepository _pesticideRepository;
        private FertilizerRepository _ferilizerRepository;
        private FarmerRepository _farmerRepository;
        private ImageReportRepository _imageReportRepository;
        private PeriodRepository _periodRepository;
        private PlanRepository _planRepository;
        private SeedRepository _seedRepository;
        private TaskFertilizerRepository _taskFertilizerRepository;
        private TaskPesticideRepository _taskPesticideRepository;
        private TaskRepository _taskRepository;

        public UnitOfWork()
        {
            _farmOwnerRepository ??= new FarmOwnerRepository();
            _pesticideRepository ??=new PesticideRepository();
            _ferilizerRepository ??= new FertilizerRepository();
            _fieldRepository ??= new FieldRepository();
            _imageFieldRepository ??= new ImageFieldRepository();
            _farmerRepository ??= new FarmerRepository();
            _imageReportRepository ??= new ImageReportRepository();
            _periodRepository ??= new PeriodRepository();
            _planRepository ??= new PlanRepository();
            _seedRepository ??= new SeedRepository();
            _taskFertilizerRepository ??= new TaskFertilizerRepository();
            _taskPesticideRepository ??= new TaskPesticideRepository();
            _taskRepository ??= new TaskRepository();
        }

        public UnitOfWork(
          FarmOwnerRepository farmOwnerRepository,
          PesticideRepository pesticideRepository,
          FertilizerRepository fertilizerRepository,
          FieldRepository fieldRepository,
          ImageFieldRepository imageFieldRepository,
          FarmerRepository farmerRepository,
          ImageReportRepository imageReportRepository,
          PeriodRepository periodRepository,
          PlanRepository planRepository,
          SeedRepository seedRepository,
          TaskFertilizerRepository taskFertilizerRepository,
          TaskPesticideRepository taskPesticideRepository,
          TaskRepository taskRepository)
        {
            _farmOwnerRepository = farmOwnerRepository;
            _pesticideRepository = pesticideRepository;
            _ferilizerRepository = fertilizerRepository;
            _fieldRepository = fieldRepository;
            _imageFieldRepository = imageFieldRepository;
            _farmerRepository = farmerRepository;
            _imageReportRepository = imageReportRepository;
            _periodRepository = periodRepository;
            _planRepository = planRepository;
            _seedRepository = seedRepository;
            _taskFertilizerRepository = taskFertilizerRepository;
            _taskRepository = taskRepository;
            _taskPesticideRepository = taskPesticideRepository;
        }

        public PesticideRepository PesticideRepository { get { return _pesticideRepository; } }
        public FarmOwnerRepository FarmOwnerRepository { get { return _farmOwnerRepository; } }
        public FieldRepository FieldRepository { get { return _fieldRepository; } }
        public ImageFieldRepository ImageFieldRepository { get { return _imageFieldRepository; } }
        public FertilizerRepository FertilizerRepository { get { return _ferilizerRepository; } }
        public FarmerRepository FarmerRepository { get { return _farmerRepository; } }
        public ImageReportRepository ImageReportRepository { get { return _imageReportRepository; } }
        public PeriodRepository PeriodRepository { get { return _periodRepository; } }
        public PlanRepository PlanRepository { get { return _planRepository; } }
        public SeedRepository SeedRepository {  get { return _seedRepository; } }
        public TaskFertilizerRepository TaskFertilizerRepository { get { return _taskFertilizerRepository; } }
        public TaskPesticideRepository TaskPesticideRepository { get { return _taskPesticideRepository; } }
        public TaskRepository TaskRepository { get { return _taskRepository; } }
    }
}
