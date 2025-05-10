using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.Product;

namespace TradingSystemApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
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
            _productCategoryRepository = productCategoryRepository;
            _barcodeRepository = barcodeRepository;
        }

        public decimal CalculateMargin(decimal costNetPrice, decimal productMargin = 3, bool percentageMargin = true) 
        {
            decimal sellingNetPrice = 0;
            if (percentageMargin)
                sellingNetPrice = costNetPrice + (costNetPrice * productMargin) / 100;
            else
                sellingNetPrice = costNetPrice + productMargin;

            return decimal.Round(sellingNetPrice, 2);
        }

        public async Task<int> GenerationPorductCode(int storeId) 
        {
            int newProductCode = 1000;
            var maxProductCode = await _productRepository.GetMaxProductCode(storeId);

            if (maxProductCode > 0)
                newProductCode = maxProductCode + 10;

            return newProductCode;
        }

        public async Task UpdateProductQuantity(int storeId, int productId, decimal quantity)  
        {
            var product = await GetProductDataById(storeId, productId);
            var productDataDto = new UpdateProductDataDto();
            productDataDto.Quantity = product.Quantity + quantity;

            //+logika do tabeli przesuniec
            await UpdateProductDataById(productDataDto, storeId, productId);
        }   
        /**/


        public async Task<int> AddNewProduct(AddNewProductDto dto, int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _productCategoryRepository.GetProductCategoryDataById(storeId, dto.ProductCategoryId);

            var product = _mapper.Map<Product>(dto);
            product.StoreId = storeId;
            product.SellingPrice = CalculateMargin(product.CostNetPrice, product.ProductMargin, product.PercentageMargin);
            product.ProductCode = await GenerationPorductCode(storeId);
            product.CreationDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;

            await _productRepository.CheckProductDataExists(storeId, dto.Name);
            await _productRepository.AddNewProduct(product);
            await _storeRepository.SaveChanges();

            return product.Id;
        }

        public async Task UpdateProductDataById(UpdateProductDataDto dto, int storeId, int productId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var product = await _productRepository.GetProductDataById(storeId, productId);
            await _productCategoryRepository.GetProductCategoryDataById(storeId, dto.ProductCategoryId ?? product.ProductCategoryId);

            if (product.Name != dto.Name)
                await _productRepository.CheckProductDataExists(storeId, dto.Name);

            product.Name = dto.Name ?? product.Name;
            product.JM = dto.JM ?? product.JM;
            product.CostNetPrice = dto.CostNetPrice ?? product.CostNetPrice;
            product.Vat = dto.Vat ?? product.Vat;
            product.ProductMargin = dto.ProductMargin ?? product.ProductMargin;
            product.PercentageMargin = dto.PercentageMargin ?? product.PercentageMargin;
            product.SellingPrice = CalculateMargin(dto.CostNetPrice ?? product.CostNetPrice, dto.ProductMargin ?? product.ProductMargin, 
                dto.PercentageMargin ?? product.PercentageMargin);
            product.Quantity = dto.Quantity ?? product.Quantity;
            product.ProductCategoryId = dto.ProductCategoryId ?? product.ProductCategoryId;
            product.UpdateDate = DateTime.Now;

            await _productRepository.UpdateProductData(product);
            await _storeRepository.SaveChanges();
        }

        public async Task DeleteProductById(int storeId, int productId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var product = await _productRepository.GetProductDataById(storeId, productId);
            await _productRepository.DeleteProduct(product);
            await _storeRepository.SaveChanges();
        }

        public async Task<ProductDto> GetProductDataById(int storeId, int productId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var product = await _productRepository.GetProductDataById(storeId, productId);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> GetProductDataByProductCode(int storeId, int productCode)
        {
            await _storeRepository.CheckStoreById(storeId);
            var product = await _productRepository.GetProductDataByProductCode(storeId, productCode);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<ProductDto> GetProductDataByBarcode(int storeId, string barcode)
        {
            await _storeRepository.CheckStoreById(storeId);
            var product = await _productRepository.GetProductDataByBarcode(storeId, barcode);
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDataByName(int storeId, string productName)
        {
            await _storeRepository.CheckStoreById(storeId);
            var products = await _productRepository.GetProductsDataByName(storeId, productName);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDataByProductCategoryId(int storeId, int productCategoryId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var products = await _productRepository.GetProductsDataByProductCategoryId(storeId, productCategoryId);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsData(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var products = await _productRepository.GetAllProductsData(storeId);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }
    }
}
