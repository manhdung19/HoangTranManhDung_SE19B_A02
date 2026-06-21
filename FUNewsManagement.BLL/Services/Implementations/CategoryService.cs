using FUNewsManagement.BLL.Repositories.Implementations;
using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public List<Category> GetCategories(string searchString) => _categoryRepository.GetCategories(searchString);
        public Category GetCategoryById(short id) => _categoryRepository.GetCategoryById(id);
        public void AddCategory(Category category) => _categoryRepository.AddCategory(category);
        public void UpdateCategory(Category category) => _categoryRepository.UpdateCategory(category);
        public bool DeleteCategory(short id) => _categoryRepository.DeleteCategory(id);
    }
}
