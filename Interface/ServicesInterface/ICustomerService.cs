using TradingSystemApi.Models.Customer;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface ICustomerService
    {
        Task<int> AddCustomerDataWithExistingtAdress(AddCustomerDetailsWithExistingtAdressDto dto, int storeId, int adressId);
        Task<int> AddCustomerDataWithNewAdress(AddCustomerDetailsWithNewAdressDto dto, int storeId);
        Task DeleteCustomerById(int storeId, int customerId);
        Task<IEnumerable<CustomerDto>> GetAllCustomersData(int storeId);
        Task<CustomerDto> GetCustomerDataById(int storeId, int customerId);
        Task<CustomerDto> GetCustomerDataByTaxId(int storeId, string taxId);
        Task UpdateCustomerDataById(UpdateCustomerDetailsDto dto, int storeId, int customerId);
    }
}