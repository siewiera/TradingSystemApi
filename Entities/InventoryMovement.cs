using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities.Documents;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities
{
    public class InventoryMovement
    {
        public int Id { get; set; }
        [Required]
        public InventoryMovementType InventoryMovementType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        [Required]
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        [Required]
        public int CashierId { get; set; }
        public virtual Cashier Cashier { get; set; }

        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }

        public virtual ICollection<InventoryMovementDetail> InventoryMovementDetails { get; set; }
    }
}
