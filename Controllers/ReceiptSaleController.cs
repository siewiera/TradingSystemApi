using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.ReceiptSale;

namespace TradingSystemApi.Controllers
{

    [Route("api/tradingSystem/store={storeId}/seller={sellerId}/cashier={cashierId}")]
    [Controller]
    public class ReceiptSaleController : ControllerBase
    {
        private readonly IReceiptSaleService _receiptSaleService;

        public ReceiptSaleController(IReceiptSaleService receiptSaleService)
        {
            _receiptSaleService = receiptSaleService;
        }

        [HttpPost("receipt")]
        public async Task<ActionResult<int>> AddNewReceiptSale([FromBody] AddNewReceiptSaleDto dto, [FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId)
        {
            var receiptId = await _receiptSaleService.AddNewReceiptSale(dto, storeId, sellerId, cashierId);
            return Created($"api/tradingSystem/store={storeId}/seller={sellerId}/cashier={cashierId}/receipt={receiptId}", null);
        }
    }
}
