using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.SellerDto
{
    public class SellerDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TaxId { get; set; }


        //Adress
        public int AdressId { get; set; }
        [Required]
        public string Street { get; set; }
        public string HouseNo { get; set; }
        [Required]
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        //public virtual ICollection<Cashier> Cashiers { get; set; }

    }
}
