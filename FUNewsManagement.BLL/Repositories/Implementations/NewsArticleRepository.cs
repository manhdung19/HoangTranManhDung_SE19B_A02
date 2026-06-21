using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Implementations
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public List<NewsArticle> GetReportStatistics(DateTime? startDate, DateTime? endDate)
            => NewsArticleDAO.Instance.GetReportStatistics(startDate, endDate);

        public List<NewsArticle> GetNewsArticles(string searchString)
            => NewsArticleDAO.Instance.GetNewsArticles(searchString);
        public NewsArticle GetNewsArticleById(string id)
            => NewsArticleDAO.Instance.GetNewsArticleById(id);
        public void AddNewsArticle(NewsArticle article, List<int> tagIds)
            => NewsArticleDAO.Instance.AddNewsArticle(article, tagIds);
        public void UpdateNewsArticle(NewsArticle article, List<int> tagIds)
            => NewsArticleDAO.Instance.UpdateNewsArticle(article, tagIds);
        public void DeleteNewsArticle(string id)
            => NewsArticleDAO.Instance.DeleteNewsArticle(id);
    }
}
