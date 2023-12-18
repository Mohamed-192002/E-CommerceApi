using AutoMapper;
using ECommerce.Api.DTO;
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
                .ReverseMap();

            CreateMap<CreateProductDTO, Product>().ReverseMap();


        }

    }
}
