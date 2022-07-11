using Ecommerce.Services.Catalog.APIBusiness.Abstract;
using Ecommerce.Services.Catalog.APIEntities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.Catalog.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryService.DeleteAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

    }
}
