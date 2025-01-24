using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Entities;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.BarcodeDto;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeService _barcodeService;

        public BarcodeController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        [HttpPost("barcode")]
        public async Task<ActionResult> AddNewBarcode([FromBody] AddNewBarcodeDto dto, [FromRoute] int storeId) 
        {
            var barcodeId = await _barcodeService.AddNewBarcode(dto, storeId);
            return Created($"api/tradingSystem/store={storeId}/barcode={barcodeId}", null);
        }

        [HttpPut("barcode={barcodeId}")]
        public async Task<ActionResult> UpdateBarcodeDataById([FromBody] UpdateBarcodeDataDto dto, [FromRoute] int storeId, [FromRoute] int barcodeId) 
        {
            await _barcodeService.UpdateBarcodeDataById(dto, storeId, barcodeId);
            return Ok();
        }

        [HttpDelete("barcode={barcodeId}")]
        public async Task<ActionResult> DeleteBarcodeById([FromRoute] int storeId, [FromRoute] int barcodeId) 
        {
            await _barcodeService.DeleteBarcodeById(storeId, barcodeId);
            return Ok();
        }

        [HttpGet("barcode={barcodeId}")]
        public async Task<ActionResult<BarcodeDto>> GetBarcodeDataById([FromRoute] int storeId, [FromRoute] int barcodeId) 
        {
            var barcode = await _barcodeService.GetBarcodeDataById(storeId, barcodeId);
            return Ok(barcode);
        }

        [HttpGet("barcode/code={code}")]
        public async Task<ActionResult<BarcodeDto>> GetBarcodeDataByCode([FromRoute] int storeId, [FromRoute] string code)
        {
            var barcode = await _barcodeService.GetBarcodeDataByCode(storeId, code);
            return Ok(barcode);
        }

        [HttpGet("barcode")]
        public async Task<ActionResult<IEnumerable<BarcodeDto>>> GetAllBarcodesData([FromRoute] int storeId)
        {
            var barcodes = await _barcodeService.GetAllBarcodesData(storeId);
            return Ok(barcodes);
        }
    }
}
