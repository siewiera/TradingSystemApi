using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly TradingSystemDbContext _dbContext;
        public ProductCategoryRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckProductCategoryExists(ProductCategory productCategory_, int storeId)
        {
            var productCategory = await _dbContext
                .ProductCategories
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.Name == productCategory_.Name);

            if (productCategory != null)
                throw new ConflictException("Product category exists");
        }

        /**/

        public async Task AddNewProductCategory(ProductCategory productCategory)
        {
            await _dbContext.ProductCategories.AddAsync(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductCategoryData(ProductCategory productCategory)
        {
            _dbContext.ProductCategories.Update(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductCategory(ProductCategory productCategory)
        {
            _dbContext.ProductCategories.Remove(productCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductCategory> GetProductCategoryDataById(int storeId, int productCategoryId)
        {
            var productCategory = await _dbContext
                .ProductCategories
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.Id == productCategoryId);

            if (productCategory == null)
                throw new NotFoundException("Product category not found");

            return productCategory;
        }

        public async Task<ProductCategory> GetProductCategoryDataByName(int storeId, string productCategoryName)
        {
            var productCategory = await _dbContext
                .ProductCategories
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.Name == productCategoryName);

            if (productCategory == null)
                throw new NotFoundException("Product category not found");

            return productCategory;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesData(int storeId)
        {
            var productCategories = await _dbContext
                .ProductCategories
                .Where(p => p.StoreId == storeId)
                .ToListAsync();

            if (!productCategories.Any())
                throw new NotFoundException("Product category not found");

            return productCategories;
        }
    }
}