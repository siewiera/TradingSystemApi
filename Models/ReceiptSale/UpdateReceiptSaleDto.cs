using System.ComponentModel.DataAnnotations;
using TradingSystemApi.Entities;
using TradingSystemApi.Models.ReceiptSaleItem;

namespace TradingSystemApi.Models.ReceiptSale
{
    public class UpdateReceiptSaleDto
    {
        [Required]
        public DateTime DateOfIssue { get; set; }
        //public UpdateReceiptSaleItemDto UpdateReceiptSaleItemDto{ get; set; }

        //public ICollection<UpdateReceiptSaleItemDto> UpdateReceiptSaleItemDtos { get; set; }
    }
}
