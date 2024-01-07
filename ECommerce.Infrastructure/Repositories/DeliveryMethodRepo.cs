using ECommerce.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class DeliveryMethodRepo : GenericRepo<DeliveryMethod>, IDeliveryMethodRepo
    {
        public DeliveryMethodRepo(AppDbContext context) : base(context)
        {
        }
    }
}
