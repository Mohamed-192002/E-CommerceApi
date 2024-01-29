using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Repositories
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustomerBasket> CreateOrUpdatePayment(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];
            var basket = await _unitOfWork.BasketRepo.GetBasketAsync(BasketId);
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.DeliveryMethodRepo.GetAsync(x => x.Id == basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Price;
            }
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.ProductRepo.GetAsync(x => x.Id == item.Id);

                if (item.Price != productItem.Price)
                    item.Price = productItem.Price;
            }
            var services = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(basket.BasketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                intent = await services.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(basket.BasketItems.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100)
                };
                await services.UpdateAsync(basket.PaymentIntentId, options);

            }
            await _unitOfWork.BasketRepo.UpdateBasketAsync(basket);
            return basket;
        }
    }
}
