using ASP.Net.Application.SDK;
using ASP.Net.Application.SDK.Models;
using AutoMapper;

namespace StorageApi.Services
{
    public interface IStorageService
    {
        IEnumerable<ProductDto> GetProducts(Guid storageId);
    }
    public class StorageService : IStorageService
    {
        private readonly IMapper _mapper;
            private readonly AppDbContext _context;
        public StorageService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<ProductDto> GetProducts(Guid storageId)
        {
            var products = new List<ProductDto>();
            using (_context)
            {
                products.AddRange(_context.Storages.FirstOrDefault(x => x.Id == storageId).Products
                    .Select(x => _mapper.Map<ProductDto>(x))
                    .ToList());
                return products;
            }
        }
    }
}
