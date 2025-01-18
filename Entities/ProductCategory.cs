using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products{ get; set; }
    }
}
