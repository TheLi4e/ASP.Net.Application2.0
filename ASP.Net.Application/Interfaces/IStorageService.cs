using ASP.Net.Application.SDK.Models;

namespace ASP.Net.Application.Interfaces
{
    public interface IStorageService
    {
        IEnumerable<StorageDto> GetStorages();
        StorageDto? GetStorage(Guid id);
        Guid SaveCategory(StorageDto storage);
        bool AddProduct(Guid storageId, Guid productId);
        bool DeleteStorage(Guid storageId);
    }
}
