using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Xóa toàn bộ Session khi đăng xuất
            HttpContext.Session.Clear();
            // Đăng xuất xong thì đá về trang chủ Public
            return RedirectToPage("/Home");
        }
    }
}
