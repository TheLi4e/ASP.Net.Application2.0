using ASP.Net.Application.Interfaces;
using ASP.Net.Application.SDK.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.Net.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly SDK.AppDbContext _context;

        public StorageService(SDK.AppDbContext context, IMemoryCache cache, IMapper mapper)
        {
            _cache = cache;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            if (_cache.TryGetValue("categories", out IEnumerable<StorageDto> storages))
                return storages;

            using (_context)
            {
                var storagesDto = _context.Storages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
                _cache.Set("storages", storagesDto);

                return storagesDto;
            }
        }

        public StorageDto? GetStorage(Guid id)
        {
            if (_cache.TryGetValue("storages", out IEnumerable<StorageDto> storages))
            {
                var storage = storages.FirstOrDefault(x => x.Id == id);
                if (storage != null) return storage;
            }

            using (_context)
            {
                var storage = _context.Storages.FirstOrDefault(x => x.Id == id);

                return _mapper.Map<StorageDto>(storage);
            }
        }

        public Guid SaveCategory(StorageDto storage)
        {
            using (_context)
            {
                if (storage.Id == null)
                    storage.Id = Guid.NewGuid();
                if (!_context.Storages.Any(x => x.Name.ToLower().Equals(storage.Name.ToLower())))
                {
                    var entity = _mapper.Map<StorageEntity>(storage);
                    _context.Storages.Add(entity);
                    _context.SaveChanges();
                    _cache.Remove("storages");

                    return entity.Id;
                }
                return Guid.Empty;
            }
        }

        public bool AddProduct(Guid storageId, Guid productId)
        {
            using (_context)
            {
                var category = _context.Storages.FirstOrDefault(x => x.Id == storageId);
                var product = _context.Products.FirstOrDefault(x => x.Id == productId);
                if (category != null && product != null)
                {

                    _context.Storages.SingleOrDefault(x => x.Products.Select(x => x.Id).Contains(productId))?.Products.Remove(product);

                    category.Products.Add(product);
                    _context.SaveChanges();

                    _cache.Remove("storages");
                    _cache.Remove("products");

                    return true;
                }
                return false;
            }
        }

        public bool DeleteStorage(Guid storageId)
        {
            using (_context)
            {
                var storage = _context.Storages.FirstOrDefault(x => x.Id == storageId);
                if (storage != null)
                {
                    _context.Storages.Remove(storage);

                    _context.SaveChanges();

                    _cache.Remove("storages");
                    _cache.Remove("products");

                    return true;
                }
                return false;
            }
        }
    }
}
