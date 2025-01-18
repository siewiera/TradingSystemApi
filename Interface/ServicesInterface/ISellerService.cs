using TradingSystemApi.Models.SellerDto;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface ISellerService
    {
        Task<int> AddSellerDataWithExistingtAdress(AddSellerDetailsWithExistingtAdressDto dto, int storeId, int adressId);
        Task<int> AddSellerDataWithNewAdress(AddSellerDetailsWithNewAdressDto dto, int storeId);
        Task DeleteSellerById(int storeId, int sellerId);
        Task<IEnumerable<SellerDto>> GetAllSellerData(int storeId);
        Task<SellerDto> GetSellerDataById(int storeId, int sellerId);
        Task<SellerDto> GetSellerDataByTaxId(int storeId, string taxId);
        Task UpdateSellerDataById(UpdateSellerDetailsDto dto, int storeId, int sellerId);
    }
}