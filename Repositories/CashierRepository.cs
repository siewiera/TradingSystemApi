using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.CashierDto;

namespace TradingSystemApi.Repositories
{
    public class CashierRepository : ICashierRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public CashierRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cashier> GetAdminCashierData() 
        { 
            var admin = await _dbContext
                .Cashiers
                .FirstOrDefaultAsync(c => c.Username == "Admin");

            return admin;
        }

        public async Task CheckCashierUsernameExists(int storeId, int sellerId, string username)
        {
            var cashier = await _dbContext
                .Cashiers
                .Include(c => c.Seller)
                .FirstOrDefaultAsync(c => c.Seller.StoreId == storeId && c.Username == username && c.Seller.Id == sellerId);

            if (cashier != null)
                throw new ConflictException("The provided Username already exists");
        }

        public async Task<Cashier> GetCashierDataByUsername(int storeId, int sellerId, string username)
        {
            var cashier = await _dbContext
                .Cashiers
                .Include (c => c.Session)
                .Include(c => c.Seller)
                .FirstOrDefaultAsync(c => c.Username == username && c.SellerId == sellerId && c.Seller.StoreId == storeId);

            if (cashier == null)
                throw new NotFoundException("Cashier not found");

            return cashier;
        }

        public async Task CheckCashierAccount(int storeId, int sellerId, int cashierId) 
        {
            var cashier = await GetCashierDataById(storeId, sellerId, cashierId);
 
            if(!cashier.Active)
                throw new Exception("Cashier is not active");
            else if(cashier.Blocked)
                throw new Exception("Cashier is blocked");

        }

        /**/

        public async Task<Cashier> GetCashierDataById(int storeId, int sellerId, int cashierId)
        {
            var cashier = await _dbContext
                .Cashiers
                .Include (c => c.Session)
                .Include(c => c.Seller)
                .FirstOrDefaultAsync(c => c.Id == cashierId && c.SellerId == sellerId && c.Seller.StoreId == storeId);

            if (cashier == null)
                throw new NotFoundException("Cashier not found");

            return cashier;
        }

        public async Task AddNewCashier(Cashier cashier)
        {
            await _dbContext.Cashiers.AddRangeAsync(cashier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCashierData(Cashier cashier)
        {
            _dbContext.Cashiers.Update(cashier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCashier(Cashier cashier)
        {
            _dbContext.Cashiers.Remove(cashier);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cashier>> GetAllCashierData(int storeId, int sellerId)
        {
            var cashiers = await _dbContext
                .Cashiers
                .Include (c => c.Session)
                .Include(c => c.Seller)
                .Where(c => c.Seller.StoreId == storeId && c.SellerId == sellerId)
                .ToListAsync();

            if (!cashiers.Any())
                throw new NotFoundException("Cashier not found");

            return cashiers;
        }
    }
}
