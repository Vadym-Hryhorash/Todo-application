using Models;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Categories> GetAllCategories();
        Categories? GetCategoryById(int id);
        Categories CreateCategory(Categories category);
        void UpdateCategory(Categories category);
        void DeleteCategory(int id);
    }
}
