using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class SalesDocumentRepository<T> : ISalesDocumentRepository<T> where T : SalesDocument
    {
        private readonly TradingSystemDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public SalesDocumentRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsyc(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int storeId, int entityId)
        {
            var entity = await _dbSet
                .Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.Id == entityId);

            if (entity == null)
                throw new NotFoundException("Invoice not found");

            return entity;
        }

        public async Task<T> GetByInvoiceNumberAsync(int storeId, string invoiceNo)
        {
            var entity = await _dbSet
                .Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.InvoiceNo == invoiceNo);

            if (entity == null)
                throw new NotFoundException("Invoice not found");

            return entity;
        }


        public async Task<IEnumerable<T>> GetByCustomerIdAsync(int storeId, int customerId)
        {
            var entities = await _dbSet
                .Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Where(e => e.StoreId == storeId && e.CustomerId == customerId)
                .ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Invoice not found");

            return entities;
        }


        public async Task<IEnumerable<T>> GetAllByCashierIdAsync(int storeId, int cashierId)
        {
            var entities = await _dbSet
                .Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Where(e => e.StoreId == storeId && e.CashierId == cashierId)
                .ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Invoice not found");

            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int storeId)
        {
            var entities = await _dbSet
                .Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Where(e => e.StoreId == storeId).
                ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Invoice not found");

            return entities;
        }
    }
}
