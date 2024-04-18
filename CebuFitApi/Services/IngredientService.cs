using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;
using CebuFitApi.Repositories;

namespace CebuFitApi.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStorageItemService _storageItemService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public IngredientService(IMapper mapper, IIngredientRepository ingredientRepository, IProductRepository productRepository, IStorageItemService storageItemService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _ingredientRepository = ingredientRepository;
            _storageItemService = storageItemService;
            _userRepository = userRepository;
        }

        public async Task<List<IngredientDTO>> GetAllIngredientsAsync(Guid userIdClaim)
        {
            var ingredientsEntities = await _ingredientRepository.GetAllAsync(userIdClaim);
            var ingredientsDTOs = _mapper.Map<List<IngredientDTO>>(ingredientsEntities);
            return ingredientsDTOs;
        }
        public async Task<List<IngredientWithProductDTO>> GetAllIngredientsWithProductAsync(Guid userIdClaim)
        {
            var ingredientsEntities = await _ingredientRepository.GetAllWithProductAsync(userIdClaim);
            var ingredientsDTOs = _mapper.Map<List<IngredientWithProductDTO>>(ingredientsEntities);
            return ingredientsDTOs;
        }
        public async Task<IngredientDTO> GetIngredientByIdAsync(Guid ingredientId, Guid userIdClaim)
        {
            var ingredientEntity = await _ingredientRepository.GetByIdAsync(ingredientId, userIdClaim);
            var ingredientDTO = _mapper.Map<IngredientDTO>(ingredientEntity);
            return ingredientDTO;
        }
        public async Task<IngredientWithProductDTO> GetIngredientByIdWithProductAsync(Guid ingredientId, Guid userIdClaim)
        {
            var ingredientEntity = await _ingredientRepository.GetByIdWithProductAsync(ingredientId, userIdClaim);
            var ingredientDTO = _mapper.Map<IngredientWithProductDTO>(ingredientEntity);
            return ingredientDTO;
        }
        public async Task<Guid> CreateIngredientAsync(IngredientCreateDTO ingredientDTO, Guid userIdClaim)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDTO);
            ingredient.Id = Guid.NewGuid();

            var foundUser = await _userRepository.GetById(userIdClaim);
            var baseProduct = await _productRepository.GetByIdAsync(ingredientDTO.baseProductId, userIdClaim);
            if (baseProduct != null && foundUser != null)
            {
                ingredient.Product = _mapper.Map<Product>(baseProduct);
                ingredient.User = foundUser;

                await _ingredientRepository.CreateAsync(ingredient, userIdClaim);
                return ingredient.Id;
            }
            return Guid.Empty;
        }
        public async Task UpdateIngredientAsync(IngredientDTO ingredientDTO, Guid userIdClaim)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDTO);
            var foundUser = await _userRepository.GetById(userIdClaim);
            if (foundUser != null)
            {
                await _ingredientRepository.UpdateAsync(ingredient, userIdClaim);
            }
        }
        public async Task DeleteIngredientAsync(Guid ingredientId, Guid userIdClaim)
        {
            var foundUser = await _userRepository.GetById(userIdClaim);
            if (foundUser != null)
            {
                await _ingredientRepository.DeleteAsync(ingredientId, userIdClaim);
            }
        }

        public async Task<bool> IsIngredientAvailable(IngredientCreateDTO ingredientDTO, Guid userIdClaim)
        {
            if (ingredientDTO.Quantity.HasValue || ingredientDTO.Weight.HasValue)
            {
                var storageItems = await _storageItemService.GetAllStorageItemsWithProductAsync(userIdClaim);

                bool isAvailable = storageItems.Any(storageItem =>
                    storageItem.Product.Id == ingredientDTO.baseProductId &&
                    (ingredientDTO.Quantity.HasValue
                        ? storageItem.ActualQuantity >= ingredientDTO.Quantity
                        : storageItem.ActualWeight >= ingredientDTO.Weight));

                return isAvailable;
            }
            return false;
        }
    }
}
