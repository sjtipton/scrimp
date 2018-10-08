using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;

namespace scrimp.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET users/:id/categories
        [HttpGet("users/:id/categories")]
        public IActionResult GetUserCategories(int id)
        {
            var userCategories = _categoryService.GetUserCategories(id);
            var userCategoryDtos = _mapper.Map<CategoryDto>(userCategories);
            return Ok(userCategoryDtos);
        }

        // POST users/:id/categories
        [HttpPost("users/:id/categories")]
        public IActionResult CreateUserCategory(int id, [FromBody]CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            try
            {
                _categoryService.CreateUserCategory(id, category);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET categories/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        // PUT categories/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.Id = id;

            try
            {
                _categoryService.Update(category);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE categories/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}
