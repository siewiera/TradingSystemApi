using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Entities
{
    public class Cashier
    {
        public int Id  { get; set; }
        [Required]
        public string Username  { get; set; }
        [Required]
        public string Password  { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Blocked { get; set; }
        [Required]
        public bool Active { get; set; }

        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }

        public virtual Session Session { get; set; }
        public virtual ICollection<SalesDocument> SalesDocuments { get; set; }
        public virtual ICollection<InventoryMovement> InventoryMovements { get; set; }

    }
}
