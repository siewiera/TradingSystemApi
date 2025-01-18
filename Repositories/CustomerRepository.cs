using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.Customer;

namespace TradingSystemApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TradingSystemDbContext _dbContext;

        public CustomerRepository(TradingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckCustomerTaxIdExists(int storeId, string taxId)
        {
            var customer = await _dbContext
                .Customers
                .Include(s => s.Store)
                .FirstOrDefaultAsync(s => s.TaxId == taxId && s.StoreId == storeId);

            if (customer != null)
                throw new ConflictException("There is already a customer with this taxId");
        }

        /**/

        public async Task AddNewCustomer(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomerData(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerDataById(int storeId, int customerId)
        {
            var customer = await _dbContext
                .Customers
                .Include(s => s.Adress)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(s => s.Id == customerId && s.StoreId == storeId);

            if (customer == null)
                throw new NotFoundException("Customer not found");

            return customer;
        }

        public async Task<Customer> GetCustomerDataByTaxId(int storeId, string taxId)
        {
            var customer = await _dbContext
                .Customers
                .Include(s => s.Adress)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(s => s.StoreId == storeId && s.TaxId == taxId);

            if (customer == null)
                throw new NotFoundException("Customer not found");

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersData(int storeId)
        {
            var customers = await _dbContext
                .Customers
                .Include(s => s.Store)
                .Include(s => s.Adress)
                .Where(s => s.StoreId == storeId)
                .ToArrayAsync();

            if (!customers.Any())
                throw new NotFoundException("Customer not found");

            return customers;
        }
    }
}
