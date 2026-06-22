using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountService _accountService;

        public IndexModel(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public FUNewsManagement.DAL.Models.SystemAccount SystemAccount { get; set; } = default!;

        public string? SuccessMessage { get; set; }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            // Cho phép Staff (1) và Lecturer (2) xem profile. Có thể mở rộng cho Admin (0) tuỳ ý.
            if (userId == null) return RedirectToPage("/Login");

            var account = _accountService.GetSystemAccountById((short)userId);
            if (account == null) return NotFound();

            SystemAccount = account;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Login");

            // Bảo vệ ID không bị thay đổi bằng cách gán cứng ID từ session
            SystemAccount.AccountId = (short)userId;

            // Để tránh người dùng hack Form HTML và đổi Role của chính mình, ta nên giữ nguyên Role hiện tại
            var existingAccount = _accountService.GetSystemAccountById((short)userId);
            if (existingAccount != null)
            {
                SystemAccount.AccountRole = existingAccount.AccountRole;
            }

            _accountService.UpdateSystemAccount(SystemAccount);

            // Cập nhật lại Tên hiển thị trong Session nếu họ đổi tên
            HttpContext.Session.SetString("UserName", SystemAccount.AccountName ?? "User");

            SuccessMessage = "Cập nhật thông tin thành công!";
            return Page();
        }
    }
}
