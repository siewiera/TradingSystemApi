using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.ReceiptSaleItem
{
    public class AddNewReceiptSaleItemDto
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }

        public int ProductId { get; set; }
    }
}
