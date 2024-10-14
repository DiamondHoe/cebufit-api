using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/productTypes")]
    public class ProductTypeController(
        IProductTypeService productTypeService,
        IJwtTokenHelper jwtTokenHelper) : Controller
    {
        [HttpGet(Name = "GetProductTypes")]
        public async Task<ActionResult<ProductTypeDto>> GetAll()
        {
            var userIdClaim = jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty) return NotFound("Username not found");
            
            var productTypes = await productTypeService.GetAllProductTypesAsync(userIdClaim);
            if (productTypes.Count == 0) return NoContent();
            return Ok(productTypes);
        }

        [HttpGet("{productTypeId}", Name = "GetProductTypeById")]
        public async Task<ActionResult<ProductTypeDto>> GetById(Guid productTypeId)
        {
            var userIdClaim = jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty) return NotFound("User not found");
            
            var productType = await productTypeService.GetProductTypeByIdAsync(productTypeId, userIdClaim);
            if (productType == null) return NotFound();
            return Ok(productType);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductType([FromBody] ProductTypeCreateDto? productTypeCreateDto)
        {
            if (productTypeCreateDto == null) return BadRequest("ProductType data is null.");
            
            var userIdClaim = jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty)return NotFound("User not found");
            
            await productTypeService.CreateProductTypeAsync(productTypeCreateDto, userIdClaim);
            return Ok();
            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductType(ProductTypeDto productTypeDto)
        {
            var userIdClaim = jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty) return NotFound("User not found");
            
            var existingProductType = await productTypeService.GetProductTypeByIdAsync(productTypeDto.Id, userIdClaim);
            if (existingProductType == null) return NotFound();

            await productTypeService.UpdateProductTypeAsync(productTypeDto, userIdClaim);
            return Ok();
        }

        [HttpDelete("{productTypeId}")]
        public async Task<ActionResult> DeleteProductType(Guid productTypeId)
        {
            var userIdClaim = jwtTokenHelper.GetCurrentUserId();
            if (userIdClaim == Guid.Empty) return NotFound("User not found");
            
            var existingProductType = await productTypeService.GetProductTypeByIdAsync(productTypeId, userIdClaim);
            if (existingProductType == null) return NotFound();

            await productTypeService.DeleteProductTypeAsync(productTypeId, userIdClaim);
            return Ok();
        }
    }
}
