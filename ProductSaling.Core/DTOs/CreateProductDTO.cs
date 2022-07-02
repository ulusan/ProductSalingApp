using System.ComponentModel.DataAnnotations;

namespace ProductSaling.Core.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Category Name Is Too Long")]
        public string Name { get; set; }

    }
}
