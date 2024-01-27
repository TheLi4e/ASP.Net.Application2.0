using ASP.Net.Application.SDK.Models;
using AutoMapper;

namespace ASP.Net.Application
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<ProductEntity, ProductDto>(MemberList.Destination).ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>(MemberList.Destination).ReverseMap();
            CreateMap<StorageEntity, StorageDto>(MemberList.Destination).ReverseMap();
        }
    }
}
