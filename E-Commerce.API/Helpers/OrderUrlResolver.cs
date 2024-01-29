using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Orders;

namespace E_Commerce.API.Helpers
{
    public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _config;

        public OrderUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            {
                return _config["ApiURL"] + source.ProductItemOrdered.PictureUrl;
            }
            return null;
        }
    }
}
