using AutoMapper;
using E_Commerce.API.Helpers;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;

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

        }

    }
}
