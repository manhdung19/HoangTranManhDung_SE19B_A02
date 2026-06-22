using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using HoangTranManhDungRazorPages.Hubs;

namespace HoangTranManhDungRazorPages.Pages.NewsArticle
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IHubContext<NewsHub> _hubContext;

        public IndexModel(INewsArticleService newsArticleService, ICategoryService categoryService, ITagService tagService, IHubContext<NewsHub> hubContext)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
            _hubContext = hubContext;
        }

        public IList<FUNewsManagement.DAL.Models.NewsArticle> NewsArticles { get; set; } = default!;

        public IActionResult OnGet(string searchString = "")
        {
            // Bảo mật: Chỉ Staff (Role = 1) mới được quản lý bài viết
            int? role = HttpContext.Session.GetInt32("UserRole");
            if (role == null || role != 1) return RedirectToPage("/Home");

            NewsArticles = _newsArticleService.GetNewsArticles(searchString ?? "");
            return Page();
        }

        public PartialViewResult OnGetCreatePartial()
        {
            var viewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FUNewsManagement.DAL.Models.NewsArticle>(ViewData, new FUNewsManagement.DAL.Models.NewsArticle());
            viewData["Categories"] = _categoryService.GetCategories("");
            viewData["Tags"] = _tagService.GetAllTags();
            return new PartialViewResult
            {
                ViewName = "_Create",
                ViewData = viewData
            };
        }

        public PartialViewResult OnGetEditPartial(string id)
        {
            var newsArticle = _newsArticleService.GetNewsArticleById(id);
            var viewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FUNewsManagement.DAL.Models.NewsArticle>(ViewData, newsArticle);
            viewData["Categories"] = _categoryService.GetCategories("");
            viewData["Tags"] = _tagService.GetAllTags();
            return new PartialViewResult
            {
                ViewName = "_Edit",
                ViewData = viewData
            };
        }

        public async Task<IActionResult> OnPostCreateAsync(FUNewsManagement.DAL.Models.NewsArticle newsArticle, int[] selectedTags)
        {
            // Gắn ID của người đang đăng nhập vào bài viết
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                newsArticle.CreatedById = (short)userId.Value;
            }

            _newsArticleService.AddNewsArticle(newsArticle, selectedTags?.ToList() ?? new List<int>());
            
            // SignalR: Đẩy thông báo cho toàn bộ client
            await _hubContext.Clients.All.SendAsync("ReceiveNewsNotification", $"Có một bài viết mới vừa được tạo: {newsArticle.NewsTitle}");
            
            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPostEditAsync(FUNewsManagement.DAL.Models.NewsArticle newsArticle, int[] selectedTags)
        {
            _newsArticleService.UpdateNewsArticle(newsArticle, selectedTags?.ToList() ?? new List<int>());

            // SignalR: Đẩy thông báo cho toàn bộ client
            await _hubContext.Clients.All.SendAsync("ReceiveNewsNotification", $"Bài viết vừa được cập nhật: {newsArticle.NewsTitle}");

            return new JsonResult(new { success = true });
        }

        public IActionResult OnPostDelete(string id)
        {
            var article = _newsArticleService.GetNewsArticleById(id);
            if (article != null)
            {
                _newsArticleService.DeleteNewsArticle(id);
            }
            return new JsonResult(new { success = true });
        }
    }
}
