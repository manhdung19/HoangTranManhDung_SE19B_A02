using FUNewsManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISystemAccountService _accountService;

        // Tiêm (Inject) Service vào thông qua Constructor công khai
        public AuthController(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        // 1. Hiển thị giao diện Login (Giao diện mặc định)
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu người dùng đã đăng nhập rồi, tự động chuyển họ đi tiếp
            if (HttpContext.Session.GetString("UserRole") != null)
            {
                return RedirectToRolePage(HttpContext.Session.GetInt32("UserRole"));
            }
            return View();
        }

        // 2. Tiếp nhận dữ liệu Đăng nhập gửi lên từ Form giao diện
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ Email và Mật khẩu!";
                return View();
            }

            // Gọi qua lớp Service để thực hiện kiểm tra logic đăng nhập tổng thể
            var account = _accountService.Authenticate(email, password);

            if (account != null)
            {
                // Lưu trữ thông tin định danh cần thiết vào Session để phân quyền ở các trang sau
                HttpContext.Session.SetInt32("UserId", account.AccountId);
                HttpContext.Session.SetString("UserName", account.AccountName ?? "User");
                HttpContext.Session.SetInt32("UserRole", account.AccountRole ?? -1);

                return RedirectToRolePage(account.AccountRole);
            }

            ViewBag.Error = "Email hoặc Mật khẩu không chính xác!";
            return View();
        }

        // 3. Hàm xử lý Đăng xuất
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa sạch dữ liệu phiên làm việc
            return RedirectToAction("Index", "Home");
        }

        // Hàm phụ trợ điều hướng trang dựa theo vai trò (Role)
        private IActionResult RedirectToRolePage(int? role)
        {
            if (role == 0) return RedirectToAction("Index", "SystemAccount");   // Admin hệ thống
            if (role == 1) return RedirectToAction("Index", "Category");   // Nhân viên làm nội dung
            if (role == 2) return RedirectToAction("Index", "Lecturer"); // Giảng viên đọc tin
            return RedirectToAction("Index", "Home");                   // Khách vãng lai
        }
    }
}