using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerce.Core.DTO
{
    public class ProductDTO
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,1000,ErrorMessage ="Price limited by {0} and {1}")]
        [RegularExpression(@"[0-9]*\.?[0-9]",ErrorMessage ="{0} must be number!")]
        public decimal Price { get; set; }
    }
    public class ProductDTOById : ProductDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }

    }
    public class CreateProductDTO : ProductDTO
    {
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }
    public class UpdateProductDTO:ProductDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }    
    }
}
