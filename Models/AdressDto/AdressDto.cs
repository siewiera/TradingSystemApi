using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.AdressDto
{
    public class AdressDto
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string HouseNo { get; set; }
        [Required]
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public int StoreId { get; set; }
    }
}
