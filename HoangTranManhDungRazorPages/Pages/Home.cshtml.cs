using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages
{
    public class HomeModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public HomeModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        // Biến chứa danh sách tin tức sẽ đẩy ra HTML
        public IList<FUNewsManagement.DAL.Models.NewsArticle> ActiveNews { get; set; } = new List<FUNewsManagement.DAL.Models.NewsArticle>();


        public void OnGet()
        {
            // Lấy toàn bộ bài viết, lọc ra những bài có Status = true và sắp xếp mới nhất lên đầu
            var allNews = _newsArticleService.GetNewsArticles("");
            ActiveNews = allNews.Where(n => n.NewsStatus == true)
                                .OrderByDescending(n => n.CreatedDate)
                                .ToList();
        }
    }
}
