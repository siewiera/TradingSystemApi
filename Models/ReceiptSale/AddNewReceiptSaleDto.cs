using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Models.ReceiptSaleItem;

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

        //Store
        public int StoreId { get; set; }

        //Cashier
        public int CashierId { get; set; }

        //ReceiptSaleItem
        public ICollection<AddNewReceiptSaleItemDto> AddNewReceiptSaleItemDtos { get; set; }
    }
}
