using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.CashierDto;
using TradingSystemApi.Models.InitData;

namespace TradingSystemApi.Services
{
    public class InitService : IInitService
    {
        private readonly IMapper _mapper;
        private readonly BCryptHash _bCryptHash;
        private readonly IStoreRepository _storeRepository;
        private readonly IAdressRepository _adressRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ICashierRepository _cashierRepository;
        private readonly IInitRepository _initRepository;
        private readonly string _passwordAdmin;

        public InitService(
            IMapper mapper,
            BCryptHash bCryptHash,
            IStoreRepository storeRepository,
            IAdressRepository adressRepository,
            ISellerRepository sellerRepository,
            ICashierRepository cashierRepository,
            IInitRepository initRepository
        )
        {
            _mapper = mapper;
            _bCryptHash = bCryptHash;
            _storeRepository = storeRepository;
            _adressRepository = adressRepository;
            _sellerRepository = sellerRepository;
            _cashierRepository = cashierRepository;
            _initRepository = initRepository;
            _passwordAdmin = "$2a$12$unH3SrYEMPn3Sh2tY5UvrO/S6tWLudE8WX5aWUsi2aK4p2n6d56.K";
        }

        public async Task<int> InitData(InitDataDto dto)
        {
            await _storeRepository.CheckStoreNameExists(dto.Name);
            dto.Name = "A";
            dto.AdminStore = true;
            dto.GlobalStore = true;

            dto.SellerName = "A";
            dto.TaxId = "A";

            dto.Street = "A";
            dto.HouseNo = "A";
            dto.City = "A";
            dto.ZipCode = "A";
            dto.Country = "A";

            Adress adress = new Adress();
            adress.Street = dto.Street;
            adress.HouseNo = dto.HouseNo;
            adress.City = dto.City;
            adress.ZipCode = dto.ZipCode;
            adress.Country = dto.Country;

            dto.Username = "Admin";
            dto.Password = _passwordAdmin;
            dto.UserRole = Enum.UserRole.Admin;
            dto.CreatedAt = DateTime.Now;
            dto.Blocked = false;
            dto.Active = true;

            var initData = _mapper.Map<Store>(dto);
            var sellerId = initData.Sellers.Select(s => s.Id).SingleOrDefault();

            await _sellerRepository.CheckSellerTaxIdExists(initData.Id, dto.TaxId, false);
            await _adressRepository.CheckAdressDataExists(adress, initData.Id);
            await _cashierRepository.CheckCashierUsernameExists(initData.Id, sellerId, dto.Username);
            await _storeRepository.AddNewStore(initData);

            return initData.Id;
        }

        public async Task CheckInitDataExists()
        {
            InitDataDto dto = new InitDataDto();
            var initData = await _initRepository.CheckInitDataExists();

            if (initData == null)
                await InitData(dto);
        }

        public async Task UpdateAdminAccount(UpdateAdminAccountDto updateAdminDto)
        {
            InitDataDto initDto = new InitDataDto();
            var adminAccount = await _cashierRepository.GetAdminCashierData();

            if (adminAccount == null)
                await InitData(initDto);
            else
            {
                bool checkPassword = _bCryptHash.VerifyPassword(updateAdminDto.Password, adminAccount.Password);

                if (!checkPassword)
                    throw new NotFoundException("Incorrect administrator password");

                adminAccount.UserRole = Enum.UserRole.Admin;
                adminAccount.Blocked = false;
                adminAccount.Active = true;

                await _cashierRepository.AddNewCashier(adminAccount);
            }
        }
    }
}
