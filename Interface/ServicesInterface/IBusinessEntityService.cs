using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Models.BusinessEntityDto;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IBusinessEntityService<T> where T : BusinessEntity
    {
        Task<int> AddNewBusinessEntityWithExistingAdress(AddBusinessEntityDataWithExistingAdressDto dto, int storeId, int adressId);
        Task<int> AddNewBusinessEntityWithNewAdress(AddBusinessEntityDetailsWithNewAdressDto dto, int storeId);
        Task DeleteBusinessEntityById(int storeId, int businessEntityId);
        Task<IEnumerable<BusinessEntityDto>> GetAllBusinessEntities(int storeId);
        Task<BusinessEntityDto> GetBusinessEntityDataById(int storeId, int businessEntityId);
        Task<BusinessEntityDto> GetBusinessEntityDataByTaxId(int storeId, string taxId);
        Task UpdateBusinessEntityDataById(UpdateBusinessEntityDataDto dto, int storeId, int businessEntityId);
    }
}