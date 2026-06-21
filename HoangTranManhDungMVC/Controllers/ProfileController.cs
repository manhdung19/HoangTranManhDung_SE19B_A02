using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ISystemAccountService _accountService;
        private readonly INewsArticleService _newsService;

        public ProfileController(ISystemAccountService accountService, INewsArticleService newsService)
        {
            _accountService = accountService;
            _newsService = newsService;
        }

        // --- TÍNH NĂNG 1: QUẢN LÝ PROFILE ---
        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            // Yêu cầu Staff (Role = 1)
            if (userId == null || role != 1) return RedirectToAction("Login", "Auth");

            var account = _accountService.GetSystemAccountById((short)userId);
            return View(account);
        }

        [HttpPost]
        public IActionResult Index(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                account.AccountId = (short)userId; // Ép cứng ID để không bị đổi ID
                account.AccountRole = 1; // Ép cứng Role Staff

                _accountService.UpdateSystemAccount(account);

                // Cập nhật lại Tên hiển thị trong Session nếu họ đổi tên
                HttpContext.Session.SetString("UserName", account.AccountName ?? "User");

                ViewBag.SuccessMessage = "Cập nhật thông tin thành công!";
            }
            return View(account);
        }

        // --- TÍNH NĂNG 2: XEM LỊCH SỬ BÀI VIẾT (HISTORY) ---
        public IActionResult History()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Auth");

            // Lấy toàn bộ bài viết, dùng LINQ để lọc ra những bài do người này tạo
            var allArticles = _newsService.GetNewsArticles("");
            var myArticles = allArticles.Where(a => a.CreatedById == userId)
                                        .OrderByDescending(a => a.CreatedDate)
                                        .ToList();

            return View(myArticles);
        }
    }
}
