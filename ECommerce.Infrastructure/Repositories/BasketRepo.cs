using AutoMapper;
using ECommerce.Core.DTO;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerce.Infrastructure.Repositories
{
    public class BasketRepo(IConnectionMultiplexer redis) : IBasketRepo
    {
        private readonly IDatabase _database = redis.GetDatabase();


        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            bool _isKeyExist = await _database.KeyExistsAsync(BasketId);
            return _isKeyExist && await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var data = await _database.StringGetAsync(BasketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            _ = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket)
                , TimeSpan.FromDays(30)
                );
            return await GetBasketAsync(customerBasket.Id);

        }
    }
}
