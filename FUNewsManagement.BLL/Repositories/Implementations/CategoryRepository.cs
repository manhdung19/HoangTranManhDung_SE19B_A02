using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories(string searchString) => CategoryDAO.Instance.GetCategories(searchString);
        public Category GetCategoryById(short id) => CategoryDAO.Instance.GetCategoryById(id);
        public void AddCategory(Category category) => CategoryDAO.Instance.AddCategory(category);
        public void UpdateCategory(Category category) => CategoryDAO.Instance.UpdateCategory(category);
        public bool DeleteCategory(short id) => CategoryDAO.Instance.DeleteCategory(id);
    }
}
