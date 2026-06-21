using FUNewsManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly INewsArticleService _newsService;

        public ReportController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            // Bảo vệ trang: Chỉ Admin (Role=0) mới được xem
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 0) return RedirectToAction("Login", "Auth");

            // Đưa ngày trở lại View để giữ trên ô Input
            ViewData["StartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = endDate?.ToString("yyyy-MM-dd");

            var reportData = _newsService.GetReportStatistics(startDate, endDate);
            return View(reportData);
        }
    }
}
