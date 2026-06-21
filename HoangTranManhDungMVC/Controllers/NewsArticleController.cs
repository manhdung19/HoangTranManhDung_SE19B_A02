using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoangTranManhDungMVC.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        // Inject cả 3 service để lấy đủ dữ liệu cho Form
        public NewsArticleController(INewsArticleService newsService, ICategoryService categoryService, ITagService tagService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public IActionResult Index(string searchString)
        {
            // Chỉ Staff (Role = 1) mới được quản lý Bài viết
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 1) return RedirectToAction("Login", "Auth");

            ViewData["CurrentFilter"] = searchString;
            var articles = _newsService.GetNewsArticles(searchString);
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            // Truyền Categories và Tags sang View để hiển thị thẻ <select>
            ViewBag.Categories = _categoryService.GetCategories("");
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(NewsArticle article, List<int> selectedTags)
        {
            if (ModelState.IsValid)
            {
                // Tự động gán ID người tạo từ Session
                article.CreatedById = (short?)HttpContext.Session.GetInt32("UserId");
                article.CreatedDate = DateTime.Now;

                _newsService.AddNewsArticle(article, selectedTags);
                return Json(new { success = true });
            }
            ViewBag.Categories = _categoryService.GetCategories("");
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_Create", article);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var article = _newsService.GetNewsArticleById(id);
            if (article == null) return NotFound();

            ViewBag.Categories = _categoryService.GetCategories("");
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_Edit", article);
        }

        [HttpPost]
        public IActionResult Edit(NewsArticle article, List<int> selectedTags)
        {
            if (ModelState.IsValid)
            {
                article.UpdatedById = (short?)HttpContext.Session.GetInt32("UserId");

                _newsService.UpdateNewsArticle(article, selectedTags);
                return Json(new { success = true });
            }
            ViewBag.Categories = _categoryService.GetCategories("");
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_Edit", article);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _newsService.DeleteNewsArticle(id);
            return Json(new { success = true });
        }
    }
}
