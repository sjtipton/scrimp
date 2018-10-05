using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Services;
using System;

namespace scrimp.Controllers
{
    [ApiController]
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

        // GET users/:id/categories
        [HttpGet("users/:id/categories")]
        public IActionResult GetUserCategories(int id)
        {
            throw new NotImplementedException();
        }

        // POST users/:id/categories
        [HttpPost("users/:id/categories")]
        public IActionResult CreateUserCategory(int id, [FromBody]CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        // GET categories/:id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        // PUT categories/:id
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        // DELETE categories/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
