using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities.Orders
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

        }
        public Order(string buyerEmail, ShipAddress shipToAddress
            , DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public ShipAddress ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        decimal TotalPrice => SubTotal + DeliveryMethod.Price;



    }
}
