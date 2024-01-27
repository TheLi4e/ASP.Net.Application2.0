using ASP.Net.Application.Interfaces;
using ASP.Net.Application.SDK.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            try
            {
                var result = _categoryService.GetCategories();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("caegory/{id}")]
        public IActionResult GetCategory(Guid id)
        {
            try
            {
                var category = _categoryService.GetCategory(id);
                return Ok(category);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("category")]
        public IActionResult SaveCategory([FromBody] CategoryDto category)
        {
            try
            {
                var result = _categoryService.SaveCategory(category);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("add_product")]
        public IActionResult AddProduct([FromQuery] Guid categoryId, Guid productId)
        {
            try
            {
                var result = _categoryService.AddProduct(categoryId, productId);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            try
            {
                var result = _categoryService.DeleteCategory(categoryId);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
