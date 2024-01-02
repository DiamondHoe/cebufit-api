using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Models;

namespace CebuFitApi.Services
{
    public class StorageItemService : IStorageItemService
    {
        private readonly IStorageItemRepository _storageItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public StorageItemService(IMapper mapper,IStorageItemRepository storageItemRepository, IProductRepository productRepository)
        {
            _mapper = mapper;
            _storageItemRepository = storageItemRepository;
            _productRepository = productRepository;
        }
        public async Task<List<StorageItemDTO>> GetAllStorageItemsAsync()
        {
            var storageItemsEntities = await _storageItemRepository.GetAllAsync();
            var storageItemsDTOs = _mapper.Map<List<StorageItemDTO>>(storageItemsEntities);
            return storageItemsDTOs;
        }
        public async Task<List<StorageItemWithProductDTO>> GetAllStorageItemsWithProductAsync()
        {
            var storageItemsEntities = await _storageItemRepository.GetAllWithProductAsync();
            var storageItemsDTOs = _mapper.Map<List<StorageItemWithProductDTO>>(storageItemsEntities);
            return storageItemsDTOs;
        }

        public async Task<StorageItemDTO> GetStorageItemByIdAsync(Guid storageItemId)
        {
            var storageItemsEntity = await _storageItemRepository.GetByIdAsync(storageItemId);
            var storageItemsDTO = _mapper.Map<StorageItemDTO>(storageItemsEntity);
            return storageItemsDTO;
        }
        public async Task<StorageItemWithProductDTO> GetStorageItemByIdWithProductAsync(Guid storageItemId)
        {
            var storageItemsEntity = await _storageItemRepository.GetByIdWithProductAsync(storageItemId);
            var storageItemsDTO = _mapper.Map<StorageItemWithProductDTO>(storageItemsEntity);
            return storageItemsDTO;
        }
        public async Task CreateStorageItemAsync(StorageItemCreateDTO storageItemDTO)
        {
            var storageItem = _mapper.Map<StorageItem>(storageItemDTO);
            storageItem.Id = Guid.NewGuid();

            var baseProduct = await _productRepository.GetByIdAsync(storageItemDTO.baseProductId);
            if (baseProduct == null)
            {
                throw new Exception("Product not found");
            }

            storageItem.Product = _mapper.Map<Product>(baseProduct);

            await _storageItemRepository.CreateAsync(storageItem);
        }

        public async Task UpdateStorageItemAsync(StorageItemDTO storageItemDTO)
        {
            var storageItem = _mapper.Map<StorageItem>(storageItemDTO);
            await _storageItemRepository.UpdateAsync(storageItem);
        }
        public async Task DeleteStorageItemAsync(Guid storageItemId)
        {
            await _storageItemRepository.DeleteAsync(storageItemId);
        }
    }
}
