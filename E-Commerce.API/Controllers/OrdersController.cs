using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities.Orders;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWork, IOrderServices orderServices, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _orderServices = orderServices;
            _mapper = mapper;
        }

        [HttpPost("Create-Order")]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var address = _mapper.Map<AddressDTO, ShipAddress>(orderDTO.ShipToAddress);
            var order = await _orderServices.CreateOrderAsync(email, orderDTO.DeliveryMethodId, orderDTO.BasketId, address);
            if (order is null)
                return NotFound();
            return Ok(order);
        }
        [HttpGet("Get-Orders-For-User")]
        public async Task<IActionResult> GetOrdersForUser()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var orders = await _orderServices.GetOrdersForUserAsync(email);
            if (orders is null)
                return NotFound();
            var result = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderReturnDTO>>(orders);
            return Ok(orders);
        }
        [HttpGet("Get-Orders-By-Id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var orders = await _orderServices.GetOrderByIdAsync(id, email);
            if (orders is null)
                return NotFound();
            var result = _mapper.Map<Order, OrderReturnDTO>(orders);
            return Ok(result);
        }
        [HttpGet("Get-Delivery-Method")]
        public async Task<IActionResult> GetDeliveryMethod()
        {
            var deliveryMethod = await _orderServices.GetDeliveryMethodsAsync();
            if (deliveryMethod is null)
                return NotFound();
            return Ok(deliveryMethod);
        }
    }
}
