using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Entities;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Interface.RepositoriesInterface;

namespace TradingSystemApi.Services
{
    public class AdressService : IAdressService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepositor;
        private readonly IAdressRepository _adressRepository;

        public AdressService(IMapper mapper, IStoreRepository storeRepositor, IAdressRepository adressRepository)
        {
            _mapper = mapper;
            _storeRepositor = storeRepositor;
            _adressRepository = adressRepository;
        }


        public async Task<int> AddingNewAdress(AddingNewAdressDto dto, int storeId)
        {
            await _storeRepositor.CheckStoreById(storeId);
            var adress = _mapper.Map<Adress>(dto);
            adress.StoreId = storeId;
            await _adressRepository.CheckAdressDataExists(adress, storeId);
            await _adressRepository.AddingNewAdress(adress);

            return adress.Id;
        }

        public async Task UpdateAdressDataById(UpdateAdressDto dto, int storeId, int adressId)
        {
            await _storeRepositor.CheckStoreById(storeId);
            var adress = await _adressRepository.GetAdressDataById(storeId, adressId);

            if(!(adress.Street == dto.Street &&
                adress.HouseNo == dto.HouseNo &&
                adress.City == dto.City &&
                adress.ZipCode == dto.ZipCode &&
                adress.Country == dto.Country &&
                adress.StoreId == storeId))
                await _adressRepository.CheckAdressDataExists(adress, storeId);


            adress.Street = dto.Street;
            adress.HouseNo = dto.HouseNo;
            adress.City = dto.City;
            adress.ZipCode = dto.ZipCode;
            adress.Country = dto.Country;
            await _adressRepository.UpdateAdressData(adress);
        }

        public async Task DeleteAdressById(int storeId, int adressId)
        {
            await _storeRepositor.CheckStoreById(storeId);
            var adress = await _adressRepository.GetAdressDataById(storeId, adressId);
            await _adressRepository.DeleteAdress(adress);
        }

        public async Task<IEnumerable<AdressDto>> GetAllAdressesData(int storeId)
        {
            await _storeRepositor.CheckStoreById(storeId);
            var adresses = await _adressRepository.GetAllAdressesData(storeId);
            var adressDto = _mapper.Map<IEnumerable<AdressDto>>(adresses);
            return adressDto;
        }

        public async Task<AdressDto> GetAdressDataById(int storeId, int adressId)
        {
            await _storeRepositor.CheckStoreById(storeId);
            var adress = await _adressRepository.GetAdressDataById(storeId, adressId);
            var adressDto = _mapper.Map<AdressDto>(adress);
            return adressDto;
        }
    }
}
