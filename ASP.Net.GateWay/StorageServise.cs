using ASP.Net.Application.SDK;
using ASP.Net.Application.SDK.Models;

namespace ASP.Net.GateWay
{
    public interface IStorageService
    {
        List<ProductEntity> GetProducts(Guid storageId);
    }
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _context;
        public StorageService(AppDbContext context)
        {
            _context = context;
        }

        public List<ProductEntity> GetProducts(Guid storageId)
        {
            var products = new List<ProductEntity>();
            using (_context)
            {
                products.AddRange(_context.Storages.FirstOrDefault(x => x.Id == storageId).Products.ToList());
                return products;
            }
        }
    }
}
