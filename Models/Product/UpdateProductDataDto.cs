using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.Product
{
    public class UpdateProductDataDto
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
        public decimal SellingPrice { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int ProductCategoryId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
