using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("Get_All_Products")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.ProductRepo.GetAllAsync(new[] { "Category" });
            if (products is not null)
            {
                var productsDto = _mapper.Map<IEnumerable<ProductDTOById>>(products);
                return Ok(productsDto);
            }
            return BadRequest("Not Found");
        }
        [HttpGet("Get_Product_By_ID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepo.GetAsync(p => p.Id == id, ["Category"]);

            if (product is not null)
            {
                var productDto = _mapper.Map<ProductDTOById>(product);
                return Ok(productDto);
            }
            return BadRequest("Not Found");
        }
        [HttpPost("Add_New_Product")]
        public async Task<IActionResult> Add([FromForm] CreateProductDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _unitOfWork.ProductRepo.AddAsync(model);
            return result ? Ok(model) : BadRequest();
        }
        //[HttpPut("Update_Product_By_ID")]
        //public async Task<IActionResult> Put([FromForm] UpdateProductDTO model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    var result = await _unitOfWork.ProductRepo.UpdateAsync(model);
        //    return result ? Ok(model) : BadRequest();


        //}
        [HttpPut("Edit_Product_By_ID")]
        public async Task<IActionResult> Put(UpdateProductDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _unitOfWork.ProductRepo.UpdateAsync(model);
                return result ? Ok(model) : BadRequest($"Category not found , Id {model.Id} incorrect");
        }
    }
}
