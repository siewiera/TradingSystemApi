using TradingSystemApi.Models.Product;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IProductService
    {
        Task<int> AddNewProduct(AddNewProductDto dto, int storeId);
        Task DeleteProductById(int storeId, int productId);
        Task<IEnumerable<ProductDto>> GetAllProductsData(int storeId);
        Task<ProductDto> GetProductDataByBarcode(int storeId, string barcode);
        Task<ProductDto> GetProductDataById(int storeId, int productId);
        Task<ProductDto> GetProductDataByProductCode(int storeId, int productCode);
        Task<IEnumerable<ProductDto>> GetProductsDataByName(int storeId, string productName);
        Task<IEnumerable<ProductDto>> GetProductsDataByProductCategoryId(int storeId, int productCategoryId);
        Task UpdateProductDataById(UpdateProductDataDto dto, int storeId, int productId);
    }
}