using TradingSystemApi.Entities.Documents;

namespace TradingSystemApi.Entities.Documents.Invoice
{
    public abstract class InvoiceDocument : Document
    {
        public string InvoiceNumber { get; set; }
        public DateTime DueDate { get; set; }
    }
}
