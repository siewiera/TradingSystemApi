using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.StoreDto;
using TradingSystemApi.Repositories;

namespace TradingSystemApi.Services
{
    public class StoreService : IStoreService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;

        public StoreService(IMapper mapper, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
        }


        public async Task<int> AddNewStore(AddNewStoreDto dto)
        {
            await _storeRepository.CheckStoreNameExists(dto.Name);
            var store = _mapper.Map<Store>(dto);
            await _storeRepository.AddNewStore(store);

            return store.Id;
        }

        public async Task UpdateStoreDataById(AddNewStoreDto dto, int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var store = await _storeRepository.GetStoreDataById(storeId);
            if (dto.Name != store.Name)
                await _storeRepository.CheckStoreNameExists(dto.Name);

            store.Name = dto.Name;
            await _storeRepository.UpdateStoreData(store);
        }

        public async Task DeleteStoreById(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var store = await _storeRepository.GetStoreDataById(storeId);
            await _storeRepository.DeleteStore(store);
        }

        public async Task<StoreDto> GetStoreDataById(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var store = await _storeRepository.GetStoreDataById(storeId);
            var storeDto = _mapper.Map<StoreDto>(store);

            return storeDto;
        }

        public async Task<IEnumerable<StoreDto>> GetAllStoresData()
        {
            var stores = await _storeRepository.GetAllStoresData();
            var storesDto = _mapper.Map<IEnumerable<StoreDto>>(stores);

            return storesDto;
        }
    }
}
