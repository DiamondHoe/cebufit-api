using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStorageItemService _storageItemService;
        private readonly IMapper _mapper;
        public IngredientService(IMapper mapper, IIngredientRepository ingredientRepository, IProductRepository productRepository, IStorageItemService storageItemService)
        {
           _mapper = mapper;
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _storageItemService = storageItemService;
        }

        public async Task<List<IngredientDTO>> GetAllIngredientsAsync()
        {
            var ingredientsEntities = await _ingredientRepository.GetAllAsync();
            var ingredientsDTOs = _mapper.Map<List<IngredientDTO>>(ingredientsEntities);
            return ingredientsDTOs;
        }
        public async Task<List<IngredientWithProductDTO>> GetAllIngredientsWithProductAsync()
        {
            var ingredientsEntities = await _ingredientRepository.GetAllWithProductAsync();
            var ingredientsDTOs = _mapper.Map<List<IngredientWithProductDTO>>(ingredientsEntities);
            return ingredientsDTOs;
        }
        public async Task<IngredientDTO> GetIngredientByIdAsync(Guid ingredientId)
        {
            var ingredientEntity = await _ingredientRepository.GetByIdAsync(ingredientId);
            var ingredientDTO = _mapper.Map<IngredientDTO>(ingredientEntity);
            return ingredientDTO;
        }
        public async Task<IngredientWithProductDTO> GetIngredientByIdWithProductAsync(Guid ingredientId)
        {
            var ingredientEntity = await _ingredientRepository.GetByIdWithProductAsync(ingredientId);
            var ingredientDTO = _mapper.Map<IngredientWithProductDTO>(ingredientEntity);
            return ingredientDTO;
        }
        public async Task CreateIngredientAsync(IngredientCreateDTO ingredientDTO)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDTO);
            ingredient.Id = Guid.NewGuid();

            var baseProduct = await _productRepository.GetByIdAsync(ingredientDTO.baseProductId);
            if (baseProduct == null)
            {
                throw new Exception("Product not found");
            }

            ingredient.Product = _mapper.Map<Product>(baseProduct);

            await _ingredientRepository.CreateAsync(ingredient);
        }
        public async Task UpdateIngredientAsync(IngredientDTO ingredientDTO)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDTO);
            await _ingredientRepository.UpdateAsync(ingredient);
        }
        public async Task DeleteIngredientAsync(Guid ingredientId)
        {
            await _ingredientRepository.DeleteAsync(ingredientId);
        }

        public async Task<bool> IsIngredientAvailable(IngredientCreateDTO ingredientDTO)
        {
            if (ingredientDTO.Quantity.HasValue || ingredientDTO.Weight.HasValue)
            {
                var storageItems = await _storageItemService.GetAllStorageItemsWithProductAsync();

                bool isAvailable = storageItems.Any(storageItem =>
                    storageItem.Product.Id == ingredientDTO.baseProductId &&
                    (ingredientDTO.Quantity.HasValue
                        ? storageItem.Quantity >= ingredientDTO.Quantity
                        : storageItem.Weight >= ingredientDTO.Weight));

                return isAvailable;
            }
            return false;
        }
    }
}
