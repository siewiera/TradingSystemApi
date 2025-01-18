using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.SellerDto
{
    public class AddSellerDetailsWithExistingtAdressDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }
    }
}
