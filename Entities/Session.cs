using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Entities
{
    public class Session
    {
        public int Id { get; set; }
        [Required]
        public Guid SessionGuid { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }


        public int CashierId { get; set; }
        public virtual Cashier Cashier { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
