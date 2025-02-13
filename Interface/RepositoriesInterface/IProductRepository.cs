using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IProductRepository
    {
        Task AddNewProduct(Product product);
        Task CheckProductDataExists(Product product_, int storeId);
        Task DeleteProduct(Product product);
        Task<IEnumerable<Product>> GetAllProductsData(int storeId);
        Task<Product> GetProductDataById(int storeId, int productId);
        Task<Product> GetProductDataByBarcode(int storeId, string barcode);
        Task<IEnumerable<Product>> GetProductsDataByName(int storeId, string productName);
        Task<IEnumerable<Product>> GetProductsDataByProductCategoryId(int storeId, int productCategoryId);
        Task<Product> GetProductDataByProductCode(int storeId, int productCode);
        Task UpdateProductData(Product product);
        Task<int> GetMaxProductCode(int storeId);
    }
}