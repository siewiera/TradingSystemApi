namespace TradingSystemApi.Entities
{
    public class SupplyInvoiceItem : SalesDocumentItem
    {
        public int SupplyInvoiceId { get; set; }
        public virtual SupplyInvoice SupplyInvoice  { get; set; }
    }
}
