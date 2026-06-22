using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages
{
    public class NewsDetailModel : PageModel
    {
        private readonly INewsArticleService _newsService;

        public NewsDetailModel(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public FUNewsManagement.DAL.Models.NewsArticle NewsArticle { get; set; } = default!;

        public IActionResult OnGet(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var article = _newsService.GetNewsArticleById(id);
            if (article == null || article.NewsStatus == false)
            {
                return NotFound(); // Chặn xem nếu tin bị ẩn hoặc không tồn tại
            }

            NewsArticle = article;
            return Page();
        }
    }
}
