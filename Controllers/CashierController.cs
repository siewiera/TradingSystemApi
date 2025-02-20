using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models;
using TradingSystemApi.Models.CashierDto;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}/seller={sellerId}")]
    [Controller]
    public class CashierController : ControllerBase
    {
        private readonly ICashierService _cashierService;

        public CashierController(ICashierService cashierService)
        {
            _cashierService = cashierService;
        }

        [HttpPost("cashier")]
        public async Task<ActionResult> AddNewCashier([FromBody] AddingNewCashierDto dto, [FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            var cashierId = await _cashierService.AddNewCashier(dto, storeId, sellerId);

            return Created($"api/tradingSystem/store={sellerId}/cashier={cashierId}", null);
        }

        [HttpPut("cashier={cashierId}")]
        public async Task<ActionResult> UpdateCashierDataById([FromBody] UpdateCashierDetailsDto dto, [FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId) 
        {
            await _cashierService.UpdateCashierDataById(dto, storeId, sellerId, cashierId);

            return Ok();
        }

        //[HttpPut("cashier={cashierId}")]
        //public async Task<ActionResult> ActiveCashierAccount([FromBody] ActiveCashierAccountDto dto, [FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId)
        //{
        //    await _cashierService.ActiveCashierAccount(dto, storeId, sellerId, cashierId);

        //    return Ok();
        //}

        [HttpDelete("cashier={cashierId}")]
        public async Task<ActionResult> DeleteCashierById([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId) 
        {
            await _cashierService.DeleteCashierById(storeId, sellerId, cashierId);

            return NoContent();
        }

        [HttpGet("cashier={cashierId}")]
        public async Task<ActionResult<CashierDto>> GetCashierDataById([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId) 
        {
            var cashier = await _cashierService.GetCashierDataById(storeId, sellerId, cashierId);

            return Ok(cashier);
        }

        [HttpGet("cashier")]
        public async Task<ActionResult<IEnumerable<CashierDto>>> GetAllCashierData([FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            var cashiers = await _cashierService.GetAllCashierData(storeId, sellerId);

            return Ok(cashiers);
        }
    }
}
