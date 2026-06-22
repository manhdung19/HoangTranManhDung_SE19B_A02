using FUNewsManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages.SystemAccount
{
    public class IndexModel : PageModel
    {
        private readonly ISystemAccountService _accountService;

        public IndexModel(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        public IList<FUNewsManagement.DAL.Models.SystemAccount> Accounts { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IActionResult OnGet()
        {
            // Bảo mật: Chỉ Admin (Role = 0) mới được xem
            int? role = HttpContext.Session.GetInt32("UserRole");
            if (role == null || role != 0) return RedirectToPage("/Home");

            var accounts = _accountService.GetSystemAccounts();
            if (!string.IsNullOrEmpty(SearchString))
            {
                accounts = accounts.Where(a =>
                    (a.AccountName != null && a.AccountName.ToLower().Contains(SearchString.ToLower())) ||
                    (a.AccountEmail != null && a.AccountEmail.ToLower().Contains(SearchString.ToLower()))
                ).ToList();
            }

            Accounts = accounts;
            return Page();
        }

        // --- CÁC HÀM XỬ LÝ AJAX POPUP ---

        public PartialViewResult OnGetCreatePartial()
        {
            return Partial("_Create", new FUNewsManagement.DAL.Models.SystemAccount());
        }

        public PartialViewResult OnGetEditPartial(short id)
        {
            var account = _accountService.GetSystemAccountById(id);
            return Partial("_Edit", account);
        }

        public IActionResult OnPostCreate(FUNewsManagement.DAL.Models.SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountService.AddSystemAccount(account);
                return new JsonResult(new { success = true });
            }
            return Partial("_Create", account);
        }

        public IActionResult OnPostEdit(FUNewsManagement.DAL.Models.SystemAccount account)
        {
            if (ModelState.IsValid)
            {
                _accountService.UpdateSystemAccount(account);
                return new JsonResult(new { success = true });
            }
            return Partial("_Edit", account);
        }

        public IActionResult OnPostDelete(short id)
        {// Bước 1: Tìm tài khoản dựa vào id
            var account = _accountService.GetSystemAccountById(id);

            // Bước 2: Nếu tìm thấy thì mới ném vào hàm Delete
            if (account != null)
            {
                _accountService.DeleteSystemAccount(account);
            }

            return new JsonResult(new { success = true });
        }
    }
}
