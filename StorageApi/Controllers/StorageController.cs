using Microsoft.AspNetCore.Mvc;
using StorageApi.Services;

namespace StorageApi.Controllers
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

        [HttpGet("products/{storageId}")]
        public IActionResult GetProducts(Guid storageId)
        {
            var products = _storageService.GetProducts(storageId);
            return Ok(products);
        }
    };
}