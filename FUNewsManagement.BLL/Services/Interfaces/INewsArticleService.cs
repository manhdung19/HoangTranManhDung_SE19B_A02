using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services.Interfaces
{
    public interface INewsArticleService
    {
        List<NewsArticle> GetReportStatistics(DateTime? startDate, DateTime? endDate);

        List<NewsArticle> GetNewsArticles(string searchString);
        NewsArticle GetNewsArticleById(string id);
        void AddNewsArticle(NewsArticle article, List<int> tagIds);
        void UpdateNewsArticle(NewsArticle article, List<int> tagIds);
        void DeleteNewsArticle(string id);
    }
}
