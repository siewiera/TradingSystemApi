using TradingSystemApi.Models.AdressDto;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IAdressService
    {
        Task<int> AddingNewAdress(AddingNewAdressDto dto, int storeId);
        Task DeleteAdressById(int storeId, int adressId);
        Task<AdressDto> GetAdressDataById(int storeId, int adressId);
        Task<IEnumerable<AdressDto>> GetAllAdressesData(int storeId);
        Task UpdateAdressDataById(UpdateAdressDto dto, int storeId, int adressId);
    }
}