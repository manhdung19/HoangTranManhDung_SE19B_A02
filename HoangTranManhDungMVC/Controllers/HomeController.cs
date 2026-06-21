using HoangTranManhDungMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FUNewsManagement.BLL.Services.Interfaces; // Thêm thư viện

namespace HoangTranManhDungMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsArticleService _newsService; // Thêm Service

        public HomeController(ILogger<HomeController> logger, INewsArticleService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            // Lọc ra các bài viết có NewsStatus = true (Active) và sắp xếp mới nhất
            var activeNews = _newsService.GetNewsArticles("")
                                         .Where(n => n.NewsStatus == true)
                                         .OrderByDescending(n => n.CreatedDate)
                                         .ToList();
            return View(activeNews);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
