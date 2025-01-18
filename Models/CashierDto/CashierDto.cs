using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.CashierDto
{
    public class CashierDto
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Blocked { get; set; }
        [Required]
        public bool Active { get; set; }

        //seller
        public int SellerId { get; set; }
        //[Required]
        //public string Name { get; set; }
        //[Required]
        //public string TaxId { get; set; }

        //public int StoreId { get; set; }
        //public virtual Store Store { get; set; }

        //public int AdressId { get; set; }
        //public virtual Adress Adress { get; set; }

        //public virtual ICollection<Cashier> Cashiers { get; set; }

        //public virtual Session Session { get; set; }
        //public virtual ICollection<SalesDocument> SalesDocuments { get; set; }
        //public virtual ICollection<InventoryMovement> InventoryMovements { get; set; }
    }
}
