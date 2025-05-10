using TradingSystemApi.Entities.Documents;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IDocumentItemRepository<T> where T : DocumentItem
    {
        Task AddItem(List<T> entities);
        Task DeleteItem(T entity);
        Task DeleteAllItems(List<T> entities);
        Task UpdateItem(List<T> entities);
        Task<T> GetDocumentItemById(int storeId, int documentId, int documentItemId);
        Task<List<T>> GetAllDocumentItemsByDocumentId(int storeId, int documentId);
        Task<decimal> GetItemSumCalculation(int storeId, int documentId);
    }
}