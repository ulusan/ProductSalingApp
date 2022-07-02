using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSaling.Data.Entities;

namespace ProductSaling.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new()
                {
                    ProductId = 1,
                    ProductName = "Süpürge",
                    CategoryId = 2,
                    UnitsInStock = 10,
                    UnitPrice = 1000
                },
                new()
                {
                    ProductId =2,
                    ProductName = "Masa",
                    CategoryId = 1,
                    UnitsInStock = 100,
                    UnitPrice = 300
                },
                new()
                {
                    ProductId = 3,
                    ProductName = "TV",
                    CategoryId = 3,
                    UnitsInStock = 5,
                    UnitPrice = 15000
                }
            );
        }
    }
}
