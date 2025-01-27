using Microsoft.AspNetCore.Mvc;
using TradingSystemApi.Interface.ServicesInterface;
using TradingSystemApi.Models.ProductCategory;

namespace TradingSystemApi.Controllers
{
    [Route("api/tradingSystem/store={storeId}")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("productCategory")]
        public async Task<ActionResult> AddNewProductCategory([FromBody] AddNewProductCategoryDto dto, [FromRoute] int storeId) 
        {
            var productCategoryId = await _productCategoryService.AddNewProductCategory(dto, storeId);
            return Created($"api/tradingSystem/store={storeId}/productCategory={productCategoryId}", null);
        }

        [HttpPut("productCategory={productCategoryId}")]
        public async Task<ActionResult> UpdateProductCategoryDataById([FromBody] UpdateProductCategoryDataDto dto, [FromRoute] int storeId, [FromRoute] int productCategoryId) 
        {
            await _productCategoryService.UpdateProductCategoryDataById(dto, storeId, productCategoryId);
            return Ok();
        }

        [HttpDelete("productCategory={productCategoryId}")]
        public async Task<ActionResult> DeleteProductCategoryById([FromRoute] int storeId, [FromRoute] int productCategoryId)
        {
            await _productCategoryService.DeleteProductCategoryById(storeId, productCategoryId);
            return NoContent();
        }

        [HttpGet("productCategory={productCategoryId}")]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategoryDataById([FromRoute] int storeId, [FromRoute] int productCategoryId)
        {
            var productCategory = await _productCategoryService.GetProductCategoryDataById(storeId, productCategoryId);
            return Ok(productCategory);
        }

        [HttpGet("productCategoryName={productCategoryName}")]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategoryDataByName([FromRoute] int storeId, [FromRoute] string productCategoryName)
        {
            var productCategory = await _productCategoryService.GetProductCategoryDataByName(storeId, productCategoryName);
            return Ok(productCategory);
        }

        [HttpGet("productCategory")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAllProductCategoriesData([FromRoute] int storeId)
        {
            var productCategories = await _productCategoryService.GetAllProductCategoriesData(storeId);
            return Ok(productCategories);
        }
    }
}
