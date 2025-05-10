using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Entities.BusinessEntities.Seller;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.BusinessEntityDto;
using TradingSystemApi.Models.SellerDto;

namespace TradingSystemApi.Controllers
{

    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IBusinessEntityService<Seller> _businessEntityService;

        public SellerController(IBusinessEntityService<Seller> businessEntityService)
        {
            _businessEntityService = businessEntityService;
        }


        [HttpPost("seller")]
        public async Task<ActionResult> AddSellerDataWithNewAdress([FromBody] AddBusinessEntityDetailsWithNewAdressDto dto, [FromRoute] int storeId) 
        {
            var seller = await _businessEntityService.AddNewBusinessEntityWithNewAdress(dto, storeId);
            //var seller = await _sellerService.AddSellerDataWithNewAdress(dto, storeId);

            return Created($"api/tradingSystem/store={storeId}/seller{seller}", null);
        }


        [HttpPost("seller/adress={adressId}")]
        public async Task<ActionResult> AddSellerDataWithExistingAdress([FromBody] AddBusinessEntityDataWithExistingAdressDto dto, [FromRoute] int storeId, [FromRoute] int adressId)
        {
            var seller = await _businessEntityService.AddNewBusinessEntityWithExistingAdress(dto, storeId, adressId);
            //var seller = await _sellerService.AddSellerDataWithExistingtAdress(dto, storeId, adressId);

            return Created($"api/tradingSystem/store={storeId}/seller{seller}", null);
        }


        [HttpPut("seller={sellerId}")]
        public async Task<ActionResult> UpdateSellerDataById([FromBody] UpdateBusinessEntityDataDto dto, [FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            await _businessEntityService.UpdateBusinessEntityDataById(dto, storeId, sellerId);
            //await _sellerService.UpdateSellerDataById(dto, storeId, sellerId);

            return Ok();
        }

        [HttpDelete("seller={sellerId}")]
        public async Task<ActionResult> DeleteSellerById([FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            await _businessEntityService.DeleteBusinessEntityById(storeId, sellerId);
            //await _sellerService.DeleteSellerById(storeId, sellerId);

            return NoContent();
        }

        [HttpGet("seller={sellerId}")]
        public async Task<ActionResult<BusinessEntityDto>> GetSellerDataById([FromRoute] int storeId, [FromRoute] int sellerId) 
        {
            var seller = await _businessEntityService.GetBusinessEntityDataById(storeId, sellerId);
            //var seller = await _sellerService.GetSellerDataById(storeId, sellerId);

            return Ok(seller);
        }

        [HttpGet("seller/taxId={taxId}")]
        public async Task<ActionResult<BusinessEntityDto>> GetSellerDataByTaxId([FromRoute] int storeId, [FromRoute] string taxId)
        {
            var seller = await _businessEntityService.GetBusinessEntityDataByTaxId(storeId, taxId);
            //var seller = await _sellerService.GetSellerDataByTaxId(storeId, taxId);

            return Ok(seller);
        }


        [HttpGet("seller")]
        public async Task<ActionResult<IEnumerable<BusinessEntityDto>>> GetAllSellerData([FromRoute] int storeId) 
        {
            var sellers = await _businessEntityService.GetAllBusinessEntities(storeId);
            //var sellers = await _sellerService.GetAllSellerData(storeId);

            return Ok(sellers);
        }
    }
}
