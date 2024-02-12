using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var nickname = GetNickname();
            if (nickname == null) { return RedirectToAction(nameof(Nickname)); }
            return View(nameof(Index), nickname);
        }

        public IActionResult Nickname()
        {
            var nickname = GetNickname();
            if (nickname != null) { return RedirectToAction(nameof(Index)); }
            return View(new UserVM());
        }

        [HttpPost]
        public IActionResult Nickname(UserVM obj)
        {
            HttpContext.Session.SetString("Nickname", obj.Name);
            return RedirectToAction(nameof(Index));
        }

        public string? GetNickname()
        {
            return HttpContext.Session.GetString("Nickname");
        }

        public IActionResult ChangeNickname()
        { 
            HttpContext.Session.Remove("Nickname");
            return RedirectToAction("Index");
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
