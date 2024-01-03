using AutoMapper;
using CebuFitApi.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("/api/products")]
    public class ProductController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(ILogger<CategoryController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }
        
        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            if(products.Count == 0) 
            {
                return NoContent();
            }
            return Ok(products);
        }
        
        [HttpGet("withMacro/", Name ="GetProductsWithMacro")]
        public async Task<ActionResult<List<ProductWithMacroDTO>>> GetAllWithMacro()
        {
            var products = await _productService.GetAllProductsWithMacroAsync();
            if (products.Count == 0)
            {
                return NoContent();
            }
            return Ok(products);
        }
        [HttpGet("{productId}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("withMacro/{productId}", Name = "GetProductWithMacro")]
        public async Task<ActionResult<ProductWithMacroDTO>> GetByIdWithMacro(Guid productId)
        {
            var product = await _productService.GetProductByIdWithMacroAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductCreateDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Product data is null.");
            }

            await _productService.CreateProductAsync(productDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(ProductUpdateDTO productDTO)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productDTO.Id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productService.UpdateProductAsync(productDTO);

            return Ok();
        }


        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(Guid productId)
        {
            var existingProduct = await _productService.GetProductByIdAsync(productId);

            if (existingProduct == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync(productId);

            return Ok();
        }
    }
}
