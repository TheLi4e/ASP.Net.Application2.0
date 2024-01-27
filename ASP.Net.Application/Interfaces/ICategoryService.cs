using ASP.Net.Application.SDK.Models;

namespace ASP.Net.Application.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto? GetCategory(Guid id);
        Guid SaveCategory(CategoryDto category);
        bool AddProduct(Guid categoryId, Guid productId);
        bool DeleteCategory(Guid categoryId);
    }
}
