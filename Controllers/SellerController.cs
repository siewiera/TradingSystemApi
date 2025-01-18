using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.SellerDto;

namespace TradingSystemApi.Controllers
{

    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }


        [HttpPost("seller")]
        public async Task<ActionResult> AddSellerDataWithNewAdress([FromBody] AddSellerDetailsWithNewAdressDto dto, [FromRoute] int storeId) 
        {
            var seller = await _sellerService.AddSellerDataWithNewAdress(dto, storeId);

            return Created($"api/tradingSystem/store={storeId}/seller{seller}", null);
        }


        [HttpPost("seller/adress={adressId}")]
        public async Task<ActionResult> AddSellerDataWithExistingtAdress([FromBody] AddSellerDetailsWithExistingtAdressDto dto, [FromRoute] int storeId, [FromRoute] int adressId)
        {
            var seller = await _sellerService.AddSellerDataWithExistingtAdress(dto, storeId, adressId);

            return Created($"api/tradingSystem/store={storeId}/seller{seller}", null);
        }


        [HttpPut("seller={sellerId}")]
        public async Task<ActionResult> UpdateSellerDataById([FromBody] UpdateSellerDetailsDto dto, [FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            await _sellerService.UpdateSellerDataById(dto, storeId, sellerId);

            return Ok();
        }

        [HttpDelete("seller={sellerId}")]
        public async Task<ActionResult> DeleteSellerById([FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            await _sellerService.DeleteSellerById(storeId, sellerId);

            return NoContent();
        }

        [HttpGet("seller={sellerId}")]
        public async Task<ActionResult<SellerDto>> GetSellerDataById([FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            var seller = await _sellerService.GetSellerDataById(storeId, sellerId);

            return Ok(seller);
        }

        [HttpGet("seller/taxId={taxId}")]
        public async Task<ActionResult<SellerDto>> GetSellerDataByTaxId([FromRoute] int storeId, [FromRoute] string taxId)
        {
            var seller = await _sellerService.GetSellerDataByTaxId(storeId, taxId);

            return Ok(seller);
        }


        [HttpGet("seller")]
        public async Task<ActionResult<IEnumerable<SellerDto>>> GetAllSellerData([FromRoute] int storeId) 
        {
            var sellers = await _sellerService.GetAllSellerData(storeId);

            return Ok(sellers);
        }
    }
}
