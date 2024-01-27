namespace ASP.Net.Application.SDK.Models
{
    public class ProductEntity : EntityBase
    {
        public uint Cost { get; set; }
        public decimal Price { get; set; }
        public virtual CategoryDto? Category { get; set; }
        public virtual StorageEntity? Storage { get; set; }
    }
}
