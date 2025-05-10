using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Enum;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Repositories
{
    public class BusinessEntityRepository<T> : IBusinessEntityRepository<T> where T : BusinessEntity
    {
        private readonly TradingSystemDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BusinessEntityRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task CheckTaxIdExists(int storeId, string taxId) 
        {
            var entity = _dbSet
                .Include(e => e.Adress)
                .Include(e => e.Store)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.TaxId == taxId);

            if(entity != null)
                throw new ConflictException("There is already a business entity with this taxId");
        }

        /**/

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
                .Include(e => e.Adress)
                .Include(e => e.Store)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.Id == entityId);

            if (entity == null)
                throw new NotFoundException("Business Entity not found");

            return entity;
        }

        public async Task<T> GetByTaxId(int storeId, string taxId)
        {
            var entity = await _dbSet
                .Include(e => e.Adress)
                .Include(e => e.Store)
                .FirstOrDefaultAsync(e => e.StoreId == storeId && e.TaxId == taxId);

            if (entity == null)
                throw new NotFoundException("Business Entity not found");

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll(int storeId)
        {
            var entities = await _dbSet
                .Include(e => e.Adress)
                .Include(e => e.Store)
                .Where(e => e.StoreId == storeId)
                .ToListAsync();

            if (!entities.Any())
                throw new NotFoundException("Business Entity not found");

            return entities;
        }
    }
}
