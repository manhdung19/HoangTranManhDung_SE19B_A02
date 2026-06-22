using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages.Profile
{
    public class HistoryModel : PageModel
    {
        private readonly INewsArticleService _newsService;

        public HistoryModel(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public IList<FUNewsManagement.DAL.Models.NewsArticle> MyArticles { get; set; } = default!;

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            // Lấy toàn bộ bài viết, dùng LINQ để lọc ra những bài do người này tạo
            var allArticles = _newsService.GetNewsArticles("");
            MyArticles = allArticles.Where(a => a.CreatedById == userId)
                                    .OrderByDescending(a => a.CreatedDate)
                                    .ToList();

            return Page();
        }
    }
}
