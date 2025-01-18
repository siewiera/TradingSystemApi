using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Models;

namespace TradingSystemApi.Entities
{
    public class InvoiceSale : SalesDocument
    {
        public virtual ICollection<InvoiceSaleItem> InvoiceSaleItems { get; set; }
    }
}
