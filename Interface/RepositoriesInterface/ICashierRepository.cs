using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ICashierRepository
    {
        Task AddNewCashier(Cashier cashier);
        Task CheckCashierUsernameExists(int storeId, int sellerId, string username);
        Task DeleteCashier(Cashier cashier);
        Task<IEnumerable<Cashier>> GetAllCashierData(int storeId, int sellerId);
        Task<Cashier> GetCashierDataByUsername(int storeId, int sellerId, string username);
        Task<Cashier> GetCashierDataById(int storeId, int sellerId, int cashierId);
        Task UpdateCashierData(Cashier cashier);
        Task<Cashier> GetAdminCashierData();
        Task CheckCashierAccount(int storeId, int sellerId, int cashierId);
    }
}