using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IProductCategoryRepository
    {
        Task AddNewProductCategory(ProductCategory productCategory);
        Task CheckProductCategoryExists(ProductCategory productCategory_, int storeId);
        Task DeleteProductCategory(ProductCategory productCategory);
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesData(int storeId);
        Task<ProductCategory> GetProductCategoryDataById(int storeId, int productCategoryId);
        Task<ProductCategory> GetProductCategoryDataByName(int storeId, string productCategoryName);
        Task UpdateProductCategoryData(ProductCategory productCategory);
    }
}