using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsService;

        public IndexModel(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public IList<FUNewsManagement.DAL.Models.NewsArticle> ReportData { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public IActionResult OnGet()
        {
            // Bảo vệ trang: Chỉ Admin (Role=0) mới được xem
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role == null || role != 0) return RedirectToPage("/Login");

            ReportData = _newsService.GetReportStatistics(StartDate, EndDate);
            return Page();
        }
    }
}
