using TradingSystemApi.Models.StoreDto;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IStoreService
    {
        Task<int> AddNewStore(AddNewStoreDto dto);
        Task DeleteStoreById(int storeId);
        Task<IEnumerable<StoreDto>> GetAllStoresData();
        Task<StoreDto> GetStoreDataById(int storeId);
        Task UpdateStoreDataById(AddNewStoreDto dto, int storeId);
    }
}