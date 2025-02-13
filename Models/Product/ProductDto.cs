using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.Product
{
    public class ProductDto
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

        //ProductCategory
        public int ProductCategoryId { get; set; }
        [Required]
        public string ProductCategoryName { get; set; }

        //Barcode
        public ICollection<BarcodeDto.BarcodeDto> BarcodeDtos { get; set; }
        //public string Code { get; set; }
        //public bool Active { get; set; }
        //public DateTime BarcodeCreationDate { get; set; }
        //public DateTime BarcodeUpdateDate { get; set; }

    }
}
