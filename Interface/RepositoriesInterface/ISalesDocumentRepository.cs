using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ISalesDocumentRepository<T> where T : SalesDocument
    {
        Task AddAsyc(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(int storeId);
        Task<T> GetByIdAsync(int storeId, int entityId);
        Task<T> GetByInvoiceNumberAsync(int storeId, string invoiceNo);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetByCustomerIdAsync(int storeId, int customerId);
        Task<IEnumerable<T>> GetAllByCashierIdAsync(int storeId, int cashierId);
    }
}