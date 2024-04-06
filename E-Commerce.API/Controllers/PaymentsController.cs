using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentsController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }
        [HttpPost("Create-And-Update-Payment/{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateAndUpdatePaymentIntent(string basketId)
        {
            return await _paymentServices.CreateOrUpdatePayment(basketId);
        }
    }
}
