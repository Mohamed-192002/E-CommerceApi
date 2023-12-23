using AutoMapper;
using AutoMapper.Execution;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;

namespace E_Commerce.API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductPicture))
            {
                return _config["ApiURL"] + source.ProductPicture;
            }
            return null;
        }
    }
}
