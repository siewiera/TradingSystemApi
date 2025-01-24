using TradingSystemApi.Models.BarcodeDto;
using TradingSystemApi.Models.InvoiceSale;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IBarcodeService
    {
        Task<int> AddNewBarcode(AddNewBarcodeDto dto, int storeId);
        Task DeleteBarcodeById(int storeId, int barcodeId);
        Task<IEnumerable<BarcodeDto>> GetAllBarcodesData(int storeId);
        Task<BarcodeDto> GetBarcodeDataByCode(int storeId, string code);
        Task<BarcodeDto> GetBarcodeDataById(int storeId, int barcodeId);
        Task UpdateBarcodeDataById(UpdateBarcodeDataDto dto, int storeId, int barcodeId);
    }
}