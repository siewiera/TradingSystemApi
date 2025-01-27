using AutoMapper;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.ProductCategory;

namespace TradingSystemApi.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IMapper mapper, IStoreRepository storeRepository, IProductCategoryRepository productCategoryRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<int> AddNewProductCategory(AddNewProductCategoryDto dto, int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var productCategory = _mapper.Map<ProductCategory>(dto);
            productCategory.StoreId = storeId;
            await _productCategoryRepository.CheckProductCategoryExists(productCategory, storeId);
            await _productCategoryRepository.AddNewProductCategory(productCategory);

            return productCategory.Id;
        }

        public async Task UpdateProductCategoryDataById(UpdateProductCategoryDataDto dto, int storeId, int productCategoryId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var productCategory = await _productCategoryRepository.GetProductCategoryDataById(storeId, productCategoryId);

            if(productCategory.Name != dto.Name)
                await _productCategoryRepository.CheckProductCategoryExists(productCategory, storeId);

            productCategory.Name = dto.Name;
            await _productCategoryRepository.UpdateProductCategoryData(productCategory);
        }

        public async Task DeleteProductCategoryById(int storeId, int productCategoryId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var productCategory = await _productCategoryRepository.GetProductCategoryDataById(storeId, productCategoryId);
            await _productCategoryRepository.DeleteProductCategory(productCategory);
        }

        public async Task<ProductCategoryDto> GetProductCategoryDataById(int storeId, int productCategoryId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var productCategory = await _productCategoryRepository.GetProductCategoryDataById(storeId, productCategoryId);
            var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategory);
            return productCategoryDto;
        }

        public async Task<ProductCategoryDto> GetProductCategoryDataByName(int storeId, string productCategoryName)
        {
            await _storeRepository.CheckStoreById(storeId);
            var productCategory = await _productCategoryRepository.GetProductCategoryDataByName(storeId, productCategoryName);
            var productCategoryDto = _mapper.Map<ProductCategoryDto>(productCategory);
            return productCategoryDto;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesData(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var productCategory = await _productCategoryRepository.GetAllProductCategoriesData(storeId);
            var productCategoriesDto = _mapper.Map<IEnumerable<ProductCategoryDto>>(productCategory);
            return productCategoriesDto;
        }
    }
}
