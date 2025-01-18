using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TradingSystemApi.Context;
using TradingSystemApi.Entities;
using TradingSystemApi.Exceptions;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.AdressDto;
using TradingSystemApi.Models.SellerDto;

namespace TradingSystemApi.Services
{
    public class SellerService : ISellerService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IAdressRepository _adressRepository;

        public SellerService(IMapper mapper, IStoreRepository storeRepository, ISellerRepository sellerRepository, IAdressRepository adressRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _sellerRepository = sellerRepository;
            _adressRepository = adressRepository;
        }


        public async Task<int> AddSellerDataWithNewAdress(AddSellerDetailsWithNewAdressDto dto, int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerTaxIdExists(storeId, dto.TaxId);

            Adress adress = new Adress();
            adress.Street = dto.Street;
            adress.HouseNo = dto.HouseNo;
            adress.City = dto.City;
            adress.ZipCode = dto.ZipCode;
            adress.Country = dto.Country;

            await _adressRepository.CheckAdressDataExists(adress, storeId);

            var seller = _mapper.Map<Seller>(dto);
            seller.StoreId = storeId;
            seller.Adress.StoreId = storeId;

            await _sellerRepository.AddNewSeller(seller);

            return seller.Id;
        }


        public async Task<int> AddSellerDataWithExistingtAdress(AddSellerDetailsWithExistingtAdressDto dto, int storeId, int adressId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _sellerRepository.CheckSellerTaxIdExists(storeId, dto.TaxId);
            await _adressRepository.GetAdressDataById(storeId, adressId);

            var seller = _mapper.Map<Seller>(dto);
            seller.AdressId = adressId;
            seller.StoreId = storeId;

            await _sellerRepository.AddNewSeller(seller);

            return seller.Id;
        }

        public async Task UpdateSellerDataById(UpdateSellerDetailsDto dto, int storeId, int sellerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var seller = await _sellerRepository.GetSellerDataById(storeId, sellerId);
            var adress = await _adressRepository.GetAdressDataById(storeId, dto.AdressId);

            if (seller.TaxId != dto.TaxId)
                await _sellerRepository.CheckSellerTaxIdExists(storeId, dto.TaxId);

            seller.Name = dto.Name;
            seller.TaxId = dto.TaxId;
            seller.AdressId = dto.AdressId;

            await _sellerRepository.UpdateSellerData(seller);
        }

        public async Task DeleteSellerById(int storeId, int sellerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var seller = await _sellerRepository.GetSellerDataById(storeId, sellerId);
            await _sellerRepository.DeleteSeller(seller);
        }

        public async Task<SellerDto> GetSellerDataById(int storeId, int sellerId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var seller = await _sellerRepository.GetSellerDataById(storeId, sellerId);
            var sellerDto = _mapper.Map<SellerDto>(seller);

            return sellerDto;
        }

        public async Task<SellerDto> GetSellerDataByTaxId(int storeId, string taxId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var seller = await _sellerRepository.GetSellerDataByTaxId(storeId, taxId);
            var sellerDto = _mapper.Map<SellerDto>(seller);

            return sellerDto;
        }

        public async Task<IEnumerable<SellerDto>> GetAllSellerData(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var sellers = await _sellerRepository.GetAllSellerData(storeId);
            var sellersDto = _mapper.Map<IEnumerable<SellerDto>>(sellers);

            return sellersDto;
        }
    }
}
