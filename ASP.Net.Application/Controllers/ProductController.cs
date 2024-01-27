using ASP.Net.Application.Interfaces;
using ASP.Net.Application.SDK.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Net.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {

            var response = _productService.GetProducts();
            return Ok(response);
        }

        [HttpGet("product/{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productService.GetProduct(id);
            return Ok(product);
        }

        [HttpPost("product")]
        public IActionResult SaveProduct([FromBody] ProductDto product)
        {
            var result = _productService.SaveProduct(product);
            return Ok(result);
        }

        [HttpPost("set_price")]
        public IActionResult SetPrice([FromQuery] Guid productId, decimal price)
        {
            _productService.SetPrice(productId, price);
            return Ok();
        }

        [HttpPost("add_count")]
        public IActionResult AddCount([FromQuery] Guid productId, int newCount)
        {
            _productService.AddCount(productId, newCount);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(Guid productId)
        {
            _productService.DeleteProduct(productId);
            return Ok();
        }

        [HttpGet("report_SCV")]
        public IActionResult GetProductReport()
        {
            var content = _productService.GetProductReport();
            return File(new System.Text.UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }
    }
}
