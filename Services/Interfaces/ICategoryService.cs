using TodoApp.Models;

namespace TodoApp.Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Categories> GetAllCategories(int userId);
        Categories? GetCategoryById(int id, int userId);
        Categories CreateCategory(Categories category);
        void UpdateCategory(Categories category, int userId);
        void DeleteCategory(int id, int userId);
    }
}
