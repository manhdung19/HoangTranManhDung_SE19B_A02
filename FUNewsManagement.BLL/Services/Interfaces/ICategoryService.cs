using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetCategories(string searchString);
        Category GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        bool DeleteCategory(short id);
    }
}
