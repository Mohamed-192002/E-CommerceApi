using AutoMapper;
using E_Commerce.API.Helpers;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Orders;
using StackExchange.Redis;
using Order = ECommerce.Core.Entities.Orders.Order;

namespace ECommerce.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryDTOById, Category>().ReverseMap();

            // Product
            CreateMap<Product, ProductDTOById>()
                .ForMember(p => p.CategoryName, o => o.MapFrom(c => c.Category.Name))
                .ForMember(p => p.Image, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
            CreateMap<Product, UpdateProductDTO>()
               .ReverseMap();

            CreateMap<CreateProductDTO, Product>().ReverseMap();

            //User
            CreateMap<Address, AddressDTO>().ReverseMap();


            // Basket
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();

            // Order
            CreateMap<ShipAddress, AddressDTO>().ReverseMap();
            CreateMap<Order, OrderReturnDTO>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(c => c.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(c => c.DeliveryMethod.Price))
                .ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductItemId, o => o.MapFrom(c => c.Id))
                .ForMember(d => d.ProductItemName, o => o.MapFrom(c => c.ProductItemOrdered.ProductItemName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(c => c.ProductItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderUrlResolver>())

                .ReverseMap();


        }

    }
}
