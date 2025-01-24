using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.ProductCategory
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int StoreId { get; set; }
    }
}
