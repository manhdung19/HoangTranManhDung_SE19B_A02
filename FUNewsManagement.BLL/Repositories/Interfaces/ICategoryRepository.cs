using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories(string searchString);
        Category GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        bool DeleteCategory(short id);
    }
}
