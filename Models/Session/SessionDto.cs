using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.Session
{
    public class SessionDto
    {
        public int Id { get; set; }
        [Required]
        public Guid SessionGuid { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        public DateTime LastAction { get; set; }
        public string Ip { get; set; }

        public int CashierId { get; set; }

        public int StoreId { get; set; }
    }
}
