namespace ProductSaling.Core.DTOs
{
    public class ProductDTO : CreateProductDTO
    {
        public int Id { get; set; }
        public CreateCategoryDTO Category { get; set; }
    }
}
