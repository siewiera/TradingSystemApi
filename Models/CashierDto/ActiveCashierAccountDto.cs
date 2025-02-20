using System.ComponentModel.DataAnnotations;

namespace TradingSystemApi.Models.CashierDto
{
    public class ActiveCashierAccountDto
    {
        [Required]
        public bool Active { get; set; }
    }
}
