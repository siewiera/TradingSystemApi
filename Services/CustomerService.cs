using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.Customer;
using TradingSystemApi.Models.SellerDto;

namespace TradingSystemApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAdressRepository _adressRepository;

        public CustomerService(IMapper mapper, IStoreRepository storeRepository, ICustomerRepository customerRepository, IAdressRepository adressRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _customerRepository = customerRepository;
            _adressRepository = adressRepository;
        }


        public async Task<int> AddCustomerDataWithExistingtAdress(AddCustomerDetailsWithExistingtAdressDto dto, int storeId, int adressId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _customerRepository.CheckCustomerTaxIdExists(storeId, dto.TaxId);
            await _adressRepository.GetAdressDataById(storeId, adressId);

            var customer = _mapper.Map<Customer>(dto);
            customer.AdressId = adressId;
            customer.StoreId = storeId;

            await _customerRepository.AddNewCustomer(customer);

            return customer.Id;
        }


        public async Task<int> AddCustomerDataWithNewAdress(AddCustomerDetailsWithNewAdressDto dto, int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _customerRepository.CheckCustomerTaxIdExists(storeId, dto.TaxId);

            Adress adress = new Adress();
            adress.Street = dto.Street;
            adress.HouseNo = dto.HouseNo;
            adress.City = dto.City;
            adress.ZipCode = dto.ZipCode;
            adress.Country = dto.Country;

            await _adressRepository.CheckAdressDataExists(adress, storeId);

            var customer = _mapper.Map<Customer>(dto);
            customer.StoreId = storeId;
            customer.Adress.StoreId = storeId;

            await _customerRepository.AddNewCustomer(customer);

            return customer.Id;
        }

        public async Task UpdateCustomerDataById(UpdateCustomerDetailsDto dto, int storeId, int customerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var customer = await _customerRepository.GetCustomerDataById(storeId, customerId);
            var adress = await _adressRepository.GetAdressDataById(storeId, dto.AdressId);

            if (customer.TaxId != dto.TaxId)
                await _customerRepository.CheckCustomerTaxIdExists(storeId, dto.TaxId);

            customer.Name = dto.Name;
            customer.TaxId = dto.TaxId;
            customer.AdressId = dto.AdressId;

            await _customerRepository.UpdateCustomerData(customer);
        }

        public async Task DeleteCustomerById(int storeId, int customerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var customer = await _customerRepository.GetCustomerDataById(storeId, customerId);
            await _customerRepository.DeleteCustomer(customer);
        }

        public async Task<CustomerDto> GetCustomerDataById(int storeId, int customerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var customer = await _customerRepository.GetCustomerDataById(storeId, customerId);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public async Task<CustomerDto> GetCustomerDataByTaxId(int storeId, string taxId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var customer = await _customerRepository.GetCustomerDataByTaxId(storeId, taxId);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersData(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var customers = await _customerRepository.GetAllCustomersData(storeId);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customersDto;
        }
    }
}
