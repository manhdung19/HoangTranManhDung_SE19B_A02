using FUNewsManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ISystemAccountService _accountService;

        public LoginModel(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            // Đổi GetString thành GetInt32 và check UserId cho an toàn
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToPage("/Home");
            }
            return Page();
        }


        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Vui lòng nhập đầy đủ Email và Mật khẩu!";
                return Page();
            }

            var account = _accountService.Authenticate(Email, Password);
            if (account != null)
            {
                // Lưu Session y hệt MVC
                HttpContext.Session.SetInt32("UserId", account.AccountId);
                HttpContext.Session.SetString("UserName", account.AccountName ?? "User");
                HttpContext.Session.SetInt32("UserRole", account.AccountRole ?? -1);

                // Tạm thời redirect ra trang Public News (Lát nữa sẽ điều hướng sau)
                return RedirectToPage("/Home");
            }

            ErrorMessage = "Email hoặc Mật khẩu không chính xác!";
            return Page();
        }
    }
}
