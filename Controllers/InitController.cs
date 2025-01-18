using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.InitData;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/init")]
    [ApiController]
    public class InitController : ControllerBase
    {
        private readonly IInitService _initService;

        public InitController(IInitService initService)
        {
            _initService = initService;
        }

        [HttpPost]
        public async Task<ActionResult> CheckInitDataExists()
        {
            await _initService.CheckInitDataExists();
            return Created($"api/tradingSystem/init", null);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAdminAccount([FromBody] UpdateAdminAccountDto dto) 
        {
            await _initService.UpdateAdminAccount(dto);
            return Ok();
        }
    }
}
