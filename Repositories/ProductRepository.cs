using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Repositories
{
    public class ProductRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public ProductRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task CheckProductExists(Product product_) 
        {
            //var product = await _dbContext
            //    .Products
            //    .Include(p => p.ProductCategory)
            //    .Include(p => p.JM)
            //    .Include(p => p.Barcodes)
            //    .FirstOrDefaultAsync
            //    (
            //        p =>
            //        p.Name == product_.Name &&
            //        p.JM == product_.JM &&
            //        p.ProductCode == product_.ProductCode &&
            //        p.CostNetPrice == product_.CostNetPrice &&
            //        p.Vat == product_.Vat &&
            //        p.SellingPrice == product_.SellingPrice &&
            //        p.StoreId == storeId
            //    );
        }
    }
}
