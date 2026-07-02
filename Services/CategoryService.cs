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

        public IEnumerable<Categories> GetAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public Categories? GetCategoryById(int id)
        {
            return _categoryRepository.FindByCondition(c => c.Id == id).FirstOrDefault();
        }

        public Categories CreateCategory(Categories category)
        {
            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
            return category;
        }

        public void UpdateCategory(Categories category)
        {
            _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                _categoryRepository.SaveChanges();
            }
        }
    }
}
