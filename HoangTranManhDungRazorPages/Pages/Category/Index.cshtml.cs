using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HoangTranManhDungRazorPages.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<FUNewsManagement.DAL.Models.Category> Categories { get; set; } = default!;

        public IActionResult OnGet(string searchString = "")
        {
            // Bảo mật: Chỉ Staff (Role = 1) mới được quản lý danh mục
            int? role = HttpContext.Session.GetInt32("UserRole");
            if (role == null || role != 1) return RedirectToPage("/Home");

            Categories = _categoryService.GetCategories(searchString ?? "");
            return Page();
        }

        public PartialViewResult OnGetCreatePartial()
        {
            return Partial("_Create");
        }

        public PartialViewResult OnGetEditPartial(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            return Partial("_Edit", category);
        }

        public IActionResult OnPostCreate(FUNewsManagement.DAL.Models.Category category)
        {
            _categoryService.AddCategory(category);
            return new JsonResult(new { success = true });
        }

        public IActionResult OnPostEdit(FUNewsManagement.DAL.Models.Category category)
        {
            _categoryService.UpdateCategory(category);
            return new JsonResult(new { success = true });
        }

        public IActionResult OnPostDelete(short id)
        {
            _categoryService.DeleteCategory(id);

            return new JsonResult(new { success = true });
        }
    }
}
