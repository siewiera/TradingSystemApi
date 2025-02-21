using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;

namespace TradingSystemApi.Models.ReceiptSale
{
    public class AddNewReceiptSaleDto
    {
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public DateTime DateOfIssue { get; set; }
        [Required]
        public DateTime DateOfSale { get; set; }

        public string? InvoiceNo { get; set; }

        //Customer
        public int? CustomerId { get; set; }

        //Store
        public int StoreId { get; set; }

        //Cashier
        public int CashierId { get; set; }


        //ReceiptSaleItem
        //public ICollection<ReceiptSaleItem> ReceiptSaleItems { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal CostNetPrice { get; set; }

        public int ProductId { get; set; }
    }
}
