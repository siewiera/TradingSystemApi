using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Repositories
{
    public class ProductCategoryRepository
    {
        private readonly TradingSystemApi _dbContext;
        public ProductCategoryRepository(TradingSystemApi dbContext)  
        {
            _dbContext = dbContext;            
        }

        public async Task AddNewProductCategory(ProductCategory productCategory) 
        {
            await _dbContext.ProductCategorys.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryData(ProductCategory productCategory) 
        {
            _dbContext.ProductCategorys.Update(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductCategory(ProductCategory productCategory) 
        {
            _dbContext.ProductCategorys.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}