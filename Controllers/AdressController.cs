using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.AdressDto;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        private readonly IAdressService _adressService;

        public AdressController(IAdressService adressService)
        {
            _adressService = adressService;
        }

        [HttpPost("adress")]
        public async Task<ActionResult> AddingNewAdress([FromBody] AddingNewAdressDto dto, [FromRoute] int storeId)
        {
            var adressId = await _adressService.AddingNewAdress(dto, storeId);
            return Created($"api/tradingSystem/store={storeId}/adress={adressId}", null);
        }

        [HttpPut("adress={adressId}")]
        public async Task<ActionResult> UpdateAdressDataById([FromBody] UpdateAdressDto dto, [FromRoute] int storeId, [FromRoute] int adressId)  
        {
            await _adressService.UpdateAdressDataById(dto, storeId, adressId);
            return Ok();
        }

        [HttpDelete("adress={adressId}")]
        public async Task<ActionResult> DeleteAdressById([FromRoute] int storeId, [FromRoute] int adressId)
        {
            await _adressService.DeleteAdressById(storeId, adressId);
            return NoContent();
        }

        [HttpGet("adress")]
        public async Task<ActionResult<IEnumerable<AdressDto>>> GetAllAdressesData([FromRoute] int storeId) 
        {
            var adresses = await _adressService.GetAllAdressesData(storeId);
            return Ok(adresses);
        }

        [HttpGet("adress={adressId}")]
        public async Task<ActionResult<AdressDto>> GetAdressDataById([FromRoute] int storeId, [FromRoute] int adressId)
        {
            var adresses = await _adressService.GetAdressDataById(storeId, adressId);
            return Ok(adresses);
        }
    }
}
