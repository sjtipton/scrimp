using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET api/users/:userid/categories
        [HttpGet]
        [Route("users/{userid}/categories")]
        public IActionResult GetUserCategories(int userid)
        {
            var userCategories = _categoryService.GetUserCategories(userid);
            var userCategoryDtos = _mapper.Map<CategoryDto>(userCategories);
            return Ok(userCategoryDtos);
        }

        // POST api/users/:userid/categories
        [HttpPost]
        [Route("users/{userid}/categories")]
        public IActionResult CreateUserCategory(int userid, [FromBody]CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            try
            {
                _categoryService.CreateUserCategory(userid, category);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/categories/:id
        [HttpGet("categories/{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        // PUT api/categories/:id
        [HttpPut("categories/{id}")]
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

        // DELETE api/categories/:id
        [HttpDelete("categories/{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}
