using ECommerce.Core.Entities.Orders;
using ECommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShipAddress shipAddress)
        {
            var basket = await _unitOfWork.BasketRepo.GetBasketAsync(basketId);
            var Items = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.ProductRepo.GetAsync(x => x.Id == item.Id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.ProductPicture);
                var orderItem = new OrderItem(productItemOrdered, item.Price, item.Quantity);
                Items.Add(orderItem);
            }
            var deliveryMethod = await _unitOfWork.DeliveryMethodRepo.GetAsync(x => x.Id == deliveryMethodId);
            var subTotal = Items.Sum(x => x.Price * x.Quantity);
            var order = new Order(buyerEmail, shipAddress, deliveryMethod, Items, subTotal);
            if (order is null)
                return null;
            // add order in database
            await _unitOfWork.OrderRepo.AddAsync(order);

            // remove basket items
            await _unitOfWork.BasketRepo.DeleteBasketAsync(basketId);
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => (IReadOnlyList<DeliveryMethod>)await _unitOfWork.DeliveryMethodRepo.GetAllAsync();

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        => await _unitOfWork.OrderRepo.GetAsync(x => x.Id == id && x.BuyerEmail == buyerEmail, ["DeliveryMethod", "OrderItems"]);

        public async Task<IEnumerable<Order>> GetOrdersForUserAsync(string buyerEmail)
                => await _unitOfWork.OrderRepo.GetAllAsync(["DeliveryMethod", "OrderItems"]);

    }
}
