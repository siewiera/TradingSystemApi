using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.ReceiptSale
{
    public class ReceiptSaleDto
    {
        public int Id { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public DateTime DateOfSale { get; set; }

        public string? InvoiceNo { get; set; }

        public int? CustomerId { get; set; }

        public int StoreId { get; set; }

        public int CashierId { get; set; }

        public ICollection<ReceiptSaleItem> ReceiptSaleItems { get; set; }

    }
}
