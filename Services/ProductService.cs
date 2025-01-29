using AutoMapper;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.Product;

namespace TradingSystemApi.Services
{
    public class ProductService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBarcodeRepository _barcodeRepository;

        public ProductService
        (
            IMapper mapper, 
            IStoreRepository storeRepository, 
            IProductRepository productRepository,
            IProductCategoryRepository productCategoryRepository,
            IBarcodeRepository barcodeRepository
        )
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _barcodeRepository = barcodeRepository;
        }


        public async Task<int> AddNewProduct(AddNewProductDto dto, int storeId, int productCategoryId) 
        {
            await _storeRepository.CheckStoreById(storeId);
        }


    }
}
