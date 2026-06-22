using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages.Lecturer
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public IndexModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IList<FUNewsManagement.DAL.Models.NewsArticle> LecturerNews { get; set; } = new List<FUNewsManagement.DAL.Models.NewsArticle>();

        public IActionResult OnGet()
        {
            // Bảo mật: Kiểm tra xem người dùng có phải là Lecturer (Role = 2) hay không?
            int? role = HttpContext.Session.GetInt32("UserRole");
            if (role == null || role != 2)
            {
                // Nếu chưa đăng nhập hoặc không phải Giảng viên, đá văng ra trang Home
                return RedirectToPage("/Home");
            }

            // Nếu đúng là Lecturer, lấy danh sách bài viết Active (giống hệt trang Public)
            var allNews = _newsArticleService.GetNewsArticles("");
            LecturerNews = allNews.Where(n => n.NewsStatus == true)
                                  .OrderByDescending(n => n.CreatedDate)
                                  .ToList();

            return Page();
        }
    }
}
