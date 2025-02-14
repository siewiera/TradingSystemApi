using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.Product
{
    public class AddNewProductDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public JM JM { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }
        [Required]
        public int Vat { get; set; }
        public decimal ProductMargin { get; set; } = 3;
        public bool PercentageMargin { get; set; } = true;
        [Required]
        public int Quantity { get; set; }
        public int StoreId { get; set; }

        public int ProductCategoryId { get; set; }

        //barcode
        //public ICollection<Barcode> Barcodes { get; set; }
        //public string Code { get; set; }
        //public bool Active { get; set; }
        //public DateTime BarcodeCreationDate { get; set; }
        //public DateTime BarcodeUpdateDate { get; set; }
    }
}
