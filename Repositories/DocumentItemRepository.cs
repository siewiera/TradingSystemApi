using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities.Documents;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.ReceiptSaleItem;

namespace TradingSystemApi.Repositories
{
    public class DocumentItemRepository<T> : IDocumentItemRepository<T> where T : DocumentItem
    {
        private readonly TradingSystemDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public DocumentItemRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<decimal> GetItemSumCalculation(int storeId, int documentId) 
        {
            var Documents = await GetAllDocumentItemsByDocumentId(storeId, documentId);
            return Documents.Select(s => s.CostNetPrice * s.Quantity).Sum();
        }

        /**/


        public async Task AddItem(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task UpdateItem(List<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task DeleteItem(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAllItems(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<T> GetDocumentItemById(int storeId, int documentId, int documentItemId)
        {
            var entity = await _dbSet
                .Include(e => e.Product)
                .Include(e => e.Document)
                .FirstOrDefaultAsync(e => e.Document.StoreId == storeId && e.DocumentId == documentId && e.Id == documentItemId);

            if (entity == null)
                throw new NotFoundException("Document element not found");

            return entity;
        }

        public async Task<List<T>> GetAllDocumentItemsByDocumentId(int storeId, int documentId)
        {
            var entities = await _dbSet
                .Include(e => e.Product)
                .Include(e => e.Document)
                .Where(e => e.Document.StoreId == storeId)
                .ToListAsync();

            if (entities == null)
                throw new NotFoundException("Document element not found");

            return entities;
        }
    }
}