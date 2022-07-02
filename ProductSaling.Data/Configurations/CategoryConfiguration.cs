using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSaling.Data.Entities;

namespace ProductSaling.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new()
                {
                    CategoryId = 1,
                    CategoryName = "Furniture",
                },
                new()
                {
                    CategoryId = 2,
                    CategoryName = "Small Home Appliances",
                },
                new()
                {
                    CategoryId = 3,
                    CategoryName = "Technological tools",
                }
            );
            
        }
    }
}
