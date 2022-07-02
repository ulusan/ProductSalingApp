namespace ProductSaling.Core.DTOs
{
    public class UpdateCategoryDTO : CreateCategoryDTO
    {
        public IList<CreateProductDTO> Products { get; set; }

    }
}
