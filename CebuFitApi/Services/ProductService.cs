using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Helpers.Enums;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> GetAllProductsAsync(Guid userIdClaim, DataType dataType)
        {
            var productsEntities = await _productRepository.GetAllAsync(userIdClaim, dataType);
            var productsDTOs = _mapper.Map<List<ProductDTO>>(productsEntities);
            return productsDTOs;
        }
        public async Task<List<ProductWithMacroDTO>> GetAllProductsWithMacroAsync(Guid userIdClaim, DataType dataType)
        {
            var productsEntities = await _productRepository.GetAllAsync(userIdClaim, dataType);
            var productsDTOs = _mapper.Map<List<ProductWithMacroDTO>>(productsEntities);
            return productsDTOs;
        }
        public async Task<List<ProductWithCategoryDTO>> GetAllProductsWithCategoryAsync(Guid userIdClaim, DataType dataType )
        {
            var productsEntities = await _productRepository.GetAllAsync(userIdClaim, dataType);
            var productsDTOs = _mapper.Map<List<ProductWithCategoryDTO>>(productsEntities);
            return productsDTOs;
        }
        public async Task<List<ProductWithDetailsDTO>> GetAllProductsWithDetailsAsync(Guid userIdClaim, DataType dataType)
        {
            var productsEntities = await _productRepository.GetAllAsync(userIdClaim, dataType);
            var productsDTOs = _mapper.Map<List<ProductWithDetailsDTO>>(productsEntities);
            return productsDTOs;
        }
        public async Task<ProductDTO> GetProductByIdAsync(Guid productId, Guid userIdClaim)
        {
            var productEntity = await _productRepository.GetByIdAsync(productId, userIdClaim);
            var productDTO = _mapper.Map<ProductDTO>(productEntity);
            return productDTO;
        }
        public async Task<ProductWithMacroDTO> GetProductByIdWithMacroAsync(Guid productId, Guid userIdClaim)
        {
            var productEntity = await _productRepository.GetByIdWithDetailsAsync(productId, userIdClaim);
            var productDTO = _mapper.Map<ProductWithMacroDTO>(productEntity);
            return productDTO;
        }
        public async Task<ProductWithCategoryDTO> GetProductByIdWithCategoryAsync(Guid productId, Guid userIdClaim)
        {
            var productEntity = await _productRepository.GetByIdWithDetailsAsync(productId, userIdClaim);
            var productDTO = _mapper.Map<ProductWithCategoryDTO>(productEntity);
            return productDTO;
        }
        public async Task<ProductWithDetailsDTO> GetProductByIdWithDetailsAsync(Guid productId, Guid userIdClaim)
        {
            var productEntity = await _productRepository.GetByIdWithDetailsAsync(productId, userIdClaim);
            var productDTO = _mapper.Map<ProductWithDetailsDTO>(productEntity);
            return productDTO;
        }
        public async Task CreateProductAsync(ProductCreateDTO productDTO, Guid userIdClaim)
        {
            var product = _mapper.Map<Product>(productDTO);
            product.Id = Guid.NewGuid();

            var macro = _mapper.Map<Macro>(productDTO.Macro);
            macro.Id = Guid.NewGuid();
            product.Macro = macro;

            var foundUser = await _userRepository.GetById(userIdClaim);
            var category = await _categoryRepository.GetByIdAsync(productDTO.CategoryId, userIdClaim);

            if (foundUser != null && category != null)
            {
                product.User = foundUser;
                product.Category = _mapper.Map<Category>(category);

                await _productRepository.CreateAsync(product, userIdClaim);
            }
        }
        public async Task UpdateProductAsync(ProductUpdateDTO productDTO, Guid userIdClaim)
        {
            var product = _mapper.Map<Product>(productDTO);
            var macro = _mapper.Map<Macro>(productDTO.Macro);
            var foundUser = await _userRepository.GetById(userIdClaim);
            var category = await _categoryRepository.GetByIdAsync(productDTO.CategoryId, userIdClaim);

            if (foundUser != null && category != null)
            {
                product.Macro = macro;
                product.Category = category;

                await _productRepository.UpdateAsync(product, userIdClaim);
            }
        }
        public async Task DeleteProductAsync(Guid productId)
        {
            await _productRepository.DeleteAsync(productId);
        }
    }
}
