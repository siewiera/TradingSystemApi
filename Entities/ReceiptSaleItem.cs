using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public class ReceiptSaleItem : SalesDocumentItem
    {
        public int ReceiptSaleId { get; set; }
        public virtual ReceiptSale ReceiptSale { get; set; }
    }
}
