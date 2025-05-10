using TradingSystemApi.Entities.Documents.Receipts;
using TradingSystemApi.Models.ReceiptSaleItem;

namespace TradingSystemApi.Interface.ServicesInterface
{
    public interface IReceiptSaleItemService
    {
        Task AddNewItemToReceiptSale(List<AddNewReceiptSaleItemDto> dtos, int storeId, int receiptSaleId);
        Task<List<ReceiptSaleItem>> GroupinOfReceiptProductItems(List<ReceiptSaleItem> dtos);
        Task UpdateReceiptSaleItemDataById(UpdateReceiptSaleItemDto dto, int storeId, int receiptSaleId, int receiptSaleItemId);
    }
}