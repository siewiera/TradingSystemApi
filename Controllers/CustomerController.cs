using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Context;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.Customer;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("customer/adress={adressId}")]
        public async Task<ActionResult> AddCustomerDataWithExistingtAdress([FromBody] AddCustomerDetailsWithExistingtAdressDto dto, [FromRoute] int storeId, [FromRoute] int adressId)
        {
            var customerId = await _customerService.AddCustomerDataWithExistingtAdress(dto, storeId, adressId);

            return Created($"api/tradingSystem/store={storeId}/cutomer{customerId}", null);
        }

        [HttpPost("customer")]
        public async Task<ActionResult> AddCustomerDataWithNewAdress([FromBody] AddCustomerDetailsWithNewAdressDto dto, [FromRoute] int storeId)
        {
            var customerId = await _customerService.AddCustomerDataWithNewAdress(dto, storeId);

            return Created($"api/tradingSystem/store={storeId}/cutomer{customerId}", null);
        }

        [HttpPut("customer={customerId}")]
        public async Task<ActionResult> UpdateCustomerDataById([FromBody] UpdateCustomerDetailsDto dto, [FromRoute] int storeId, [FromRoute] int customerId) 
        {
            await _customerService.UpdateCustomerDataById(dto, storeId, customerId);

            return Ok();
        }

        [HttpDelete("customer={customerId}")]
        public async Task<ActionResult> DeleteCustomerById([FromRoute] int storeId, [FromRoute] int customerId) 
        {
            await _customerService.DeleteCustomerById(storeId, customerId);

            return Ok();
        }

        [HttpGet("customer={customerId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerDataById([FromRoute] int storeId, [FromRoute] int customerId) 
        {
            var customer = await _customerService.GetCustomerDataById(storeId, customerId);

            return Ok(customer);
        }

        [HttpGet("customer/taxId={taxId}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerDataByTaxId([FromRoute] int storeId, [FromRoute] string taxId)
        {
            var customer = await _customerService.GetCustomerDataByTaxId(storeId, taxId);

            return Ok(customer);
        }

        [HttpGet("customer")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomersData([FromRoute] int storeId)
        {
            var customers = await _customerService.GetAllCustomersData(storeId);

            return Ok(customers);
        }
    }
}
