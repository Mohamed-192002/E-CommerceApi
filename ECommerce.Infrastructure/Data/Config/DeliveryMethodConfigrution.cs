using ECommerce.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Data.Config
{
    public class DeliveryMethodConfigrution : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.HasData(
                new DeliveryMethod { Id = 1, ShortName = "DHL", Description = "Fasted Delivery Time", Price = 20 },
                new DeliveryMethod { Id = 2, ShortName = "Aramex", Description = "Get it with 3 days", Price = 10 },
                new DeliveryMethod { Id = 3, ShortName = "Fedex", Description = "Slower but cheap", Price = 5 },
                new DeliveryMethod { Id = 4, ShortName = "Jumia", Description = "Free", Price = 0 }
                );


        }
    }
}
