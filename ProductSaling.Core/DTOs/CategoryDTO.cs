namespace ProductSaling.Core.DTOs
{
    public class CategoryDTO : CreateCategoryDTO
    {
        public int Id { get; set; }
        public IList<ProductDTO> Products { get; set; }
    }

}
