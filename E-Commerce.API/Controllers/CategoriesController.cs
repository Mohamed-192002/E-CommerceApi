using AutoMapper;
using ECommerce.Api.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("Get_All_Categories")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            if (categories is not null)
            {
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTOById>>(categories);
                return Ok(categoriesDto);
            }
            return BadRequest("Not Found");
        }
        [HttpGet("Get_Category_By_ID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepo.GetAsync(c => c.Id == id);

            if (category is not null)
            {
                var categoryDto = _mapper.Map<CategoryDTOById>(category);
                return Ok(categoryDto);
            }
            return BadRequest("Not Found");
        }
        [HttpPost("Add_New_Category")]
        public async Task<IActionResult> Add(CategoryDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapping
            var category = _mapper.Map<Category>(model);

            await _unitOfWork.CategoryRepo.AddAsync(category);
            return Ok(category);
        }
        [HttpPut("Edit_Category_By_ID/{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, CategoryDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var ExitingCategory = await _unitOfWork.CategoryRepo.GetAsync(c => c.Id == id);
            if (ExitingCategory is null)
                return BadRequest($"Category not found , Id {id} incorrect");
            // mapping
            _mapper.Map(model, ExitingCategory);

            await _unitOfWork.CategoryRepo.Update(id, ExitingCategory);
            return Ok(model);
        }

        [HttpDelete("Delete_Category_By_ID/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ExitingCategory = await _unitOfWork.CategoryRepo.GetAsync(c => c.Id == id);
            if (ExitingCategory is null)
                return BadRequest($"Category not found , Id {id} incorrect");
            await _unitOfWork.CategoryRepo.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
