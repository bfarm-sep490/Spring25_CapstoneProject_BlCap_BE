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
        private DataEnvironmentRepository _dataEnvironmentRepository;   
        private FarmerCaringTaskRepository _farmerCaringTaskRepository;
        private FarmerHarvestingTaskRepository _farmerHarvestingTaskRepository;
        private FarmerPackagingTaskRepository _farmerPackagingTaskRepository;
        private InspectingResultRepository _inspectingResultRepository;
        private NotificationExpertRepository _notificationExpertRepository;
        private NotificationFarmerRepository _notificationFarmerRepository;
        private NotificationOwnerRepository _notificationOwnerRepository;
        private NotificationRetailerRepository _notificationRetailerRepository;
        private PackagingProductRepository _packagingProductRepository;
        private PackagingTypeRepository _packagingTypeRepository;
        private PlantYieldRepository _plantYieldRepository;
        private PlanTransactionRepository _planTransactionRepository;
        private OrderProductRepository _orderProductRepository;

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
            _packagingTaskRepository ??= new PackagingTaskRepository();
            _packagingItemRepository ??= new PackagingItemRepository();
            _packagingImageRepository ??= new PackagingImageRepository();
            _inspectorRepository ??= new InspectorRepository();
            _problemImageRepository ??= new ProblemImageRepository();
            _problemRepository ??= new ProblemRepository();
            _dataEnvironmentRepository ??= new DataEnvironmentRepository();
            _farmerCaringTaskRepository ??= new FarmerCaringTaskRepository();
            _farmerHarvestingTaskRepository ??= new FarmerHarvestingTaskRepository();
            _farmerPackagingTaskRepository ??= new FarmerPackagingTaskRepository();
            _inspectingResultRepository ??= new InspectingResultRepository();
            _notificationExpertRepository ??= new NotificationExpertRepository();
            _notificationFarmerRepository ??= new NotificationFarmerRepository();
            _notificationOwnerRepository ??= new NotificationOwnerRepository();
            _notificationRetailerRepository ??= new NotificationRetailerRepository();
            _packagingProductRepository ??= new PackagingProductRepository();
            _packagingTypeRepository ??= new PackagingTypeRepository();
            _plantYieldRepository ??= new PlantYieldRepository();
            _planTransactionRepository ??= new PlanTransactionRepository();
            _orderProductRepository ??= new OrderProductRepository();
        }

        public UnitOfWork(AccountRepository accountRepository, FarmerRepository farmerRepository,
            ExpertRepository expertRepository, ItemRepository itemRepository,YieldRepository yieldRepository,
            PlantRepository plantRepository, RetailerRepository retailerRepository,
            OrderRepository orderRepository, TransactionRepository transactionRepository, PlanRepository planRepository,
            DeviceRepository deviceRepository, FarmerPermissionRepository farmerPermissionRepository,
            CaringTaskRepository caringTaskRepository, DataEnvironmentRepository dataEnvironmentRepository,
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
            PlantYieldRepository plantYieldRepository, PlanTransactionRepository planTransactionRepository, OrderProductRepository orderProductRepository)
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
            _dataEnvironmentRepository = dataEnvironmentRepository;
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
            _orderProductRepository = orderProductRepository;
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
        public DataEnvironmentRepository DataEnvironmentRepository { get { return _dataEnvironmentRepository; } }
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
        public OrderProductRepository OrderProductRepository { get { return _orderProductRepository; } }
    }
}