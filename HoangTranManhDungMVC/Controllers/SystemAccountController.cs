using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly ISystemAccountService _accountService;

        public SystemAccountController(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        // Action hiển thị danh sách tài khoản (hỗ trợ tìm kiếm)
        public IActionResult Index(string searchString)
        {
            // Kiểm tra phân quyền đơn giản bằng Session (0 là Admin)
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 0)
            {
                return RedirectToAction("Login", "Auth");
            }
            // Lấy toàn bộ danh sách từ Service
            var accounts = _accountService.GetSystemAccounts();
            // Sử dụng LINQ để tìm kiếm theo Tên hoặc Email nếu có từ khóa
            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a =>
                    (a.AccountName != null && a.AccountName.ToLower().Contains(searchString.ToLower())) ||
                    (a.AccountEmail != null && a.AccountEmail.ToLower().Contains(searchString.ToLower()))
                ).ToList();
            }
            // Gửi từ khóa về lại View để giữ nguyên chữ trên ô Input
            ViewData["CurrentFilter"] = searchString;
            return View(accounts);
        }

        // 1. Action GET: Trả về Partial View chứa Form HTML để hiển thị lên Popup
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        // 2. Action POST: Nhận dữ liệu từ Form gửi lên qua AJAX
        [HttpPost]
        public IActionResult Create(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                // Gọi Service để lưu vào DB
                _accountService.AddSystemAccount(account);

                // Trả về JSON báo thành công để JavaScript biết đường đóng Popup
                return Json(new { success = true });
            }

            // Nếu Validation lỗi, trả lại View kèm theo dữ liệu để hiện câu chữ cảnh báo lỗi
            return PartialView("_Create", account);
        }

        // ================= PHẦN EDIT =================
        [HttpGet]
        public IActionResult Edit(short id)
        {
            var account = _accountService.GetSystemAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", account);
        }

        [HttpPost]
        public IActionResult Edit(SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountService.UpdateSystemAccount(account);
                return Json(new { success = true });
            }
            return PartialView("_Edit", account);
        }

        // ================= PHẦN DELETE =================
        [HttpPost]
        public IActionResult Delete(short id)
        {
            var account = _accountService.GetSystemAccountById(id);
            if (account != null)
            {
                _accountService.DeleteSystemAccount(account);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Không tìm thấy tài khoản!" });
        }


    }
}
