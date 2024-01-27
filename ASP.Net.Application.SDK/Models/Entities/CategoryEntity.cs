namespace ASP.Net.Application.SDK.Models
{
    public class CategoryEntity : EntityBase
    {
        public virtual List<ProductEntity?> Products { get; set; } = new List<ProductEntity>();
    }
}
