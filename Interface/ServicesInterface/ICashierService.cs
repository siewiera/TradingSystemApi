using TradingSystemApi.Models.CashierDto;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface ICashierService
    {
        Task<int> AddNewCashier(AddingNewCashierDto dto, int storeId, int sellerId);
        Task DeleteCashierById(int storeId, int sellerId, int cashierId);
        Task<IEnumerable<CashierDto>> GetAllCashierData(int storeId, int sellerId);
        Task<CashierDto> GetCashierDataById(int storeId, int sellerId, int cashierId);
        Task UpdateCashierDataById(UpdateCashierDetailsDto dto, int storeId, int sellerId, int cashierId);
    }
}