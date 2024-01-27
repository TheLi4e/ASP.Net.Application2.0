using ASP.Net.Application.SDK.Models;

namespace ASP.Net.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto? GetProduct(Guid id);
        Guid SaveProduct(ProductDto product);
        void SetPrice(Guid ProductId, decimal price);
        void AddCount(Guid ProductId, int addCount);
        void DeleteProduct(Guid productId);
        string GetProductReport();
    }
}
