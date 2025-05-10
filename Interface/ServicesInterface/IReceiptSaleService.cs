using TradingSystemApi.Models.ReceiptSale;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IReceiptSaleService
    {
        Task<int> AddNewReceiptSale(AddNewReceiptSaleDto dto, int storeId, int sellerId, int cashierId);
        Task DeleteReceiptSaleById(int storeId, int sellerId, int cashierId, int receiptSaleId);
        Task<IEnumerable<ReceiptSaleDto>> GetAllReceiptsSale(int storeId);
        Task<IEnumerable<ReceiptSaleDto>> GetAllReceiptsSaleByCashierId(int storeId, int sellerId, int cashierId);
        Task<ReceiptSaleDto> GetReceiptSaleById(int storeId, int sellerId, int cashierId, int receiptSaleId);
        Task<ReceiptSaleDto> GetReceiptSaleByInvoiceNumber(int storeId, int sellerId, int cashierId, string invoiceNo);
        Task UpdateReceiptSaleDataById(UpdateReceiptSaleDto dto, int storeId, int sellerId, int cashierId, int receiptSaleId, int receiptSaleItemId = -1);
        Task ValidData(int storeId, int sellerId, int cashierId);
    }
}