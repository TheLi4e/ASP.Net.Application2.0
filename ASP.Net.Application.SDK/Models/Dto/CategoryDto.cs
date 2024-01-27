namespace ASP.Net.Application.SDK.Models
{
    public class CategoryDto : BaseDto
    {
        public virtual List<ProductEntity?> Products { get; set; } = new List<ProductEntity>();
    }
}
