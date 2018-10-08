using System;
using System.Collections.Generic;
using scrimp.Entities;

namespace scrimp.Services
{
    public class CategoryService : ICategoryService
    {
        private DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public Category CreateUserCategory(int id, Category account)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetUserCategories(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
