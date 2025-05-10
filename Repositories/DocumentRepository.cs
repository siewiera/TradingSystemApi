using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities.Documents;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class DocumentRepository<T> : IDocumentRepository<T> where T : Document
    {
        private readonly TradingSystemDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public DocumentRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> GetById(int storeId, int entityId)
        {
            var entity = await _dbSet
                .Include(e => e.BusinessEntity)
                //.Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Include(e => e.DocumentItems)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.Id == entityId);

            if (entity == null)
                throw new NotFoundException("Document not found");

            return entity;
        }

        public async Task<T> GetByInvoiceNumber(int storeId, string invoiceNo)
        {
            var entity = await _dbSet
                .Include(e => e.BusinessEntity)
                //.Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Include(e => e.DocumentItems)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.DocumentNumber == invoiceNo);

            if (entity == null)
                throw new NotFoundException("Document not found");

            return entity;
        }


        public async Task<IEnumerable<T>> GetAllByCustomerId(int storeId, int customerId)
        {
            var entities = await _dbSet
                .Include(e => e.BusinessEntity)
                //.Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Include(e => e.DocumentItems)
                .Where(e => e.StoreId == storeId && e.CustomerId == customerId)
                .ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Document not found");

            return entities;
        }


        public async Task<IEnumerable<T>> GetAllByCashierId(int storeId, int cashierId)
        {
            var entities = await _dbSet
                .Include(e => e.BusinessEntity)
                //.Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Include(e => e.DocumentItems)
                .Where(e => e.StoreId == storeId && e.CashierId == cashierId)
                .ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Document not found");

            return entities;
        }

        public async Task<IEnumerable<T>> GetAll(int storeId)
        {
            var entities = await _dbSet
                .Include(e => e.BusinessEntity)
                //.Include(e => e.Customer)
                .Include(e => e.Cashier)
                .Include(e => e.InventoryMovement)
                .Include(e => e.DocumentItems)
                .Where(e => e.StoreId == storeId).
                ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Document not found");

            return entities;
        }
    }
}
