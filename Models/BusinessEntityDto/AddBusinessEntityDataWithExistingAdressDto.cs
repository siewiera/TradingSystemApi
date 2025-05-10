using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.BusinessEntityDto
{
    public class AddBusinessEntityDataWithExistingAdressDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }
    }
}
