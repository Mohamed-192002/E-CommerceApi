﻿
namespace ECommerce.Core.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductPicture { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }  
    }
}
