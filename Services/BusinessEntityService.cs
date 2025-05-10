using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using TradingSystemApi.Entities;
using TradingSystemApi.Entities.BusinessEntities;
using TradingSystemApi.Entities.BusinessEntities.Customer;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.BusinessEntityDto;
using TradingSystemApi.Repositories;

namespace TradingSystemApi.Services
{
    public class BusinessEntityService<T> : IBusinessEntityService<T> where T : BusinessEntity
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IBusinessEntityRepository<T> _businessEntityRepository;
        private readonly IAdressRepository _adressRepository;

        public BusinessEntityService(IMapper mapper, IStoreRepository storeRepository, IBusinessEntityRepository<T> businessEntityRepository, IAdressRepository adressRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _businessEntityRepository = businessEntityRepository;
            _adressRepository = adressRepository;
        }

        public async Task<int> AddNewBusinessEntityWithExistingAdress(AddBusinessEntityDataWithExistingAdressDto dto, int storeId, int adressId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _businessEntityRepository.CheckTaxIdExists(storeId, dto.TaxId);
            await _adressRepository.GetAdressDataById(storeId, adressId);

            var businessEntity = _mapper.Map<T>(dto);
            businessEntity.AdressId = adressId;
            businessEntity.StoreId = storeId;

            await _businessEntityRepository.Add(businessEntity);
            await _storeRepository.SaveChanges();

            return businessEntity.Id;
        }

        public async Task<int> AddNewBusinessEntityWithNewAdress(AddBusinessEntityDetailsWithNewAdressDto dto, int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _businessEntityRepository.CheckTaxIdExists(storeId, dto.TaxId);

            Adress adress = new Adress();
            adress.Street = dto.Street;
            adress.HouseNo = dto.HouseNo;
            adress.City = dto.City;
            adress.ZipCode = dto.ZipCode;
            adress.Country = dto.Country;

            await _adressRepository.CheckAdressDataExists(adress, storeId);

            var businessEntity = _mapper.Map<T>(dto);
            businessEntity.StoreId = storeId;
            businessEntity.Adress.StoreId = storeId;

            await _businessEntityRepository.Add(businessEntity);
            await _storeRepository.SaveChanges();

            return businessEntity.Id;
        }


        public async Task UpdateBusinessEntityDataById(UpdateBusinessEntityDataDto dto, int storeId, int businessEntityId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var businessEntity = await _businessEntityRepository.GetById(storeId, businessEntityId);
            var adress = await _adressRepository.GetAdressDataById(storeId, dto.AdressId);

            if (businessEntity.TaxId != dto.TaxId)
                await _businessEntityRepository.CheckTaxIdExists(storeId, dto.TaxId);

            businessEntity.Name = dto.Name;
            businessEntity.TaxId = dto.TaxId;
            businessEntity.AdressId = dto.AdressId;

            await _businessEntityRepository.Update(businessEntity);
            await _storeRepository.SaveChanges();
        }

        public async Task DeleteBusinessEntityById(int storeId, int businessEntityId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var businessEntity = await _businessEntityRepository.GetById(storeId, businessEntityId);
            await _businessEntityRepository.Delete(businessEntity);
            await _storeRepository.SaveChanges();
        }

        public async Task<BusinessEntityDto> GetBusinessEntityDataById(int storeId, int businessEntityId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var businessEntity = await _businessEntityRepository.GetById(storeId, businessEntityId);
            var businessEntityDto = _mapper.Map<BusinessEntityDto>(businessEntity);

            return businessEntityDto;
        }

        public async Task<BusinessEntityDto> GetBusinessEntityDataByTaxId(int storeId, string taxId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var businessEntity = await _businessEntityRepository.GetByTaxId(storeId, taxId);
            var businessEntityDto = _mapper.Map<BusinessEntityDto>(businessEntity);

            return businessEntityDto;
        }

        public async Task<IEnumerable<BusinessEntityDto>> GetAllBusinessEntities(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var businessEntities = await _businessEntityRepository.GetAll(storeId);
            var businessEntitiesDto = _mapper.Map<IEnumerable<BusinessEntityDto>>(businessEntities);

            return businessEntitiesDto;
        }
    }
}
