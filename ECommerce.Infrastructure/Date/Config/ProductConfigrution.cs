using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Date.Config
{
    internal class ProductConfigrution : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
            builder.HasData(
                new Product { Id = 1, Name = "Product 1", Price = 2000, Description = "P1", CategoryId = 1 },
                new Product { Id = 2, Name = "Product 2", Price = 3000, Description = "P2", CategoryId = 1 },
                new Product { Id = 3, Name = "Product 3", Price = 2500, Description = "P3", CategoryId = 2 },
                new Product { Id = 4, Name = "Product 4", Price = 4000, Description = "P4", CategoryId = 3 }
                );

        }
    }
}
