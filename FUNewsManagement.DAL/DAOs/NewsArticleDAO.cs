using FUNewsManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagement.DAL.DAOs
{
    public class NewsArticleDAO
    {
        private static NewsArticleDAO _instance = null;
        private static readonly object _instanceLock = new object();
        private NewsArticleDAO() { }
        public static NewsArticleDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null) _instance = new NewsArticleDAO();
                    return _instance;
                }
            }
        }

        // Lấy danh sách thống kê báo cáo theo ngày (Sắp xếp giảm dần)
        public List<NewsArticle> GetReportStatistics(DateTime? startDate, DateTime? endDate)
        {
            using var context = new FunewsManagementContext();
            var query = context.NewsArticles
                .Include(n => n.CreatedBy) // Nối bảng SystemAccount lấy tên
                .Include(n => n.Category)  // Nối bảng Category lấy tên
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(n => n.CreatedDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                // Cộng thêm 1 ngày để lấy trọn vẹn dữ liệu của ngày EndDate
                query = query.Where(n => n.CreatedDate < endDate.Value.AddDays(1));
            }

            return query.OrderByDescending(n => n.CreatedDate).ToList();
        }

        public List<NewsArticle> GetNewsArticles(string searchString)
        {
            using var context = new FunewsManagementContext();
            var query = context.NewsArticles
                .Include(n => n.CreatedBy)
                .Include(n => n.Category)
                .Include(n => n.Tags) // Bắt buộc Include để lấy Tags
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(n => n.NewsTitle.Contains(searchString));
            }
            return query.ToList();
        }

        public NewsArticle GetNewsArticleById(string id)
        {
            using var context = new FunewsManagementContext();
            return context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefault(n => n.NewsArticleId == id);
        }

        public void AddNewsArticle(NewsArticle article, List<int> tagIds)
        {
            using var context = new FunewsManagementContext();
            // Lọc các Tag được chọn và gắn vào Article
            if (tagIds != null && tagIds.Any())
            {
                var tags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                article.Tags = tags;
            }
            context.NewsArticles.Add(article);
            context.SaveChanges();
        }

        public void UpdateNewsArticle(NewsArticle article, List<int> tagIds)
        {
            using var context = new FunewsManagementContext();
            var existingArticle = context.NewsArticles
                .Include(n => n.Tags)
                .FirstOrDefault(n => n.NewsArticleId == article.NewsArticleId);

            if (existingArticle != null)
            {
                existingArticle.NewsTitle = article.NewsTitle;
                existingArticle.Headline = article.Headline;
                existingArticle.NewsContent = article.NewsContent;
                existingArticle.NewsSource = article.NewsSource;
                existingArticle.CategoryId = article.CategoryId;
                existingArticle.NewsStatus = article.NewsStatus;
                existingArticle.UpdatedById = article.UpdatedById;
                existingArticle.ModifiedDate = DateTime.Now;

                // Xóa Tags cũ, cập nhật Tags mới
                existingArticle.Tags.Clear();
                if (tagIds != null && tagIds.Any())
                {
                    var tags = context.Tags.Where(t => tagIds.Contains(t.TagId)).ToList();
                    foreach (var tag in tags)
                    {
                        existingArticle.Tags.Add(tag);
                    }
                }
                context.SaveChanges();
            }
        }

        public void DeleteNewsArticle(string id)
        {
            using var context = new FunewsManagementContext();
            var article = context.NewsArticles.Include(n => n.Tags).FirstOrDefault(n => n.NewsArticleId == id);
            if (article != null)
            {
                article.Tags.Clear(); // Xóa khóa ngoại nhiều-nhiều trước
                context.NewsArticles.Remove(article);
                context.SaveChanges();
            }
        }

    }
}
