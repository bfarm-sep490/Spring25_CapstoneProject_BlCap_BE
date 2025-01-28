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
        private FarmerRepository _farmerRepository;
        private ExpertRepository _expertRepository;
        private ItemRepository _itemRepository;
        private YieldRepository _yieldRepository;
        private SeedRepository _seedRepository;
        private RetailerRepository _retailerRepository;
        private DriverRepository _driverRepository;
        private OrderRepository _orderRepository;
        private ShipmentTripRepository _shipmentTripRepository;
        private ShipmentImageRepository _shipmentImageRepository;
        private TransactionRepository _transactionRepository;
        private PlanRepository _planRepository;
        private PackedProductRepository _packedProductRepository;
        private OrderDetailRepository _orderDetailRepository;
        private DeviceRepository _deviceRepository;
        private ExpertPermissionRepository _expertPermissionRepository;
        private FarmerPermissionRepository _farmerPermissionRepository;
        private YieldPlanRepository _yieldPlanRepository;
        private PeriodRepository _periodRepository;
        private ProductionTaskRepository _productionTaskRepository;
        private HarvestingTaskRepository _harvestingTaskRepository;
        private PackagingTaskRepository _packagingTaskRepository;
        private InspectingTaskRepository _inspectingTaskRepository;
        private ProductionImageRepository _productionImageRepository;
        private HarvestingImageRepository _harvestingImageRepository;
        private PackagingImageRepository _packagingImageRepository;
        private InspectingImageRepository _inspectingImageRepository;
        private PesticideRepository _pesticideRepository;
        private ProductionPesticideRepository _productionPesticideRepository;
        private FertilizerRepository _fertilizerRepository;
        private ProductionFertilizerRepository _productionFertilizerRepository;
        private ProductionItemRepository _productionItemRepository;
        private HarvestingItemRepository _harvestingItemRepository;
        private PackagingItemRepository _packagingItemRepository;
        private InspectingItemRepository _inspectingItemRepository;

        public UnitOfWork()
        {
            _accountRepository ??= new AccountRepository();
            _farmerRepository ??= new FarmerRepository();
            _expertRepository ??= new ExpertRepository();
            _itemRepository ??= new ItemRepository();
            _yieldRepository ??= new YieldRepository();
            _seedRepository ??= new SeedRepository();
            _retailerRepository ??= new RetailerRepository();
            _driverRepository ??= new DriverRepository();
            _orderRepository ??= new OrderRepository();
            _shipmentTripRepository ??= new ShipmentTripRepository();
            _shipmentImageRepository ??= new ShipmentImageRepository();
            _transactionRepository ??= new TransactionRepository();
            _planRepository ??= new PlanRepository();
            _packedProductRepository ??= new PackedProductRepository();
            _orderDetailRepository ??= new OrderDetailRepository();
            _deviceRepository ??= new DeviceRepository();
            _expertPermissionRepository ??= new ExpertPermissionRepository();
            _farmerPermissionRepository ??= new FarmerPermissionRepository();
            _yieldPlanRepository ??= new YieldPlanRepository();
            _periodRepository ??= new PeriodRepository();
            _productionTaskRepository ??= new ProductionTaskRepository();
            _harvestingTaskRepository ??= new HarvestingTaskRepository();
            _packagingTaskRepository ??= new PackagingTaskRepository();
            _inspectingTaskRepository ??= new InspectingTaskRepository();
            _productionImageRepository ??= new ProductionImageRepository();
            _harvestingImageRepository ??= new HarvestingImageRepository();
            _packagingImageRepository ??= new PackagingImageRepository();
            _inspectingImageRepository ??= new InspectingImageRepository();
            _pesticideRepository ??= new PesticideRepository();
            _productionPesticideRepository ??= new ProductionPesticideRepository();
            _fertilizerRepository ??= new FertilizerRepository();
            _productionFertilizerRepository ??= new ProductionFertilizerRepository();
            _productionItemRepository ??= new ProductionItemRepository();
            _harvestingItemRepository ??= new HarvestingItemRepository();
            _packagingItemRepository ??= new PackagingItemRepository();
            _inspectingItemRepository ??= new InspectingItemRepository();
        }

        public UnitOfWork(AccountRepository accountRepository, FarmerRepository farmerRepository,
            ExpertRepository expertRepository, ItemRepository itemRepository,YieldRepository yieldRepository,
            SeedRepository seedRepository, RetailerRepository retailerRepository, DriverRepository driverRepository,
            OrderRepository orderRepository, ShipmentTripRepository shipmentTripRepository,
            ShipmentImageRepository shipmentImageRepository, TransactionRepository transactionRepository, PlanRepository planRepository,
            PackedProductRepository packedProductRepository, OrderDetailRepository orderDetailRepository,
            DeviceRepository deviceRepository, ExpertPermissionRepository expertPermissionRepository, FarmerPermissionRepository farmerPermissionRepository,
            YieldPlanRepository yieldPlanRepository, PeriodRepository periodRepository, ProductionTaskRepository productionTaskRepository,
            HarvestingTaskRepository harvestingTaskRepository, PackagingTaskRepository packagingTaskRepository,
            InspectingTaskRepository inspectingTaskRepository, ProductionImageRepository productionImageRepository,
            HarvestingImageRepository harvestingImageRepository, PackagingImageRepository packagingImageRepository, InspectingImageRepository inspectingImageRepository,
            PesticideRepository pesticideRepository, ProductionPesticideRepository productionPesticideRepository,
            FertilizerRepository fertilizerRepository, ProductionFertilizerRepository productionFertilizerRepository,
            ProductionItemRepository productionItemRepository, HarvestingItemRepository harvestingItemRepository,
            PackagingItemRepository packagingItemRepository, InspectingItemRepository inspectingItemRepository)
        {
            _accountRepository = accountRepository;
            _farmerRepository = farmerRepository;
            _expertRepository = expertRepository;
            _itemRepository = itemRepository;
            _yieldRepository = yieldRepository;
            _seedRepository = seedRepository;
            _retailerRepository = retailerRepository;
            _driverRepository = driverRepository;
            _orderRepository = orderRepository;
            _shipmentTripRepository = shipmentTripRepository;
            _shipmentImageRepository = shipmentImageRepository;
            _transactionRepository = transactionRepository;
            _planRepository = planRepository;
            _packedProductRepository = packedProductRepository;
            _orderDetailRepository = orderDetailRepository;
            _deviceRepository = deviceRepository;
            _expertPermissionRepository = expertPermissionRepository;
            _farmerPermissionRepository = farmerPermissionRepository;
            _yieldPlanRepository = yieldPlanRepository;
            _periodRepository = periodRepository;
            _productionTaskRepository = productionTaskRepository;
            _harvestingTaskRepository = harvestingTaskRepository;
            _packagingTaskRepository = packagingTaskRepository;
            _inspectingTaskRepository = inspectingTaskRepository;
            _productionImageRepository = productionImageRepository;
            _harvestingImageRepository = harvestingImageRepository;
            _packagingImageRepository = packagingImageRepository;
            _inspectingImageRepository = inspectingImageRepository;
            _pesticideRepository = pesticideRepository;
            _productionPesticideRepository = productionPesticideRepository;
            _fertilizerRepository = fertilizerRepository;
            _productionFertilizerRepository = productionFertilizerRepository;
            _productionItemRepository = productionItemRepository;
            _harvestingItemRepository = harvestingItemRepository;
            _packagingItemRepository = packagingItemRepository;
            _inspectingItemRepository = inspectingItemRepository;
        }

        public AccountRepository AccountRepository { get { return _accountRepository; } }
        public FarmerRepository FarmerRepository { get { return _farmerRepository; } }
        public ExpertRepository ExpertRepository { get { return _expertRepository; } }
        public ItemRepository ItemRepository { get { return _itemRepository; } }
        public YieldRepository YieldRepository { get {return _yieldRepository; } }
        public SeedRepository SeedRepository { get { return _seedRepository; } }
        public RetailerRepository RetailerRepository { get { return _retailerRepository; } }
        public DriverRepository DriverRepository { get { return _driverRepository; } }
        public OrderRepository OrderRepository { get { return _orderRepository; } }
        public ShipmentTripRepository ShipmentTripRepository { get { return _shipmentTripRepository; } }
        public ShipmentImageRepository ShipmentImageRepository { get { return _shipmentImageRepository; } }
        public TransactionRepository TransactionRepository { get { return _transactionRepository; } }
        public PlanRepository PlanRepository { get { return _planRepository; } }
        public PackedProductRepository PackedProductRepository { get { return _packedProductRepository; } }
        public OrderDetailRepository OrderDetailRepository { get { return _orderDetailRepository; } }
        public DeviceRepository DeviceRepository { get { return _deviceRepository; } }
        public ExpertPermissionRepository ExpertPermissionRepository { get { return _expertPermissionRepository; } }
        public FarmerPermissionRepository FarmerPermissionRepository { get { return _farmerPermissionRepository; } }
        public YieldPlanRepository YieldPlanRepository { get { return _yieldPlanRepository; } }
        public PeriodRepository PeriodRepository { get { return _periodRepository; } }
        public ProductionTaskRepository ProductionTaskRepository { get { return _productionTaskRepository; } }
        public HarvestingTaskRepository HarvestingTaskRepository { get {return _harvestingTaskRepository; } }
        public PackagingTaskRepository PackagingTaskRepository { get { return _packagingTaskRepository; } }
        public InspectingTaskRepository InspectingTaskRepository { get { return _inspectingTaskRepository; } }
        public ProductionImageRepository ProductionImageRepository { get { return _productionImageRepository; } }
        public HarvestingImageRepository HarvestingImageRepository { get { return _harvestingImageRepository; } }
        public PackagingImageRepository PackagingImageRepository { get { return _packagingImageRepository; } }
        public InspectingImageRepository InspectingImageRepository { get { return _inspectingImageRepository; } }
        public PesticideRepository PesticideRepository { get { return _pesticideRepository; } }
        public ProductionPesticideRepository ProductionPesticideRepository { get { return _productionPesticideRepository; } }
        public FertilizerRepository FertilizerRepository {  get { return _fertilizerRepository; } }
        public ProductionFertilizerRepository ProductionFertilizerRepository { get { return _productionFertilizerRepository; } }
        public ProductionItemRepository ProductionItemRepository { get { return _productionItemRepository; } }
        public HarvestingItemRepository HarvestingItemRepository { get { return _harvestingItemRepository; } }
        public PackagingItemRepository PackagingItemRepository { get { return _packagingItemRepository; } }
        public InspectingItemRepository InspectingItemRepository { get { return _inspectingItemRepository; } }
    }
}