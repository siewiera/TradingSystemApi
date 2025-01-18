namespace TradingSystemApi.Entities
{
    public class SupplyInvoice : SalesDocument
    {
        public virtual ICollection<SupplyInvoiceItem> SupplyInvoiceItems { get; set; }
    }
}
