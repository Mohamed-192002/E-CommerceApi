using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTO
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }  
        public string ProductPicture { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}
