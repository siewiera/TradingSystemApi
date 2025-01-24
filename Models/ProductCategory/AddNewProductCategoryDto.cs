using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.ProductCategory
{
    public class AddNewProductCategoryDto
    {
        [Required]
        public string Name { get; set; }

        public int StoreId { get; set; }
    }
}
