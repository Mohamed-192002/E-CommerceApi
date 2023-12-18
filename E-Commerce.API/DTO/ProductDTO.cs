using ECommerce.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.DTO
{
    public class ProductDTO
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class ProductDTOById : ProductDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
    public class CreateProductDTO : ProductDTO
    {
        public int CategoryId { get; set; }

    }
}
