using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IStoreRepository
    {
        Task SaveChanges();
        Task AddNewStore(Store store);
        Task CheckStoreNameExists(string name);
        Task DeleteStore(Store store);
        Task<IEnumerable<Store>> GetAllStoresData();
        Task<Store> GetStoreDataById(int storeId);
        Task UpdateStoreData(Store store);
        Task CheckStoreById(int storeId);
    }
}