using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Entities;
using TradingSystemApi.Models.CashierDto;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;

namespace TradingSystemApi.Services
{
    public class CashierService : ICashierService
    {
        private readonly IMapper _mapper;
        private readonly BCryptHash _bCryptHash;
        private readonly IStoreRepository _storeRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly ISellerRepository _sellerRepository;

        public CashierService(IMapper mapper, BCryptHash bCryptHash, IStoreRepository storeRepository, ICashierRepository cashierRepository, ISellerRepository sellerRepository)
        {
            _mapper = mapper;
            _bCryptHash = bCryptHash;
            _storeRepository = storeRepository;
            _cashierRepository = cashierRepository;
            _sellerRepository = sellerRepository;
        }


        public async Task<int> AddNewCashier(AddingNewCashierDto dto, int storeId, int sellerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);
            await _cashierRepository.CheckCashierUsernameExists(storeId, sellerId, dto.Username);

            var cashier = _mapper.Map<Cashier>(dto);
            cashier.Password = _bCryptHash.HashPassword(dto.Password);
            cashier.UserRole = Enum.UserRole.Cashier;
            cashier.CreatedAt = DateTime.Now;
            cashier.Blocked = false;
            cashier.Active = true;
            cashier.SellerId = sellerId;

            await _cashierRepository.AddNewCashier(cashier);

            return cashier.Id;
        }

        public async Task UpdateCashierDataById(UpdateCashierDetailsDto dto, int storeId, int sellerId, int cashierId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);

            var cashier = await _cashierRepository.GetCashierDataById(storeId, sellerId, cashierId);
            if (cashier.Id != cashierId)
                await _cashierRepository.GetCashierDataByUsername(storeId, sellerId, dto.Username);

            cashier.Username = dto.Username;
            cashier.Password = _bCryptHash.HashPassword(dto.Password);
            cashier.Blocked = dto.Blocked;

            await _cashierRepository.UpdateCashierData(cashier);
        }

        public async Task DeleteCashierById(int storeId, int sellerId, int cashierId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);

            var cashier = await _cashierRepository.GetCashierDataById(storeId, sellerId, cashierId);
            await _cashierRepository.DeleteCashier(cashier);
        }

        public async Task<CashierDto> GetCashierDataById(int storeId, int sellerId, int cashierId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);

            var cashier = await _cashierRepository.GetCashierDataById(storeId, sellerId, cashierId);
            var cashierDto = _mapper.Map<CashierDto>(cashier);

            return cashierDto;
        }

        public async Task<IEnumerable<CashierDto>> GetAllCashierData(int storeId, int sellerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerById(storeId, sellerId);

            var cashiers = await _cashierRepository.GetAllCashierData(storeId, sellerId);
            var cashierDto = _mapper.Map<IEnumerable<CashierDto>>(cashiers);

            return cashierDto;
        }
    }
}
