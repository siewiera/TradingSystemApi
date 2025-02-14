using AutoMapper;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.RepositoriesInterface;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.BarcodeDto;
using TradingSystemApi.Models.InvoiceSale;
using TradingSystemApi.Models.StoreDto;

namespace TradingSystemApi.Services
{
    public class BarcodeService : IBarcodeService
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBarcodeRepository _barcodeRepository;

        public BarcodeService(IMapper mapper, IStoreRepository storeRepository, IProductRepository productRepository, IBarcodeRepository barcodeRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _barcodeRepository = barcodeRepository;
        }

        public async Task<int> AddNewBarcode(AddNewBarcodeDto dto, int storeId, int productId)
        {
            await _storeRepository.CheckStoreById(storeId);
            await _productRepository.GetProductDataById(storeId, productId);

            var barcode = _mapper.Map<Barcode>(dto);
            //barcode.Active = true;
            barcode.CreationDate = DateTime.Now;
            barcode.UpdateDate = DateTime.Now;
            barcode.StoreId = storeId;
            barcode.ProductId = productId;

            await _barcodeRepository.CheckBarcodeExists(storeId, dto.Code);
            await _barcodeRepository.AddNewBarcode(barcode);

            return barcode.Id;
        }

        public async Task UpdateBarcodeDataById(UpdateBarcodeDataDto dto, int storeId, int barcodeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var barcode = await _barcodeRepository.GetBarcodeDataById(storeId, barcodeId);
            
            if(barcode.Code != dto.Code)
                await _barcodeRepository.CheckBarcodeExists(storeId, dto.Code);

            barcode.Code = dto.Code;
            barcode.Active = dto.Active;
            barcode.UpdateDate = DateTime.Now;
            await _barcodeRepository.UpdateBarcodeData(barcode);
        }

        public async Task DeleteBarcodeById(int storeId, int barcodeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var barcode = await _barcodeRepository.GetBarcodeDataById(storeId, barcodeId);
            await _barcodeRepository.DeleteBarcode(barcode);
        }

        public async Task<BarcodeDto> GetBarcodeDataById(int storeId, int barcodeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var barcode = await _barcodeRepository.GetBarcodeDataById(storeId, barcodeId);
            var barcodeDto = _mapper.Map<BarcodeDto>(barcode);
            return barcodeDto;
        }

        public async Task<BarcodeDto> GetBarcodeDataByCode(int storeId, string code)
        {
            await _storeRepository.CheckStoreById(storeId);
            var barcode = await _barcodeRepository.GetBarcodeDataByCode(storeId, code);
            var barcodeDto = _mapper.Map<BarcodeDto>(barcode);
            return barcodeDto;
        }

        public async Task<IEnumerable<BarcodeDto>> GetAllBarcodesData(int storeId)
        {
            await _storeRepository.CheckStoreById(storeId);
            var barcodes = await _barcodeRepository.GetAllBarcodesData(storeId);
            var barcodesDto = _mapper.Map<IEnumerable<BarcodeDto>>(barcodes);
            return barcodesDto;
        }
    }
}
