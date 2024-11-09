using AutoMapper;
using CebuFitApi.DTOs;
using CebuFitApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CebuFitApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/storageitems")]
    public class StorageItemController : ControllerBase
    {
        private readonly ILogger<StorageItemController> _logger;
        private readonly IMapper _mapper;
        private readonly IStorageItemService _storageItemService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public StorageItemController(
            ILogger<StorageItemController> logger,
            IMapper mapper,
            IStorageItemService storageItemService,
            IJwtTokenHelper jwtTokenHelper)
        {
            _logger = logger;
            _mapper = mapper;
            _storageItemService = storageItemService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpGet(Name = "GetStorageItems")]
        public async Task<ActionResult<List<StorageItemDTO>>> GetAll()
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItems = await _storageItemService.GetAllStorageItemsAsync(userIdClaim);
                if (storageItems.Count == 0)
                {
                    return NoContent();
                }
                return Ok(storageItems);
            }

            return NotFound("User not found");
        }

        [HttpGet("withProduct/", Name = "GetStorageItemsWithProduct")]
        public async Task<ActionResult<List<StorageItemWithProductDTO>>> GetAllWithProduct(bool withoutEaten = false)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItems = await _storageItemService.GetAllStorageItemsWithProductAsync(userIdClaim, withoutEaten);
                if (storageItems.Count == 0)
                {
                    return NoContent();
                }
                return Ok(storageItems);
            }

            return NotFound("User not found");
        }
        [HttpGet("byProduct/{productId}", Name = "GetStorageItemsByProductId")]
        public async Task<ActionResult<List<StorageItemDTO>>> GetAllByProductIdWithProduct([FromQuery] Guid productId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItems = await _storageItemService.GetAllStorageItemsByProductIdWithProductAsync(productId, userIdClaim);
                if (storageItems.Count == 0)
                {
                    return NoContent();
                }
                return Ok(storageItems);
            }

            return NotFound("User not found");
        }

        [HttpGet("{siId}", Name = "GetStorageItemById")]
        public async Task<ActionResult<StorageItemDTO>> GetById(Guid siId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItem = await _storageItemService.GetStorageItemByIdAsync(siId, userIdClaim);
                if (storageItem == null)
                {
                    return NotFound();
                }
                return Ok(storageItem);
            }

            return NotFound("User not found");
        }

        [HttpGet("withProduct/{siId}", Name = "GetStorageItemByIdWithProduct")]
        public async Task<ActionResult<StorageItemWithProductDTO>> GetByIdWithProduct(Guid siId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var storageItem = await _storageItemService.GetStorageItemByIdWithProductAsync(siId, userIdClaim);
                if (storageItem == null)
                {
                    return NotFound();
                }
                return Ok(storageItem);
            }

            return NotFound("User not found");
        }

        [HttpPost]
        public async Task<ActionResult> CreateStorageItem(StorageItemCreateDTO storageItemDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                if (storageItemDTO == null)
                {
                    return BadRequest("Storage item data is null.");
                }

                await _storageItemService.CreateStorageItemAsync(storageItemDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStorageItem(StorageItemDTO storageItemDTO)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingStorageItem = await _storageItemService.GetStorageItemByIdAsync(storageItemDTO.Id, userIdClaim);

                if (existingStorageItem == null)
                {
                    return NotFound();
                }

                await _storageItemService.UpdateStorageItemAsync(storageItemDTO, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }

        [HttpDelete("{storageItemId}")]
        public async Task<ActionResult> DeleteStorageItem(Guid storageItemId)
        {
            var userIdClaim = _jwtTokenHelper.GetCurrentUserId();

            if (userIdClaim != Guid.Empty)
            {
                var existingStorageItem = await _storageItemService.GetStorageItemByIdAsync(storageItemId, userIdClaim);

                if (existingStorageItem == null)
                {
                    return NotFound();
                }

                await _storageItemService.DeleteStorageItemAsync(storageItemId, userIdClaim);

                return Ok();
            }

            return NotFound("User not found");
        }
    }
}
