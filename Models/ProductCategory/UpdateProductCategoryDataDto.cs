using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.ProductCategory
{
    public class UpdateProductCategoryDataDto
    {
        [Required]
        public string Name { get; set; }
    }
}
