using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Context;
using TradingSystemApi.Entities.BusinessEntities.Customer;
using TradingSystemApi.Entities.BusinessEntities.Seller;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.BusinessEntityDto;
using TradingSystemApi.Models.Customer;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IBusinessEntityService<Customer> _businessEntityService;

        public CustomerController(IBusinessEntityService<Customer> businessEntityService)
        {
            _businessEntityService = businessEntityService;
        }


        [HttpPost("customer/adress={adressId}")]
        public async Task<ActionResult> AddCustomerDataWithExistingtAdress([FromBody] AddBusinessEntityDataWithExistingAdressDto dto, [FromRoute] int storeId, [FromRoute] int adressId)
        {
            var customerId = await _businessEntityService.AddNewBusinessEntityWithExistingAdress(dto, storeId, adressId);
            //var customerId = await _customerService.AddCustomerDataWithExistingtAdress(dto, storeId, adressId);

            return Created($"api/tradingSystem/store={storeId}/cutomer{customerId}", null);
        }

        [HttpPost("customer")]
        public async Task<ActionResult> AddCustomerDataWithNewAdress([FromBody] AddBusinessEntityDetailsWithNewAdressDto dto, [FromRoute] int storeId)
        {
            var customerId = await _businessEntityService.AddNewBusinessEntityWithNewAdress(dto, storeId);
            //var customerId = await _customerService.AddCustomerDataWithNewAdress(dto, storeId);

            return Created($"api/tradingSystem/store={storeId}/cutomer{customerId}", null);
        }

        [HttpPut("customer={customerId}")]
        public async Task<ActionResult> UpdateCustomerDataById([FromBody] UpdateBusinessEntityDataDto dto, [FromRoute] int storeId, [FromRoute] int customerId) 
        {
            await _businessEntityService.UpdateBusinessEntityDataById(dto, storeId, customerId);
            //await _customerService.UpdateCustomerDataById(dto, storeId, customerId);

            return Ok();
        }

        [HttpDelete("customer={customerId}")]
        public async Task<ActionResult> DeleteCustomerById([FromRoute] int storeId, [FromRoute] int customerId) 
        {
            await _businessEntityService.DeleteBusinessEntityById(storeId, customerId);
            //await _customerService.DeleteCustomerById(storeId, customerId);

            return Ok();
        }

        [HttpGet("customer={customerId}")]
        public async Task<ActionResult<BusinessEntityDto>> GetCustomerDataById([FromRoute] int storeId, [FromRoute] int customerId) 
        {
            var customer = await _businessEntityService.GetBusinessEntityDataById(storeId, customerId);
            //var customer = await _customerService.GetCustomerDataById(storeId, customerId);

            return Ok(customer);
        }

        [HttpGet("customer/taxId={taxId}")]
        public async Task<ActionResult<BusinessEntityDto>> GetCustomerDataByTaxId([FromRoute] int storeId, [FromRoute] string taxId)
        {
            var customer = await _businessEntityService.GetBusinessEntityDataByTaxId(storeId, taxId);
            //var customer = await _customerService.GetCustomerDataByTaxId(storeId, taxId);

            return Ok(customer);
        }

        [HttpGet("customer")]
        public async Task<ActionResult<IEnumerable<BusinessEntityDto>>> GetAllCustomersData([FromRoute] int storeId)
        {
            var customers = await _businessEntityService.GetAllBusinessEntities(storeId);
            //var customers = await _customerService.GetAllCustomersData(storeId);

            return Ok(customers);
        }
    }
}
