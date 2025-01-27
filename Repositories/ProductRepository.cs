using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;
using TradingSystemApi.Exceptions;

namespace TradingSystemApi.Repositories
{
    public class ProductRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public ProductRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckProductDataExists(Product product_, int storeId) 
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.JM)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync
                (
                    p =>
                    p.Name == product_.Name &&
                    p.JM == product_.JM &&
                    p.ProductCode == product_.ProductCode &&
                    p.CostNetPrice == product_.CostNetPrice &&
                    p.Vat == product_.Vat &&
                    p.SellingPrice == product_.SellingPrice &&
                    p.StoreId == storeId
                );

            if (product != null)
                throw new ConflictException("Product exists");
        }

        /**/

        public async Task AddNewProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
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
                .Include(p => p.JM)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.Id == productId);

            if (product == null)
                throw new NotFoundException("Product not found");

            return product;
        }

        public async Task<Product> GetProductsDataByProductCode(int storeId, int productCode)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.JM)
                .Include(p => p.Barcodes)
                .FirstOrDefaultAsync(p => p.StoreId == storeId && p.ProductCode == productCode);

            if (product == null)
                throw new NotFoundException("Product not found");

            return product;
        }

        public async Task<Product> GetProductsDataByBarcode(int storeId, string barcode)
        {
            var product = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.JM)
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
                .Include(p => p.JM)
                .Include(p => p.Barcodes)
                .Where(p => p.StoreId == storeId && p.Name == productName)
                .ToListAsync();

            if (!products.Any())
                throw new NotFoundException("Product not found");

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsDataByJM(int storeId, JM jm)
        {
            var products = await _dbContext
                .Products
                .Include(p => p.ProductCategory)
                .Include(p => p.JM)
                .Include(p => p.Barcodes)
                .Where(p => p.StoreId == storeId && p.JM == jm)
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
                .Include(p => p.JM)
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
                .Include(p => p.JM)
                .Include(p => p.Barcodes)
                .Where(p => p.StoreId == storeId)
                .ToListAsync();

            if (!products.Any())
                throw new NotFoundException("Product not found");

            return products;
        }
    }
}
