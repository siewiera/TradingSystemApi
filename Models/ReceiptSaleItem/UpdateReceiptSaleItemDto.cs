using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.ReceiptSaleItem
{
    public class UpdateReceiptSaleItemDto
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }

        public int? ProductId { get; set; }

        //public int SalesDocumentId { get; set; }
        //public virtual SalesDocument SalesDocument { get; set; }
        //public virtual InventoryMovementDetail InventoryMovementDetail { get; set; }
    }
}
