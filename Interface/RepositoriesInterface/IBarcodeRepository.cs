using TradingSystemApi.Entities;

namespace TradingSystemApi.Interface.RepositoriesInterface
{
    public interface IBarcodeRepository
    {
        Task AddNewBarcode(Barcode barcode);
        Task CheckBarcodeExists(int storeId, string code);
        Task DeleteBarcode(Barcode barcode);
        Task<IEnumerable<Barcode>> GetAllBarcodesData(int storeId);
        Task<Barcode> GetBarcodeDataByCode(int storeId, string code);
        Task<Barcode> GetBarcodeDataById(int storeId, int barcodeId);
        Task UpdateBarcodeData(Barcode barcode);
    }
}