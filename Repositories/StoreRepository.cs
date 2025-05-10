using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public StoreRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        /**/

        public async Task CheckStoreById(int storeId) 
        {
            var store = await _dbContext
                .Stores
                .FirstOrDefaultAsync(s => s.Id == storeId);

            if (store == null)
                throw new NotFoundException("Store not found");
            else if (store.GlobalStore == true)
                throw new ForbiddenException("Store is prohibited");
        }

        public async Task CheckStoreNameExists(string name)
        {
            var store = await _dbContext
                .Stores
                .FirstOrDefaultAsync(s => s.Name == name);

            if (store != null)
                throw new ConflictException("Store name exists");
        }

        /**/

        public async Task<Store> GetStoreDataById(int storeId)
        {
            var store = await _dbContext
                .Stores
                .Include(s => s.BusinessEntities)
                //.Include(s => s.Sellers)
                .Include(s => s.Adresses)
                .Include(s => s.Products)
                //.Include(s => s.Customers)
                .Include(s => s.SalesDocuments)
                .Include(s => s.InventoryMovements)
                .FirstOrDefaultAsync(s => s.Id == storeId);

            if (store == null)
                throw new NotFoundException("Store not found");

            return store;
        }

        public async Task<IEnumerable<Store>> GetAllStoresData()
        {
            var stores = await _dbContext
                    .Stores
                    .Include(s => s.BusinessEntities)
                    //.Include(s => s.Sellers)
                    .Include(s => s.Adresses)
                    .Include(s => s.Products)
                    //.Include(s => s.Customers)
                    .Include(s => s.SalesDocuments)
                    .Include(s => s.InventoryMovements)
                    .Where(s => s.GlobalStore == false)
                    .ToListAsync();

            if (!stores.Any())
                throw new NotFoundException("Stores not found");

            return stores;
        }

        public async Task AddNewStore(Store store)
        {
            await _dbContext.Stores.AddAsync(store);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStoreData(Store store)
        {
            _dbContext.Stores.Update(store);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStore(Store store)
        {
            _dbContext.Stores.Remove(store);
            await _dbContext.SaveChangesAsync();
        }
    }
}
