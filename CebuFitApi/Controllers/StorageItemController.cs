using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CebuFitApi.Controllers
{
    [ApiController]
    [Route("/api/storageitems")]
    public class StorageItemController : Controller
    {
        private readonly ILogger<StorageItemController> _logger;
        private readonly IMapper _mapper;
        private readonly IStorageItemService _storageItemService;
        public StorageItemController(ILogger<StorageItemController> logger, IMapper mapper, IStorageItemService storageItemService)
        {
            _logger = logger;
            _mapper = mapper;
            _storageItemService = storageItemService;
        }

        [HttpGet(Name = "GetStorageItems")]
        public async Task<ActionResult<List<StorageItemDTO>>> GetAll()
        {
            var storageItems = await _storageItemService.GetAllStorageItemsAsync();
            if(storageItems.Count == 0)
            {
                return NoContent();
            }
            return Ok(storageItems);
        }

        [HttpGet("withProduct/", Name = "GetStorageItemsWithProduct")]
        public async Task<ActionResult<List<StorageItemWithProductDTO>>> GetAllWithProduct()
        {
            var storageItems = await _storageItemService.GetAllStorageItemsWithProductAsync();
            if (storageItems.Count == 0)
            {
                return NoContent();
            }
            return Ok(storageItems);
        }

        [HttpGet("{siId}", Name = "GetStorageItemById")]
        public async Task<ActionResult<StorageItemDTO>> GetById(Guid siId)
        {
            var storageItem = await _storageItemService.GetStorageItemByIdAsync(siId);
            if (storageItem == null)
            {
                return NotFound();
            }
            return Ok(storageItem);
        }
        [HttpGet("withProduct/{siId}", Name = "GetStorageItemByIdWithProduct")]
        public async Task<ActionResult<StorageItemWithProductDTO>> GetByIdWithProduct(Guid siId)
        {
            var storageItem = await _storageItemService.GetStorageItemByIdWithProductAsync(siId);
            if (storageItem == null)
            {
                return NotFound();
            }
            return Ok(storageItem);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateStorageItem(StorageItemCreateDTO storageItemDTO)
        {
            if (storageItemDTO == null)
            {
                return BadRequest("Product data is null.");
            }

            await _storageItemService.CreateStorageItemAsync(storageItemDTO);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(StorageItemDTO storageItemDTO)
        {
            var existingStorageItem = await _storageItemService.GetStorageItemByIdAsync(storageItemDTO.Id);

            if (existingStorageItem == null)
            {
                return NotFound();
            }

            await _storageItemService.UpdateStorageItemAsync(storageItemDTO);

            return Ok();
        }

        [HttpDelete("{storageItemId}")]
        public async Task<ActionResult> DeleteProduct(Guid storageItemId)
        {
            var existingStorageItem = await _storageItemService.GetStorageItemByIdAsync(storageItemId);

            if (existingStorageItem == null)
            {
                return NotFound();
            }

            await _storageItemService.DeleteStorageItemAsync(storageItemId);

            return Ok();
        }
    }
}
