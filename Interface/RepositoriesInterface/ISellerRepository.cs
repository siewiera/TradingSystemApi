using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ISellerRepository
    {
        Task AddNewSeller(Seller seller);
        Task CheckSellerTaxIdExists(int storeId, string taxId, bool checkStoreId = true);
        Task DeleteSeller(Seller seller);
        Task<IEnumerable<Seller>> GetAllSellerData(int storeId);
        Task<Seller> GetSellerDataById(int storeId, int sellerId);
        Task<Seller> GetSellerDataByTaxId(int storeId, string taxId);
        Task UpdateSellerData(Seller seller);
        Task CheckSellerById(int storeId, int sellerId);
    }
}