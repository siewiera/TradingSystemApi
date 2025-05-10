using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities.BusinessEntities.Seller;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.SellerDto;

namespace TradingSystemApi.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public SellerRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckSellerById(int storeId, int sellerId) 
        {
            var seller = await _dbContext
                .Sellers
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.Id == sellerId);

            if (seller == null)
                throw new NotFoundException("Seller not found");
        }

        public async Task CheckSellerTaxIdExists(int storeId, string taxId, bool checkStoreId = true)
        {
            var seller = await _dbContext
                .Sellers
                .Include(s => s.Store)
                .FirstOrDefaultAsync(s => s.TaxId == taxId && s.StoreId == storeId);

            if (seller != null)
                throw new ConflictException("There is already a seller with this taxId");
        }

        /**/

        public async Task AddNewSeller(Seller seller)
        {
            await _dbContext.Sellers.AddAsync(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSellerData(Seller seller)
        {
            _dbContext.Sellers.Update(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSeller(Seller seller)
        {
            _dbContext.Sellers.Remove(seller);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Seller> GetSellerDataById(int storeId, int sellerId)
        {
            var seller = await _dbContext
                .Sellers
                .Include(s => s.Adress)
                .Include(s => s.Store)
                .Include(s => s.Cashiers)
                .FirstOrDefaultAsync(s => s.Id == sellerId && s.StoreId == storeId);

            if (seller == null)
                throw new NotFoundException("Seller not found");

            return seller;
        }


        public async Task<Seller> GetSellerDataByTaxId(int storeId, string taxId)
        {
            var seller = await _dbContext
                .Sellers
                .Include(s => s.Adress)
                .Include(s => s.Store)
                .Include(s => s.Cashiers)
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.TaxId == taxId);

            if (seller == null)
                throw new NotFoundException("Sellers not found");

            return seller;
        }

        public async Task<IEnumerable<Seller>> GetAllSellerData(int storeId)
        {
            var sellers = await _dbContext
                .Sellers
                .Include(s => s.Store)
                .Include(s => s.Adress)
                .Where(s => s.StoreId == storeId)
                .ToListAsync();

            if (!sellers.Any())
                throw new NotFoundException("Sellers not found");

            return sellers;
        }
    }
}
