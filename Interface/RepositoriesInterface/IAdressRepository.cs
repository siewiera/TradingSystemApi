using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IAdressRepository
    {
        Task AddingNewAdress(Adress adress);
        Task CheckAdressDataExists(Adress adress_, int storeId);
        Task DeleteAdress(Adress adress);
        Task<Adress> GetAdressDataById(int storeId, int adressId);
        Task<IEnumerable<Adress>> GetAllAdressesData(int storeId);
        Task UpdateAdressData(Adress adress);
    }
}