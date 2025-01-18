using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities
{
    public abstract class SalesDocumentItem
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int SalesDocumentId { get; set; }
        public virtual SalesDocument SalesDocument { get; set; }
        public virtual InventoryMovementDetail InventoryMovementDetail { get; set; }
    }
}
