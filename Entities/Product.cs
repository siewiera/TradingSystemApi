using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public JM JM { get; set; }
        [Required]
        public int ProductCode { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }
        [Required]
        public int Vat { get; set; }
        public decimal ProductMargin { get; set; } = 3;
        public bool PercentageMargin { get; set; } = true;
        [Required]
        public decimal SellingPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public virtual ICollection<InventoryMovementDetail> InventoryMovementDetails { get; set; }
        public virtual ICollection<SalesDocumentItem> SalesDocumentItems { get; set; }
        public virtual ICollection<Barcode> Barcodes { get; set; }
    }
}
