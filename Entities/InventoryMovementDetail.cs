using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public class InventoryMovementDetail
    {
        public int Id{ get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int StockAfterMovement { get; set; }

        public int ProductsId { get; set; }
        public virtual Product Products { get; set; }

        public int InventoryMovementId { get; set; }
        public virtual InventoryMovement InventoryMovement { get; set; }

        //public int SalesDocumentItemId { get; set; }
        //public virtual DocumentItem SalesDocumentItem { get; set; }
    }
}
