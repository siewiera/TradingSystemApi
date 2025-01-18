

namespace TradingSystemApi.Entities
{
    public class ReceiptSale : SalesDocument
    {
        public virtual ICollection<ReceiptSaleItem> ReceiptSaleItems { get; set; }
    }
}
