using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities.Documents
{
    public abstract class DocumentItem
    {
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }
        public bool IsReturn { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
        //public virtual InventoryMovementDetail InventoryMovementDetail { get; set; }
    }
}
