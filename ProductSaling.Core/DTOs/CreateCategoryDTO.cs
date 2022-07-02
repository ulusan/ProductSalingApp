using System.ComponentModel.DataAnnotations;

namespace ProductSaling.Core.DTOs
{
    public class CreateCategoryDTO
    {
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Product Name Is Too Long")]
        public string CategoryName { get; set; }
    }
}
