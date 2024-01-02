using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var productsEntities = await _productRepository.GetAllAsync();
            var productsDTOs = _mapper.Map<List<ProductDTO>>(productsEntities);
            return productsDTOs;
        }
        public async Task<List<ProductWithMacroDTO>> GetAllProductsWithMacroAsync()
        {
            var productsEntities = await _productRepository.GetAllWithMacroAsync();
            var productsDTOs = _mapper.Map<List<ProductWithMacroDTO>>(productsEntities);
            return productsDTOs;
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid productId)
        {
            var productEntity = await _productRepository.GetByIdAsync(productId);
            var productDTO = _mapper.Map<ProductDTO>(productEntity);
            return productDTO;
        }
        public async Task<ProductWithMacroDTO> GetProductByIdWithMacroAsync(Guid productId)
        {
            var productEntity = await _productRepository.GetByIdWithMacroAsync(productId);
            var productDTO = _mapper.Map<ProductWithMacroDTO>(productEntity);
            return productDTO;
        }
        public async Task CreateProductAsync(ProductCreateDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            product.Id = Guid.NewGuid();

            var macro = _mapper.Map<Macro>(productDTO.Macro);
            macro.Id = Guid.NewGuid();
            product.Macro = macro;

            var category = await _categoryRepository.GetByIdAsync(productDTO.CategoryId);
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            product.Category = _mapper.Map<Category>(category);

            await _productRepository.CreateAsync(product);
        }
        public async Task UpdateProductAsync(ProductUpdateDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            var macro = _mapper.Map<Macro>(productDTO.Macro);
            product.Macro = macro;

            await _productRepository.UpdateAsync(product);
        }
        public async Task DeleteProductAsync(Guid productId)
        {
            await _productRepository.DeleteAsync(productId);
        }
    }
}
