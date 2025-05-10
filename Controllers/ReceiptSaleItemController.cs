using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.ReceiptSale;
using TradingSystemApi.Models.ReceiptSaleItem;
using TradingSystemApi.Services;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}/seller={sellerId}/cashier={cashierId}/receipt={receiptSaleId}")]
    [Controller]
    public class ReceiptSaleItemController : ControllerBase
    {
        private readonly IReceiptSaleItemService _receiptSaleItemService;

        public ReceiptSaleItemController(IReceiptSaleItemService receiptSaleItemService)
        {
            _receiptSaleItemService = receiptSaleItemService;
        }

        [HttpPost("item")]
        public async Task<ActionResult> AddNewReceiptSale
            (
                [FromBody] List<AddNewReceiptSaleItemDto> dtos, 
                [FromRoute] int storeId, 
                [FromRoute] int sellerId, 
                [FromRoute] int cashierId, 
                [FromRoute] int receiptSaleId
            )
        {
            await _receiptSaleItemService.AddNewItemToReceiptSale(dtos, storeId, receiptSaleId);
            return Created($"api/tradingSystem/store={storeId}/seller={sellerId}/cashier={cashierId}/receipt={receiptSaleId}", null);
        }

        [HttpPut("item={receiptSaleItemId}")]
        public async Task<ActionResult> AddNewReceiptSale
            (
                [FromBody] UpdateReceiptSaleItemDto dto,
                [FromRoute] int storeId,
                [FromRoute] int sellerId,
                [FromRoute] int cashierId,
                [FromRoute] int receiptSaleId,
                [FromRoute] int receiptSaleItemId
            )
        {
            await _receiptSaleItemService.UpdateReceiptSaleItemDataById(dto, storeId, receiptSaleId, receiptSaleItemId);
            return Ok();
        }
    }
}
