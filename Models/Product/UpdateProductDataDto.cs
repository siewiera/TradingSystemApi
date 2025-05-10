using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.Product
{
    public class UpdateProductDataDto
    {
        public string? Name { get; set; }
        public JM? JM { get; set; }
        public decimal? CostNetPrice { get; set; }
        public int? Vat { get; set; }
        public decimal? ProductMargin { get; set; }
        public bool? PercentageMargin { get; set; } = true;
        public decimal? SellingPrice { get; set; }
        public decimal? Quantity { get; set; }

        public int? ProductCategoryId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
