using ECommerce.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTO
{
    public class OrderDTO
    {
        public string BasketId { get; set; }
        public AddressDTO ShipToAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
