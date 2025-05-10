using TradingSystemApi.Entities.BusinessEntities.Customer;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface ICustomerRepository
    {
        Task AddNewCustomer(Customer customer);
        Task CheckCustomerTaxIdExists(int storeId, string taxId);
        Task DeleteCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersData(int storeId);
        Task<Customer> GetCustomerDataById(int storeId, int customerId);
        Task<Customer> GetCustomerDataByTaxId(int storeId, string taxId);
        Task UpdateCustomerData(Customer customer);
    }
}