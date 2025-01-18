using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.StoreDto;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("store")]
        public async Task<ActionResult> AddNewStore([FromBody]AddNewStoreDto dto) 
        {
            var store = await _storeService.AddNewStore(dto);
            return Created($"api/tradingSystem/store={store}", null);
        }

        [HttpPut("store={storeId}")]
        public async Task<ActionResult> UpdateStoreData([FromBody]AddNewStoreDto dto, [FromRoute] int storeId) 
        {
            await _storeService.UpdateStoreDataById(dto, storeId);
            return Ok();
        }

        [HttpDelete("store={storeId}")]
        public async Task<ActionResult> DeleteStoreById([FromRoute] int storeId) 
        {
            await _storeService.DeleteStoreById(storeId);
            return NoContent();
        }

        [HttpGet("store={storeId}")]
        public async Task<ActionResult<StoreDto>> GetStoreDataById([FromRoute] int storeId) 
        {
            var store = await _storeService.GetStoreDataById(storeId);
            return Ok(store);
        }

        [HttpGet("store")]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetAllStoresData() 
        {
            var stories = await _storeService.GetAllStoresData();
            return Ok(stories);
        }
    }
}
