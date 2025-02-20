using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.Session;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}/seller={sellerId}")]
    [Controller]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public string GetClientIPAddress()
        {
            var forwardedFor = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrEmpty(forwardedFor))
                ip = forwardedFor.Split(',').First().Trim();

            return ip;
        }

        /**/

        [HttpPost("cashier={cashierId}/session")]
        public async Task<ActionResult> AddNewSession([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId)
        {
            var ip = GetClientIPAddress();
            var sessionId = await _sessionService.AddNewSession(storeId, sellerId, cashierId, ip);
            return Created($"api/tradingSystem/store={storeId}/seller={sellerId}/cashier={cashierId}/seller={sellerId}", null);
        }

        [HttpPut("cashier={cashierId}/session")]

        public async Task<ActionResult> ExtensionSessionByCashierId([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId) 
        {
            await _sessionService.ExtensionSessionByCashierId(storeId, sellerId, cashierId);
            return Ok();
        }

        [HttpPut("sessionGuid={sessionGuid}")]
        public async Task<ActionResult> ExtensionSessionBySessionGuid([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] Guid sessionGuid)
        {
            await _sessionService.ExtensionSessionBySessionGuid(storeId, sellerId, sessionGuid);
            return Ok();
        }

        [HttpDelete("cashier={cashierId}/session")]
        public async Task<ActionResult> DeleteSessionByCashierId([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId) 
        {
            await _sessionService.DeleteSessionByCashierid(storeId, sellerId, cashierId);
            return NoContent();
        }

        [HttpGet("cashier={cashierId}/session")]
        public async Task<ActionResult<SessionDto>> GetSessionDataByCashierId([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] int cashierId) 
        {
            var session = await _sessionService.GetSessionDataByCashierId(storeId, sellerId, cashierId);
            return Ok(session);
        }

        [HttpGet("sessionGuid={sessionGuid}")]
        public async Task<ActionResult<SessionDto>> GetSessionDataBySessionGuid([FromRoute] int storeId, [FromRoute] int sellerId, [FromRoute] Guid sessionGuid) 
        {   
            var session = await _sessionService.GetSessionDataBySessionGuid(storeId, sellerId, sessionGuid);
            return Ok(session);
        }

        [HttpGet("session")]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetAllSessionsData([FromRoute] int storeId, [FromRoute] int sellerId)
        {
            var sessions = await _sessionService.GetAllSessionsData(storeId, sellerId);
            return Ok(sessions);
        }
    }
}
