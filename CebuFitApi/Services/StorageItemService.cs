using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class StorageItemService : IStorageItemService
    {
        private readonly IStorageItemRepository _storageItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public StorageItemService(IMapper mapper, IStorageItemRepository storageItemRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _storageItemRepository = storageItemRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;

        }
        public async Task<List<StorageItemDTO>> GetAllStorageItemsAsync(Guid userIdClaim)
        {
            var storageItemsEntities = await _storageItemRepository.GetAllAsync(userIdClaim);
            var storageItemsDTOs = _mapper.Map<List<StorageItemDTO>>(storageItemsEntities);
            return storageItemsDTOs;
        }
        public async Task<List<StorageItemWithProductDTO>> GetAllStorageItemsWithProductAsync(Guid userIdClaim)
        {
            var storageItemsEntities = await _storageItemRepository.GetAllWithProductAsync(userIdClaim);
            var storageItemsDTOs = _mapper.Map<List<StorageItemWithProductDTO>>(storageItemsEntities);
            return storageItemsDTOs;
        }
        public async Task<List<StorageItemWithProductDTO>> GetAllStorageItemsByProductIdWithProductAsync(Guid productId, Guid userIdClaim)
        {
            var storageItemsEntities = await _storageItemRepository.GetAllByProductIdWithProductAsync(productId, userIdClaim);
            var storageItemsDTOs = _mapper.Map<List<StorageItemWithProductDTO>>(storageItemsEntities);
            return storageItemsDTOs;
        }
        public async Task<StorageItemDTO> GetStorageItemByIdAsync(Guid storageItemId, Guid userIdClaim)
        {
            var storageItemEntity = await _storageItemRepository.GetByIdAsync(storageItemId, userIdClaim);
            var storageItemDTO = _mapper.Map<StorageItemDTO>(storageItemEntity);
            return storageItemDTO;
        }
        public async Task<StorageItemWithProductDTO> GetStorageItemByIdWithProductAsync(Guid storageItemId, Guid userIdClaim)
        {
            var storageItemsEntity = await _storageItemRepository.GetByIdWithProductAsync(storageItemId, userIdClaim);
            var storageItemsDTO = _mapper.Map<StorageItemWithProductDTO>(storageItemsEntity);
            return storageItemsDTO;
        }
        public async Task CreateStorageItemAsync(StorageItemCreateDTO storageItemDTO, Guid userIdClaim)
        {
            var storageItem = _mapper.Map<StorageItem>(storageItemDTO);
            storageItem.Id = Guid.NewGuid();

            var foundUser = await _userRepository.GetById(userIdClaim);
            var baseProduct = await _productRepository.GetByIdAsync(storageItemDTO.baseProductId, userIdClaim);
            if (baseProduct != null && foundUser != null)
            {
                storageItem.User = foundUser;
                storageItem.Product = _mapper.Map<Product>(baseProduct);

                await _storageItemRepository.CreateAsync(storageItem, userIdClaim);
            }
        }

        public async Task UpdateStorageItemAsync(StorageItemDTO storageItemDTO, Guid userIdClaim)
        {
            var storageItem = _mapper.Map<StorageItem>(storageItemDTO);
            var foundUser = await _userRepository.GetById(userIdClaim);
            if (foundUser != null)
            {
                await _storageItemRepository.UpdateAsync(storageItem, userIdClaim);
            }
        }
        public async Task DeleteStorageItemAsync(Guid storageItemId, Guid userIdClaim)
        {
            await _storageItemRepository.DeleteAsync(storageItemId, userIdClaim);
        }

    }
}
