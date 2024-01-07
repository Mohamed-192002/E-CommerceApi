using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("Get-Basket-Item/{id}")]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var basket = await _unitOfWork.BasketRepo.GetBasketAsync(id);
            if (basket is null)
            return NotFound();
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost("Update-Basket-Item")]
        public async Task<IActionResult> UpdateBasketById(CustomerBasketDTO oldBasket)
        {
            var result = _mapper.Map<CustomerBasketDTO, CustomerBasket>(oldBasket);
            var basket = await _unitOfWork.BasketRepo.UpdateBasketAsync(result);
            return Ok(basket);
        }
        [HttpDelete("Delete-Basket-Item/{id}")]
        public async Task<IActionResult> DeleteBasketById(string id)
        {
            var basket = await _unitOfWork.BasketRepo.DeleteBasketAsync(id);
            return Ok(basket);
        }
    }
}
