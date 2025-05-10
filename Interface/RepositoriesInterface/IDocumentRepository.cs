using TradingSystemApi.Entities.Documents;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IDocumentRepository<T> where T : Document
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll(int storeId);
        Task<T> GetById(int storeId, int entityId);
        Task<T> GetByInvoiceNumber(int storeId, string invoiceNo);
        Task Update(T entity);
        Task<IEnumerable<T>> GetAllByCustomerId(int storeId, int customerId);
        Task<IEnumerable<T>> GetAllByCashierId(int storeId, int cashierId);
    }
}