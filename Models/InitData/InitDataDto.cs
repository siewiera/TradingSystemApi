using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.InitData
{
    public class InitDataDto
    {
        //store
        [Required]
        public string Name { get; set; }
        public bool AdminStore { get; set; }
        public bool GlobalStore { get; set; }

        //seller
        public string SellerName { get; set; }
        [Required]
        public string TaxId { get; set; }

        //adress
        [Required]
        public string Street { get; set; }
        public string HouseNo { get; set; }
        [Required]
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        //cashier
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Blocked { get; set; }
        [Required]
        public bool Active { get; set; }


        //public virtual ICollection<Product> Products { get; set; }
        //public virtual ICollection<Customer> Customers { get; set; }
        //public virtual ICollection<SalesDocument> SalesDocuments { get; set; }
        //public virtual ICollection<InventoryMovement> InventoryMovements { get; set; }
    }
}
