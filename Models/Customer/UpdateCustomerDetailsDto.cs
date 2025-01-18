using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.Customer
{
    public class UpdateCustomerDetailsDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }

        public int AdressId { get; set; }
    }
}
