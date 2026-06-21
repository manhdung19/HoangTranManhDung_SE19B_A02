using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.DAL.DAOs
{
    public class CategoryDAO
    {
        private static CategoryDAO _instance = null;
        private static readonly object _instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null) _instance = new CategoryDAO();
                    return _instance;
                }
            }
        }

        public List<Category> GetCategories(string searchString)
        {
            using var context = new FunewsManagementContext();
            var query = context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.CategoryName.Contains(searchString));
            }
            return query.ToList();
        }

        public Category GetCategoryById(short id)
        {
            using var context = new FunewsManagementContext();
            return context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public void AddCategory(Category category)
        {
            using var context = new FunewsManagementContext();
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            using var context = new FunewsManagementContext();
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public bool DeleteCategory(short id)
        {
            using var context = new FunewsManagementContext();
            // Logic của đề bài: Kiểm tra xem có bài News nào đang dùng Category này không
            bool hasNews = context.NewsArticles.Any(n => n.CategoryId == id);
            if (hasNews) return false; // Không cho xóa

            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
