using FUNewsManagement.BLL.Repositories.Implementations;
using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services.Implementations
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsRepository;

        public NewsArticleService()
        {
            _newsRepository = new NewsArticleRepository();
        }

        public List<NewsArticle> GetReportStatistics(DateTime? startDate, DateTime? endDate)
        {
            return _newsRepository.GetReportStatistics(startDate, endDate);
        }
        public List<NewsArticle> GetNewsArticles(string searchString)
            => _newsRepository.GetNewsArticles(searchString);
        public NewsArticle GetNewsArticleById(string id)
            => _newsRepository.GetNewsArticleById(id);
        public void AddNewsArticle(NewsArticle article, List<int> tagIds)
            => _newsRepository.AddNewsArticle(article, tagIds);
        public void UpdateNewsArticle(NewsArticle article, List<int> tagIds)
            => _newsRepository.UpdateNewsArticle(article, tagIds);
        public void DeleteNewsArticle(string id)
            => _newsRepository.DeleteNewsArticle(id);
    }
}
