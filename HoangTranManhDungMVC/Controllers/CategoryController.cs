using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(string searchString)
        {
            // Phân quyền: Chỉ Staff (Role = 1) mới được vào đây
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 1) return RedirectToAction("Login", "Auth");

            ViewData["CurrentFilter"] = searchString;
            var categories = _categoryService.GetCategories(searchString);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_Create");

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return Json(new { success = true });
            }
            return PartialView("_Create", category);
        }

        [HttpGet]
        public IActionResult Edit(short id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null) return NotFound();
            return PartialView("_Edit", category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return Json(new { success = true });
            }
            return PartialView("_Edit", category);
        }

        [HttpPost]
        public IActionResult Delete(short id)
        {
            bool isDeleted = _categoryService.DeleteCategory(id);
            if (isDeleted)
            {
                return Json(new { success = true });
            }
            // Trả về thông báo lỗi nếu Category đang được dùng trong NewsArticle
            return Json(new { success = false, message = "Không thể xóa danh mục này vì đang có bài viết sử dụng nó!" });
        }
    }
}
