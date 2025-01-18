using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.SellerDto
{
    public class UpdateSellerDetailsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }

        public int AdressId { get; set; }
    }
}
