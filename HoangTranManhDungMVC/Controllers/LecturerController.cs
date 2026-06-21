using FUNewsManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class LecturerController : Controller
    {
        private readonly INewsArticleService _newsService;

        public LecturerController(INewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            // Kiểm tra phân quyền: Chỉ Role = 2 (Lecturer) mới được vào
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 2) return RedirectToAction("Login", "Auth");

            // Lấy danh sách tin Active y hệt như Public
            var activeNews = _newsService.GetNewsArticles("")
                                         .Where(n => n.NewsStatus == true)
                                         .OrderByDescending(n => n.CreatedDate)
                                         .ToList();
            return View(activeNews);
        }
    }
}
