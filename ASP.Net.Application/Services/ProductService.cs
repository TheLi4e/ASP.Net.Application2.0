using System.Text;
using ASP.Net.Application.Interfaces;
using ASP.Net.Application.SDK.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Net.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly SDK.AppDbContext _context;

        public ProductService(SDK.AppDbContext context, IMemoryCache cache, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _cache = cache;
        }
        public IEnumerable<ProductDto> GetProducts()
        {
            if (_cache.TryGetValue("products", out IEnumerable<ProductDto> products))
                return products;

            using (_context)
            {
                var productsDto = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();

                _cache.Set("products", productsDto);

                return productsDto;
            }
        }


        public ProductDto? GetProduct(Guid id)
        {
            if (_cache.TryGetValue("products", out IEnumerable<ProductDto> products))
            {
                var product = products.FirstOrDefault(x => x.Id == id);
                if (product != null) return product;
            }

            using (_context)
            {
                var entitty = _context.Products.FirstOrDefault(x => x.Id == id);
                return _mapper.Map<ProductDto>(entitty);
            }
        }

        public Guid SaveProduct(ProductDto product)
        {
            if (product.Id == null)
                product.Id = Guid.NewGuid();

            using (_context)
            {
                if (!_context.Products.Any(x => x.Name.ToLower().Equals(product.Name.ToLower())))
                {
                    var entity = _mapper.Map<ProductEntity>(product);
                    _context.Products.Add(entity);
                    _context.SaveChanges();
                    _cache.Remove("products");
                    return entity.Id;
                }
            }
            return Guid.Empty;
        }

        public void SetPrice(Guid ProductId, decimal price)
        {
            using (_context)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
                if (product != null)
                {
                    product.Price = price;
                    _context.SaveChanges();
                    _cache.Remove("products");
                }
            }
        }

        public void AddCount(Guid ProductId, int addCount)
        {
            using (_context)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == ProductId);
                if (product != null && addCount > 0)
                {
                    product.Cost += (uint)addCount;
                    _context.SaveChanges();
                    _cache.Remove("products");
                }
            }
        }

        public void DeleteProduct(Guid productId)
        {
            using (_context)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == productId);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    _cache.Remove("products");
                }
            }
        }
        public string GetProductReport()
        {
            var products = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();

            return GetSCV(products);
        }

        private string GetSCV(IEnumerable<ProductDto> products)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var product in products)
            {
                sb.AppendLine(product.Name + ";" + product.Cost + "/" + product.Price + "\n");
            }
            return sb.ToString();
        }
    }
}
