using ECommerce.Core.Entities.Orders;

namespace ECommerce.Core.DTO
{
    public class OrderItemDTO
    {
        public int ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}