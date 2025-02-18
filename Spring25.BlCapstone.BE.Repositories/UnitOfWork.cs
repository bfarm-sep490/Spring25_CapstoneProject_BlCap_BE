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
        private AccountRepository _accountRepository;
        private CaringFertilizerRepository _caringFertilizerRepository;
        private CaringImageRepository _caringImageRepository;
        private CaringItemRepository _caringItemRepository;
        private CaringPesticideRepository _caringPesticideRepository;
        private CaringTaskRepository _caringTaskRepository;
        private DeviceRepository _deviceRepository;
        private ExpertRepository _expertRepository;
        private FarmerPermissionRepository _farmerPermissionRepository;
        private FarmerRepository _farmerRepository;
        private FertilizerRangeRepository _fertilizerRangeRepository;
        private FertilizerRepository _fertilizerRepository;
        private HarvestingImageRepository _harvestingImageRepository;
        private HarvestingItemRepository _harvestingItemRepository;
        private HarvestingTaskRepository _harvestingTaskRepository;
        private InspectingFormRepository _inspectingFormRepository;
        private InspectingImageRepository _inspectingImageRepository;
        private InspectingItemRepository _inspectingItemRepository;
        private InspectorRepository _inspectorRepository;
        private IssueRepository _issueRepository;
        private ItemRepository _itemRepository;
        private OrderPlanRepository _orderPlanRepository;
        private OrderPlantRepository _orderPlantRepository;
        private OrderRepository _orderRepository;
        private PesticideRangeRepository _pesticideRangeRepository;
        private PesticideRepository _pesticideRepository;
        private PlanRepository _planRepository;
        private PlantRepository _plantRepository;
        private ProblemImageRepository _problemImageRepository;
        private ProblemRepository _problemRepository;
        private RetailerRepository _retailerRepository;
        private SampleSolutionRepository _sampleSolutionRepository;
        private TransactionRepository _transactionRepository;
        private YieldPlanRepository _yieldPlanRepository;
        private YieldRepository _yieldRepository;

        public UnitOfWork()
        {
            _accountRepository ??= new AccountRepository();
            _farmerRepository ??= new FarmerRepository();
            _expertRepository ??= new ExpertRepository();
            _itemRepository ??= new ItemRepository();
            _yieldRepository ??= new YieldRepository();
            _plantRepository ??= new PlantRepository();
            _retailerRepository ??= new RetailerRepository();
            _orderRepository ??= new OrderRepository();
            _transactionRepository ??= new TransactionRepository();
            _planRepository ??= new PlanRepository();
            _deviceRepository ??= new DeviceRepository();
            _farmerPermissionRepository ??= new FarmerPermissionRepository();
            _yieldPlanRepository ??= new YieldPlanRepository();
            _caringTaskRepository ??= new CaringTaskRepository();
            _harvestingTaskRepository ??= new HarvestingTaskRepository();
            _inspectingFormRepository ??= new InspectingFormRepository();
            _caringImageRepository ??= new CaringImageRepository();
            _harvestingImageRepository ??= new HarvestingImageRepository();
            _inspectingImageRepository ??= new InspectingImageRepository();
            _pesticideRepository ??= new PesticideRepository();
            _caringPesticideRepository ??= new CaringPesticideRepository();
            _fertilizerRepository ??= new FertilizerRepository();
            _caringFertilizerRepository ??= new CaringFertilizerRepository();
            _caringItemRepository ??= new CaringItemRepository();
            _harvestingItemRepository ??= new HarvestingItemRepository();
            _inspectingItemRepository ??= new InspectingItemRepository();
            _fertilizerRangeRepository ??= new FertilizerRangeRepository();
            _inspectorRepository ??= new InspectorRepository();
            _issueRepository ??= new IssueRepository();
            _orderPlanRepository ??= new OrderPlanRepository();
            _orderPlantRepository ??= new OrderPlantRepository();
            _pesticideRangeRepository ??= new PesticideRangeRepository();
            _problemImageRepository ??= new ProblemImageRepository();
            _problemRepository ??= new ProblemRepository();
            _sampleSolutionRepository ??= new SampleSolutionRepository();
        }

        public UnitOfWork(AccountRepository accountRepository, FarmerRepository farmerRepository,
            ExpertRepository expertRepository, ItemRepository itemRepository,YieldRepository yieldRepository,
            PlantRepository plantRepository, RetailerRepository retailerRepository,
            OrderRepository orderRepository, TransactionRepository transactionRepository, PlanRepository planRepository,
            DeviceRepository deviceRepository, FarmerPermissionRepository farmerPermissionRepository,
            YieldPlanRepository yieldPlanRepository, CaringTaskRepository caringTaskRepository,
            HarvestingTaskRepository harvestingTaskRepository,
            InspectingFormRepository inspectingFormRepository, CaringImageRepository caringImageRepository,
            HarvestingImageRepository harvestingImageRepository, InspectingImageRepository inspectingImageRepository,
            PesticideRepository pesticideRepository, CaringPesticideRepository caringPesticideRepository,
            FertilizerRepository fertilizerRepository, CaringFertilizerRepository caringFertilizerRepository,
            CaringItemRepository caringItemRepository, HarvestingItemRepository harvestingItemRepository,
            InspectingItemRepository inspectingItemRepository, FertilizerRangeRepository fertilizerRangeRepository,
            InspectorRepository inspectorRepository, IssueRepository issueRepository, OrderPlanRepository orderPlanRepository,
            OrderPlantRepository orderPlantRepository, PesticideRangeRepository pesticideRangeRepository, ProblemImageRepository problemImageRepository,
            ProblemRepository problemRepository, SampleSolutionRepository sampleSolutionRepository)
        {
            _accountRepository = accountRepository;
            _farmerRepository = farmerRepository;
            _expertRepository = expertRepository;
            _itemRepository = itemRepository;
            _yieldRepository = yieldRepository;
            _plantRepository = plantRepository;
            _retailerRepository = retailerRepository;
            _orderRepository = orderRepository;
            _transactionRepository = transactionRepository;
            _planRepository = planRepository;
            _deviceRepository = deviceRepository;
            _farmerPermissionRepository = farmerPermissionRepository;
            _yieldPlanRepository = yieldPlanRepository;
            _caringTaskRepository = caringTaskRepository;
            _harvestingTaskRepository = harvestingTaskRepository;
            _inspectingFormRepository = inspectingFormRepository;
            _caringImageRepository = caringImageRepository;
            _harvestingImageRepository = harvestingImageRepository;
            _inspectingImageRepository = inspectingImageRepository;
            _pesticideRepository = pesticideRepository;
            _caringPesticideRepository = caringPesticideRepository;
            _fertilizerRepository = fertilizerRepository;
            _caringFertilizerRepository = caringFertilizerRepository;
            _caringItemRepository = caringItemRepository;
            _harvestingItemRepository = harvestingItemRepository;
            _inspectingItemRepository = inspectingItemRepository;
            _fertilizerRangeRepository = fertilizerRangeRepository;
            _inspectorRepository = inspectorRepository;
            _issueRepository = issueRepository;
            _orderPlanRepository = orderPlanRepository;
            _orderPlantRepository = orderPlantRepository;
            _pesticideRangeRepository = pesticideRangeRepository;
            _problemImageRepository = problemImageRepository;
            _problemRepository = problemRepository;
            _sampleSolutionRepository = sampleSolutionRepository;
        }

        public AccountRepository AccountRepository { get { return _accountRepository; } }
        public FarmerRepository FarmerRepository { get { return _farmerRepository; } }
        public ExpertRepository ExpertRepository { get { return _expertRepository; } }
        public ItemRepository ItemRepository { get { return _itemRepository; } }
        public YieldRepository YieldRepository { get {return _yieldRepository; } }
        public PlantRepository PlantRepository { get { return _plantRepository; } }
        public RetailerRepository RetailerRepository { get { return _retailerRepository; } }
        public OrderRepository OrderRepository { get { return _orderRepository; } }
        public TransactionRepository TransactionRepository { get { return _transactionRepository; } }
        public PlanRepository PlanRepository { get { return _planRepository; } }
        public DeviceRepository DeviceRepository { get { return _deviceRepository; } }
        public FarmerPermissionRepository FarmerPermissionRepository { get { return _farmerPermissionRepository; } }
        public YieldPlanRepository YieldPlanRepository { get { return _yieldPlanRepository; } }
        public CaringTaskRepository ProductionTaskRepository { get { return _caringTaskRepository; } }
        public HarvestingTaskRepository HarvestingTaskRepository { get {return _harvestingTaskRepository; } }
        public InspectingFormRepository InspectingTaskRepository { get { return _inspectingFormRepository; } }
        public CaringImageRepository ProductionImageRepository { get { return _caringImageRepository; } }
        public HarvestingImageRepository HarvestingImageRepository { get { return _harvestingImageRepository; } }
        public InspectingImageRepository InspectingImageRepository { get { return _inspectingImageRepository; } }
        public PesticideRepository PesticideRepository { get { return _pesticideRepository; } }
        public CaringPesticideRepository ProductionPesticideRepository { get { return _caringPesticideRepository; } }
        public FertilizerRepository FertilizerRepository {  get { return _fertilizerRepository; } }
        public CaringFertilizerRepository ProductionFertilizerRepository { get { return _caringFertilizerRepository; } }
        public CaringItemRepository ProductionItemRepository { get { return _caringItemRepository; } }
        public HarvestingItemRepository HarvestingItemRepository { get { return _harvestingItemRepository; } }
        public InspectingItemRepository InspectingItemRepository { get { return _inspectingItemRepository; } }
        public FertilizerRangeRepository FertilizerRangeRepository { get { return _fertilizerRangeRepository; } }
        public InspectorRepository InspectorRepository { get { return _inspectorRepository; } }
        public IssueRepository IssueRepository { get { return _issueRepository; } }
        public OrderPlanRepository OrderPlanRepository { get { return _orderPlanRepository; } }
        public OrderPlantRepository OrderPlantRepository { get { return _orderPlantRepository; } }
        public PesticideRangeRepository PesticideRangeRepository { get { return _pesticideRangeRepository; } }
        public ProblemImageRepository ProblemImageRepository { get { return _problemImageRepository; } }
        public ProblemRepository ProblemRepository { get { return _problemRepository; } }
        public SampleSolutionRepository SampleSolutionRepository { get { return _sampleSolutionRepository; } }
    }
}