using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.Session
{
    public class AddNewSessionDto
    {
        [Required]
        public Guid SessionGuid { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }

        public int CashierId { get; set; }

        public int StoreId { get; set; }
    }
}
