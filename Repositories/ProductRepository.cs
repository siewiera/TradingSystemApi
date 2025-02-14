using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public ProductRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckProductDataExists(int storeId, string name)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync
                (
                    p =>
                    p.Name == name &&
                    p.StoreId == storeId
                );

            if (product != null)
                throw new ConflictException("Product exists");
        }

        public async Task<int> GetMaxProductCode(int storeId) 
        {
            int maxProductCode = 0;
            var products = await _dbContext
                .Products
                .Select(p => p.ProductCode)
                .ToListAsync();

            if(products.Any())
                maxProductCode = products.Max();

            return maxProductCode;
        }

        /**/

        public async Task AddNewProduct(Product product)
        {
            await _dbContext.Products.AddRangeAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductData(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductDataById(int storeId, int productId)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.Id == productId);

            if (product == null)
                throw new NotFoundException("Product not found");

            return product;
        }

        public async Task<Product> GetProductDataByProductCode(int storeId, int productCode)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.ProductCode == productCode);

            if (product == null)
                throw new NotFoundException("Product not found");

            return product;
        }

        public async Task<Product> GetProductDataByBarcode(int storeId, string barcode)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.Barcodes.Any(b => b.Code == barcode));

            if (product == null)
                throw new NotFoundException("Product not found");

            return product;
        }



        public async Task<IEnumerable<Product>> GetProductsDataByName(int storeId, string productName)
        {
            var products = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)
                .Where(p => p.StoreId == storeId && p.Name == productName)
                .ToListAsync();

            if (!products.Any())
                throw new NotFoundException("Product not found");

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsDataByProductCategoryId(int storeId, int productCategoryId)
        {
            var products = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)
                .Where(p => p.StoreId == storeId && p.ProductCategory.Id == productCategoryId)
                .ToListAsync();

            if (!products.Any())
                throw new NotFoundException("Product not found");

            return products;
        }

        public async Task<IEnumerable<Product>> GetAllProductsData(int storeId)
        {
            var products = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.Barcodes)     
                .Where(p => p.StoreId == storeId)
                .ToListAsync();

            if (!products.Any())
                throw new NotFoundException("Product not found");

            return products;
        }
    }
}
