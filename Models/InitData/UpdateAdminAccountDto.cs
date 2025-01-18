using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Enum;

namespace TradingSystemApi.Models.InitData
{
    public class UpdateAdminAccountDto
    {
        //cashier
        //public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //public UserRole UserRole { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public bool Blocked { get; set; }
        //[Required]
        //public bool Active { get; set; }
    }
}
