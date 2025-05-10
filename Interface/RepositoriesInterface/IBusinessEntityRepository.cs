using TradingSystemApi.Entities.BusinessEntities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IBusinessEntityRepository<T> where T : BusinessEntity
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll(int storeId);
        Task<T> GetById(int storeId, int entityId);
        Task<T> GetByTaxId(int storeId, string taxId);
        Task Update(T entity);
        Task CheckTaxIdExists(int storeId, string taxId);
    }
}