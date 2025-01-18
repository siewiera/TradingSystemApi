using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities
{
    public class InvoiceSaleItem : SalesDocumentItem
    {
        public int InvoiceSaleId { get; set; }
        public virtual InvoiceSale InvoiceSale { get; set; }
    }
}
