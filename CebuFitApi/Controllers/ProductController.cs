using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/products")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper, IJwtTokenHelper jwtTokenHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll(DataType dataType = DataType.Both)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var products = await _productService.GetAllProductsAsync(userIdClaim, dataType);
                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            return NotFound("User not found");
        }

        [HttpGet("withMacro/", Name = "GetProductsWithMacro")]
        public async Task<ActionResult<List<ProductWithMacroDTO>>> GetAllWithMacro(
            [FromQuery] DataType dataType = DataType.Both)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var products = await _productService.GetAllProductsWithMacroAsync(userIdClaim, dataType);
                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            return NotFound("User not found");
        }

        [HttpGet("withCategory/", Name = "GetProductsWithCategory")]
        public async Task<ActionResult<List<ProductWithCategoryDTO>>> GetAllWithCategory(DataType dataType = DataType.Both)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var products = await _productService.GetAllProductsWithCategoryAsync(userIdClaim, dataType);
                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            return NotFound("User not found");
        }

        [HttpGet("withDetails/", Name = "GetProductsWithDetails")]
        public async Task<ActionResult<List<ProductWithDetailsDTO>>> GetAllWithDetails(
            [FromQuery] DataType dataType = DataType.Both)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var products = await _productService.GetAllProductsWithDetailsAsync(userIdClaim, dataType);
                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            return NotFound("User not found");
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid productId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var product = await _productService.GetProductByIdAsync(productId, userIdClaim);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            return NotFound("User not found");
        }

        [HttpGet("withMacro/{productId}", Name = "GetProductWithMacro")]
        public async Task<ActionResult<ProductWithMacroDTO>> GetByIdWithMacro(Guid productId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var product = await _productService.GetProductByIdWithMacroAsync(productId, userIdClaim);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            return NotFound("User not found");
        }

        [HttpGet("withCategory/{productId}", Name = "GetProductWithCategory")]
        public async Task<ActionResult<ProductWithCategoryDTO>> GetByIdWithCategory(Guid productId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var product = await _productService.GetProductByIdWithCategoryAsync(productId, userIdClaim);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            return NotFound("User not found");
        }

        [HttpGet("withDetails/{productId}", Name = "GetProductWithDetails")]
        public async Task<ActionResult<ProductWithDetailsDTO>> GetByIdWithDetails(Guid productId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var product = await _productService.GetProductByIdWithDetailsAsync(productId, userIdClaim);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreateDTO productDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (productDTO == null)
                {
                    return BadRequest("Product data is null.");
                }

                await _productService.CreateProductAsync(productDTO, userIdClaim);

                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductUpdateDTO productDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingProduct = await _productService.GetProductByIdAsync(productDTO.Id, userIdClaim);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                await _productService.UpdateProductAsync(productDTO, userIdClaim);

                return Ok();
            }
            return NotFound("User not found");
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();
            var userRoleClaim = _jwtTokenHelper.GetUserRole();
            if(userRoleClaim == RoleEnum.Admin)
            {
                await _productService.DeleteProductAsync(productId);
                return Ok();
            }
            if (userIdClaim != Guid.Empty)
            {
                var existingProduct = await _productService.GetProductByIdAsync(productId, userIdClaim);

                if (existingProduct == null)
                {
                    return NotFound();
                }
                if (existingProduct.IsPublic)
                {
                    return BadRequest("Cannot delete public product.");
                }

                await _productService.DeleteProductAsync(productId);

                return Ok();
            }
            return NotFound("User not found");
        }

        #region Importance
        [HttpGet("importances", Name = "GetImportances")]
        public async Task<ActionResult<Dictionary<string, int>>> GetImportances()
        {
            Dictionary<string, int> importanceDict = new Dictionary<string, int>();
            var importanceValues = Enum.GetValues(typeof(ImportanceEnum));

            foreach (var value in importanceValues)
            {
                // Assuming the enum values are strings, you can convert them to string
                var stringValue = value.ToString();

                // Assign each enum value to a corresponding key in the dictionary
                importanceDict[stringValue] = (int)value;
            }

            return Ok(importanceDict);
        }
        #endregion
    }
}
