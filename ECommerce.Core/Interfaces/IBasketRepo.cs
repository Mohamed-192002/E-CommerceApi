

using ECommerce.Core.DTO;
using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces
{
    public interface IBasketRepo
    {
        Task<CustomerBasket> GetBasketAsync(string BasketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string BasketId);

    }
}
