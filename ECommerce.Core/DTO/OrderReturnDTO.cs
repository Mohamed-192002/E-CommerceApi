using ECommerce.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTO
{
    public class OrderReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } 
        public ShipAddress ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public IReadOnlyList<OrderItemDTO> OrderItems { get; set; }
    }
}
