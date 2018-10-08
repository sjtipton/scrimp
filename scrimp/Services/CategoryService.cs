using System.Collections.Generic;
using System.Linq;
using scrimp.Entities;
using scrimp.Helpers;

namespace scrimp.Services
{
    public class CategoryService : ICategoryService
    {
        private DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public Category CreateUserCategory(int userId, Category category)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
                throw new AppException("User not found. Cannot create an Category.");

            category.UserId = user.Id;

            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetUserCategories(int userId)
        {
            return _context.Categories.Where(x => x.UserId == userId).ToList();
        }

        public void Update(Category categoryParam)
        {
            var category = _context.Categories.Find(categoryParam.Id);

            if (category == null)
                throw new AppException("Category not found. Cannot update an Category.");

            category.Name = categoryParam.Name;
            category.Color = categoryParam.Color;
            category.ParentId = categoryParam.ParentId;
            category.IsTransfer = categoryParam.IsTransfer;

            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
