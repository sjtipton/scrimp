using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;
using System.Collections.Generic;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
    public class CategoriesController : ControllerBase
    {
        private IUserService _userService;
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoriesController(IUserService userService, ICategoryService categoryService, IMapper mapper)
        {
            _userService = userService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET api/users/:id/categories
        [HttpGet]
        [Route("users/{id}/categories")]
        public IActionResult GetUserCategories(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user is User)
            {
                var userCategories = _categoryService.GetUserCategories(id);
                var userCategoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(userCategories);
                return Ok(userCategoryDtos);
            }
            return BadRequest("The user is not valid.");
        }

        // POST api/users/:id/categories
        [HttpPost]
        [Route("users/{id}/categories")]
        public IActionResult CreateUserCategory(int id, [FromBody]CategoryDto categoryDto)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user is User)
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
            return BadRequest("The user is not valid.");
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
