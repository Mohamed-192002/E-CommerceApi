using ECommerce.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Config
{
    public class OrderItemConfigrution : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(x => x.ProductItemOrdered, n => { n.WithOwner(); });
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
