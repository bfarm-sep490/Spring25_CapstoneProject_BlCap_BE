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
        private readonly Context _context;

        private AccountRepository _accountRepository;
        private CaringFertilizerRepository _caringFertilizerRepository;
        private CaringImageRepository _caringImageRepository;
        private CaringItemRepository _caringItemRepository;
        private CaringPesticideRepository _caringPesticideRepository;
        private CaringTaskRepository _caringTaskRepository;
        private ExpertRepository _expertRepository;
        private FarmerPermissionRepository _farmerPermissionRepository;
        private FarmerRepository _farmerRepository;
        private FertilizerRepository _fertilizerRepository;
        private HarvestingImageRepository _harvestingImageRepository;
        private HarvestingItemRepository _harvestingItemRepository;
        private HarvestingTaskRepository _harvestingTaskRepository;
        private InspectingFormRepository _inspectingFormRepository;
        private InspectingImageRepository _inspectingImageRepository;
        private PackagingTaskRepository _packagingTaskRepository;
        private PackagingItemRepository _packagingItemRepository;
        private PackagingImageRepository _packagingImageRepository;
        private InspectorRepository _inspectorRepository;
        private ItemRepository _itemRepository;
        private OrderRepository _orderRepository;
        private PesticideRepository _pesticideRepository;
        private PlanRepository _planRepository;
        private PlantRepository _plantRepository;
        private ProblemImageRepository _problemImageRepository;
        private ProblemRepository _problemRepository;
        private RetailerRepository _retailerRepository;
        private TransactionRepository _transactionRepository;
        private YieldRepository _yieldRepository;
        private FarmerCaringTaskRepository _farmerCaringTaskRepository;
        private FarmerHarvestingTaskRepository _farmerHarvestingTaskRepository;
        private FarmerPackagingTaskRepository _farmerPackagingTaskRepository;
        private InspectingResultRepository _inspectingResultRepository;
        private NotificationExpertRepository _notificationExpertRepository;
        private NotificationFarmerRepository _notificationFarmerRepository;
        private NotificationOwnerRepository _notificationOwnerRepository;
        private NotificationRetailerRepository _notificationRetailerRepository;
        private NotificationInspectorRepository _notificationInspectorRepository;
        private PackagingProductRepository _packagingProductRepository;
        private PackagingTypeRepository _packagingTypeRepository;
        private PlantYieldRepository _plantYieldRepository;
        private PlanTransactionRepository _planTransactionRepository;
        private SpecializationRepository _specializationRepository;
        private FarmerSpecializationRepository _farmerSpecializationRepository;
        private FarmerPerformanceRepository _farmerPerformanceRepository;
        private SeasonalPlantRepository _seasonalPlantRepository;
        private OrderPlanRepository _orderPlanRepository;
        private ProductPickupBatchRepository _productPickupBatchRepository;
        private ConfigurationSystemRepository _configurationSystemRepository;

        public UnitOfWork()
        {
            _context = new Context();
            _accountRepository ??= new AccountRepository(_context);
            _farmerRepository ??= new FarmerRepository(_context);
            _expertRepository ??= new ExpertRepository(_context);
            _itemRepository ??= new ItemRepository(_context);
            _yieldRepository ??= new YieldRepository(_context);
            _plantRepository ??= new PlantRepository(_context);
            _retailerRepository ??= new RetailerRepository(_context);
            _orderRepository ??= new OrderRepository(_context);
            _transactionRepository ??= new TransactionRepository(_context);
            _planRepository ??= new PlanRepository(_context);
            _farmerPermissionRepository ??= new FarmerPermissionRepository(_context);
            _caringTaskRepository ??= new CaringTaskRepository(_context);
            _harvestingTaskRepository ??= new HarvestingTaskRepository(_context);
            _inspectingFormRepository ??= new InspectingFormRepository(_context);
            _caringImageRepository ??= new CaringImageRepository(_context);
            _harvestingImageRepository ??= new HarvestingImageRepository(_context);
            _inspectingImageRepository ??= new InspectingImageRepository(_context);
            _pesticideRepository ??= new PesticideRepository(_context);
            _caringPesticideRepository ??= new CaringPesticideRepository(_context);
            _fertilizerRepository ??= new FertilizerRepository(_context);
            _caringFertilizerRepository ??= new CaringFertilizerRepository(_context);
            _caringItemRepository ??= new CaringItemRepository(_context);
            _harvestingItemRepository ??= new HarvestingItemRepository(_context);
            _packagingTaskRepository ??= new PackagingTaskRepository(_context);
            _packagingItemRepository ??= new PackagingItemRepository(_context);
            _packagingImageRepository ??= new PackagingImageRepository(_context);
            _inspectorRepository ??= new InspectorRepository(_context);
            _problemImageRepository ??= new ProblemImageRepository(_context);
            _problemRepository ??= new ProblemRepository(_context);
            _farmerCaringTaskRepository ??= new FarmerCaringTaskRepository(_context);
            _farmerHarvestingTaskRepository ??= new FarmerHarvestingTaskRepository(_context);
            _farmerPackagingTaskRepository ??= new FarmerPackagingTaskRepository(_context);
            _inspectingResultRepository ??= new InspectingResultRepository(_context);
            _notificationExpertRepository ??= new NotificationExpertRepository(_context);
            _notificationFarmerRepository ??= new NotificationFarmerRepository(_context);
            _notificationOwnerRepository ??= new NotificationOwnerRepository(_context);
            _notificationInspectorRepository ??= new NotificationInspectorRepository(_context);
            _notificationRetailerRepository ??= new NotificationRetailerRepository(_context);
            _packagingProductRepository ??= new PackagingProductRepository(_context);
            _packagingTypeRepository ??= new PackagingTypeRepository(_context);
            _plantYieldRepository ??= new PlantYieldRepository(_context);
            _planTransactionRepository ??= new PlanTransactionRepository(_context);
            _specializationRepository ??= new SpecializationRepository(_context);
            _farmerSpecializationRepository ??= new FarmerSpecializationRepository(_context);
            _farmerPerformanceRepository ??= new FarmerPerformanceRepository(_context);
            _seasonalPlantRepository ??= new SeasonalPlantRepository(_context);
            _orderPlanRepository ??= new OrderPlanRepository(_context);
            _productPickupBatchRepository ??= new ProductPickupBatchRepository(_context);
            _configurationSystemRepository ??= new ConfigurationSystemRepository(_context);
        }

        public UnitOfWork(AccountRepository accountRepository, FarmerRepository farmerRepository,
            ExpertRepository expertRepository, ItemRepository itemRepository,YieldRepository yieldRepository,
            PlantRepository plantRepository, RetailerRepository retailerRepository,
            OrderRepository orderRepository, TransactionRepository transactionRepository, PlanRepository planRepository,
            FarmerPermissionRepository farmerPermissionRepository,
            CaringTaskRepository caringTaskRepository, NotificationInspectorRepository notificationInspectorRepository,
            HarvestingTaskRepository harvestingTaskRepository, FarmerCaringTaskRepository farmerCaringTaskRepository,
            InspectingFormRepository inspectingFormRepository, CaringImageRepository caringImageRepository,
            HarvestingImageRepository harvestingImageRepository, InspectingImageRepository inspectingImageRepository,
            PesticideRepository pesticideRepository, CaringPesticideRepository caringPesticideRepository,
            FertilizerRepository fertilizerRepository, CaringFertilizerRepository caringFertilizerRepository,
            CaringItemRepository caringItemRepository, HarvestingItemRepository harvestingItemRepository,
            PackagingTaskRepository packagingTaskRepository, FarmerHarvestingTaskRepository farmerHarvestingTaskRepository,
            PackagingItemRepository packagingItemRepository, PackagingImageRepository packagingImageRepository, FarmerPackagingTaskRepository farmerPackagingTaskRepository,
            InspectorRepository inspectorRepository, InspectingResultRepository inspectingResultRepository, NotificationExpertRepository notificationExpertRepository,
            ProblemImageRepository problemImageRepository, NotificationFarmerRepository notificationFarmerRepository, NotificationOwnerRepository notificationOwnerRepository, NotificationRetailerRepository notificationRetailerRepository,
            ProblemRepository problemRepository, PackagingProductRepository packagingProductRepository, PackagingTypeRepository packagingTypeRepository,
            PlantYieldRepository plantYieldRepository, PlanTransactionRepository planTransactionRepository,
            SpecializationRepository specializationRepository, FarmerSpecializationRepository farmerSpecializationRepository, FarmerPerformanceRepository farmerPerformanceRepository,
            SeasonalPlantRepository seasonalPlantRepository, OrderPlanRepository orderPlanRepository, 
            ProductPickupBatchRepository productPickupBatchRepository, ConfigurationSystemRepository configurationSystemRepository)
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
            _farmerPermissionRepository = farmerPermissionRepository;
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
            _packagingTaskRepository = packagingTaskRepository;
            _packagingItemRepository = packagingItemRepository;
            _packagingImageRepository = packagingImageRepository;
            _inspectorRepository = inspectorRepository;
            _problemImageRepository = problemImageRepository;
            _problemRepository = problemRepository;
            _farmerCaringTaskRepository = farmerCaringTaskRepository;
            _farmerHarvestingTaskRepository = farmerHarvestingTaskRepository;
            _farmerPackagingTaskRepository = farmerPackagingTaskRepository;
            _inspectingResultRepository = inspectingResultRepository;
            _notificationExpertRepository = notificationExpertRepository;
            _notificationFarmerRepository = notificationFarmerRepository;
            _notificationOwnerRepository = notificationOwnerRepository;
            _notificationRetailerRepository = notificationRetailerRepository;
            _packagingProductRepository = packagingProductRepository;
            _packagingTypeRepository = packagingTypeRepository;
            _plantYieldRepository = plantYieldRepository;
            _planTransactionRepository = planTransactionRepository;
            _specializationRepository = specializationRepository;
            _farmerSpecializationRepository = farmerSpecializationRepository;
            _farmerPerformanceRepository = farmerPerformanceRepository;
            _seasonalPlantRepository = seasonalPlantRepository;
            _orderPlanRepository = orderPlanRepository;
            _productPickupBatchRepository = productPickupBatchRepository;
            _configurationSystemRepository = configurationSystemRepository;
            _notificationInspectorRepository = notificationInspectorRepository;
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
        public FarmerPermissionRepository FarmerPermissionRepository { get { return _farmerPermissionRepository; } }
        public CaringTaskRepository CaringTaskRepository { get { return _caringTaskRepository; } }
        public HarvestingTaskRepository HarvestingTaskRepository { get {return _harvestingTaskRepository; } }
        public InspectingFormRepository InspectingFormRepository { get { return _inspectingFormRepository; } }
        public CaringImageRepository CaringImageRepository { get { return _caringImageRepository; } }
        public HarvestingImageRepository HarvestingImageRepository { get { return _harvestingImageRepository; } }
        public InspectingImageRepository InspectingImageRepository { get { return _inspectingImageRepository; } }
        public PesticideRepository PesticideRepository { get { return _pesticideRepository; } }
        public CaringPesticideRepository CaringPesticideRepository { get { return _caringPesticideRepository; } }
        public FertilizerRepository FertilizerRepository {  get { return _fertilizerRepository; } }
        public CaringFertilizerRepository CaringFertilizerRepository { get { return _caringFertilizerRepository; } }
        public CaringItemRepository CaringItemRepository { get { return _caringItemRepository; } }
        public HarvestingItemRepository HarvestingItemRepository { get { return _harvestingItemRepository; } }
        public InspectorRepository InspectorRepository { get { return _inspectorRepository; } }
        public ProblemImageRepository ProblemImageRepository { get { return _problemImageRepository; } }
        public ProblemRepository ProblemRepository { get { return _problemRepository; } }
        public PackagingTaskRepository PackagingTaskRepository { get { return _packagingTaskRepository; } }
        public PackagingItemRepository PackagingItemRepository { get { return _packagingItemRepository; } }
        public PackagingImageRepository PackagingImageRepository { get { return _packagingImageRepository; } }
        public FarmerCaringTaskRepository FarmerCaringTaskRepository { get { return _farmerCaringTaskRepository; } }
        public FarmerHarvestingTaskRepository FarmerHarvestingTaskRepository { get { return _farmerHarvestingTaskRepository; } }
        public FarmerPackagingTaskRepository FarmerPackagingTaskRepository { get { return _farmerPackagingTaskRepository; } }
        public InspectingResultRepository InspectingResultRepository { get { return _inspectingResultRepository; } }
        public NotificationExpertRepository NotificationExpertRepository { get { return _notificationExpertRepository; } }
        public NotificationFarmerRepository NotificationFarmerRepository { get { return _notificationFarmerRepository; } }
        public NotificationOwnerRepository NotificationOwnerRepository { get { return _notificationOwnerRepository; } }
        public NotificationRetailerRepository NotificationRetailerRepository { get { return _notificationRetailerRepository; } }
        public PackagingProductRepository PackagingProductRepository { get { return _packagingProductRepository; } }
        public PackagingTypeRepository PackagingTypeRepository { get { return _packagingTypeRepository; } }
        public PlantYieldRepository PlantYieldRepository { get { return _plantYieldRepository; } }
        public PlanTransactionRepository PlanTransactionRepository { get { return _planTransactionRepository; } }
        public SpecializationRepository SpecializationRepository { get { return _specializationRepository; } }
        public FarmerSpecializationRepository FarmerSpecializationRepository { get { return _farmerSpecializationRepository; } }
        public FarmerPerformanceRepository FarmerPerformanceRepository { get { return _farmerPerformanceRepository; } }
        public SeasonalPlantRepository SeasonalPlantRepository { get { return _seasonalPlantRepository; } }
        public OrderPlanRepository OrderPlanRepository { get { return _orderPlanRepository; } }
        public ProductPickupBatchRepository ProductPickupBatchRepository { get { return _productPickupBatchRepository; } }
        public ConfigurationSystemRepository ConfigurationSystemRepository { get { return _configurationSystemRepository; } }
        public NotificationInspectorRepository NotificationInspectorRepository {  get { return _notificationInspectorRepository; } }

        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
        public async Task CommitAsync() => await _context.Database.CommitTransactionAsync();
        public async Task RollbackAsync() => await _context.Database.RollbackTransactionAsync();
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}