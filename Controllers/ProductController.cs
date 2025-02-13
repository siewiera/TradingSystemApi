using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.Product;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("product")]
        public async Task<ActionResult> AddNewProduct([FromBody] AddNewProductDto dto, [FromRoute] int storeId) 
        {
            var productId = await _productService.AddNewProduct(dto, storeId);
            return Created($"api/tradingSystem/store={storeId}/product={productId}", null);
        }

        [HttpPut("product={productId}")]
        public async Task<ActionResult> UpdateProductDataById([FromBody] UpdateProductDataDto dto, [FromRoute] int storeId, [FromRoute] int productId) 
        {
            await _productService.UpdateProductDataById(dto, storeId, productId);
            return Ok();
        }

        [HttpDelete("product={productId}")]
        public async Task<ActionResult> DeleteProductById([FromRoute] int storeId, [FromRoute] int productId) 
        {
            await _productService.DeleteProductById(storeId, productId);
            return NoContent();
        }

        [HttpGet("product={productId}")]
        public async Task<ActionResult<ProductDto>> GetProductDataById([FromRoute] int storeId, [FromRoute] int productId) 
        {
            var product = await _productService.GetProductDataById(storeId, productId);
            return Ok(product);
        }

        [HttpGet("product/productCode={productCode}")]
        public async Task<ActionResult<ProductDto>> GetProductDataByProductCode([FromRoute] int storeId, [FromRoute] int productCode) 
        {
            var product = await _productService.GetProductDataByProductCode(storeId, productCode);
            return Ok(product);
        }

        [HttpGet("product/barcode={barcode}")]
        public async Task<ActionResult<ProductDto>> GetProductDataByBarcode([FromRoute] int storeId, [FromRoute] string barcode) 
        {
            var product = await _productService.GetProductDataByBarcode(storeId, barcode);
            return Ok(product);
        }

        [HttpGet("product/productName={productName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsDataByName([FromRoute] int storeId, [FromRoute] string productName) 
        {
            var product  = await _productService.GetProductsDataByName(storeId, productName);
            return Ok(product);
        }

        [HttpGet("product/productCategory={productCategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsDataByProductCategoryId([FromRoute] int storeId, [FromRoute] int productCategoryId) 
        {
            var product = await _productService.GetProductsDataByProductCategoryId(storeId, productCategoryId);
            return Ok(product);
        }

        [HttpGet("product")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> MyProperty([FromRoute] int storeId)
        {
            var product = await _productService.GetAllProductsData(storeId);
            return Ok(product);
        }
    }
}
