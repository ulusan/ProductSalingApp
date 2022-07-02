using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSaling.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSaling.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
                new()
                {
                    CustomerId = 1,
                    CompanyName = "Konuşarak Öğren",
                    City ="İstanbul",
                    ContactName = "05055050505"
                },
                new()
                {
                    CustomerId = 2,
                    CompanyName = "HepsiBurada",
                    City = "İstanbul",
                    ContactName = "05495499494"
                },
                new()
                {
                    CustomerId = 3,
                    CompanyName = "Trendyol",
                    City = "İstanbul",
                    ContactName = "05325323232"
                }
            );
        }
    }
}
