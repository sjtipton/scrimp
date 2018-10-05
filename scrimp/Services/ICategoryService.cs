using scrimp.Entities;
using System.Collections.Generic;

namespace scrimp.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetUserCategories(int id);
        Category GetById(int id);
        Category CreateUserCategory(int id, Category account);
        void Update(int id, Category category);
        void Delete(int id);
    }
}
