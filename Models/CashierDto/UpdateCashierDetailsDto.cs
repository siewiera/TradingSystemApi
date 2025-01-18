using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.CashierDto
{
    public class UpdateCashierDetailsDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Blocked { get; set; }
        public int SellerId { get; set; }
    }
}
