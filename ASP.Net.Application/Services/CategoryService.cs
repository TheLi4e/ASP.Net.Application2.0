using ASP.Net.Application.Interfaces;
using ASP.Net.Application.SDK.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Net.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly SDK.AppDbContext _context;

        public CategoryService(SDK.AppDbContext context, IMemoryCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            if (_cache.TryGetValue("categories", out IEnumerable<CategoryDto> categories))
                return categories;

            using (_context)
            {
                var categoriesDto = _context.Categories.Select(x => _mapper.Map<CategoryDto>(x)).ToList();

                _cache.Set("categories", categoriesDto);

                return categoriesDto;
            }
        }

        public CategoryDto? GetCategory(Guid id)
        {
            if (_cache.TryGetValue("categories", out IEnumerable<CategoryDto> categories))
            {
                var category = categories.FirstOrDefault(x => x.Id == id);
                if (category != null) return category;
            }

            using (_context)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == id);
                return _mapper.Map<CategoryDto>(category);
            }
        }

        public Guid SaveCategory(CategoryDto category)
        {
            using (_context)
            {
                if (!_context.Categories.Any(x => x.Name.ToLower().Equals(category.Name.ToLower())))
                {
                    if (category.Id == null)
                        category.Id = Guid.NewGuid();

                    var entity = _mapper.Map<CategoryEntity>(category);
                    _context.Categories.Add(entity);
                    _context.SaveChanges();

                    _cache.Remove("category");
                    return entity.Id;
                }
            }
            return Guid.Empty;
        }

        public bool AddProduct(Guid categoryId, Guid productId)
        {
            using (_context)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
                var product = _context.Products.FirstOrDefault(x => x.Id == productId);
                if (category != null && product != null)
                {

                    _context.Categories.SingleOrDefault(x => x.Products.Select(x => x.Id).Contains(productId))?.Products.Remove(product);

                    category.Products.Add(product);
                    _context.SaveChanges();

                    _cache.Remove("category");
                    _cache.Remove("products");
                    return true;
                }
                return false;
            }
        }

        public bool DeleteCategory(Guid categoryId)
        {
            using (_context)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == categoryId);
                if (category != null)
                {
                    _context.Categories.Remove(category);

                    _context.SaveChanges();

                    _cache.Remove("categories");
                    _cache.Remove("products");

                    return true;
                }
                return false;
            }
        }
    }
}
