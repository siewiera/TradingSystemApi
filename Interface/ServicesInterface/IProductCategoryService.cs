using TradingSystemApi.Models.ProductCategory;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IProductCategoryService
    {
        Task<int> AddNewProductCategory(AddNewProductCategoryDto dto, int storeId);
        Task DeleteProductCategoryById(int storeId, int productCategoryId);
        Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesData(int storeId);
        Task<ProductCategoryDto> GetProductCategoryDataById(int storeId, int productCategoryId);
        Task<ProductCategoryDto> GetProductCategoryDataByName(int storeId, string productCategoryName);
        Task UpdateProductCategoryDataById(UpdateProductCategoryDataDto dto, int storeId, int productCategoryId);
    }
}