using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.Customer
{
    public class AddCustomerDetailsWithExistingtAdressDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }
    }
}
