using TodoApp.DataAccess.Repositories;
using TodoApp.Models;
using TodoApp.Services.Interfaces;

namespace TodoApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Categories> _categoryRepository;

        public CategoryService(IRepository<Categories> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Categories> GetAllCategories(int userId)
        {
            return _categoryRepository.FindByCondition(c => c.UserId == userId).ToList();
        }

        public Categories? GetCategoryById(int id, int userId)
        {
            return _categoryRepository.FindByCondition(c => c.Id == id && c.UserId == userId).FirstOrDefault();
        }

        public Categories CreateCategory(Categories category)
        {
            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
            return category;
        }

        public void UpdateCategory(Categories category, int userId)
        {
            var existingCategory = GetCategoryById(category.Id, userId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }
            existingCategory.Name = category.Name;
            _categoryRepository.Update(existingCategory);
            _categoryRepository.SaveChanges();
        }

        public void DeleteCategory(int id, int userId)
        {
            var category = GetCategoryById(id, userId);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                _categoryRepository.SaveChanges();
            }
        }
    }
}
