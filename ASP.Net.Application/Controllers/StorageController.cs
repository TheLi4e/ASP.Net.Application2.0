using ASP.Net.Application.Interfaces;
using ASP.Net.Application.SDK.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet("storages")]
        public IActionResult GetStorages()
        {
            try
            {
                var result = _storageService.GetStorages();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("storage/{id}")]
        public IActionResult GetStorage(Guid id)
        {
            try
            {
                var result = _storageService.GetStorage(id);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("storage")]
        public IActionResult SaveCategory([FromBody] StorageDto storage)
        {
            try
            {
                var result = _storageService.SaveCategory(storage);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("add_product")]
        public IActionResult AddStorage([FromQuery] Guid storageId, Guid productId)
        {
            try
            {
                var result = _storageService.AddProduct(storageId, productId);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{storageId}")]
        public IActionResult DeleteStorage(Guid storageId)
        {
            try
            {
                var result = _storageService.DeleteStorage(storageId);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
