using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.BusinessEntityDto
{
    public class UpdateBusinessEntityDataDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }

        public int AdressId { get; set; }
    }
}
