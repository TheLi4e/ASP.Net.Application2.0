using System.Reflection;
using ASP.Net.Application.SDK.Models;

namespace ASP.Net.GateWay.Query
{
    public class GetProducts
    {
        public List<ProductEntity> GetProductsOnStorqage([Service] IStorageService repository, Guid storageId) => repository.GetProducts(storageId);
    }
}
