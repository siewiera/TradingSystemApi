using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public abstract class SalesDocument
    {
        public int Id { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public DateTime DateOfSale { get; set; }

        public string? InvoiceNo { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public int CashierId { get; set; }
        public virtual Cashier Cashier { get; set; }

        public virtual InventoryMovement InventoryMovement { get; set; }
        public virtual ICollection<SalesDocumentItem> SalesDocumentItems { get; set; }
    }
}
