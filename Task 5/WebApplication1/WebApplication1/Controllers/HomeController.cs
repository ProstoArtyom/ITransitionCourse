using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.Utility;
using WebApplication1.Utility.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserGenerator _userGenerator;
        public HomeController(ILogger<HomeController> logger,
            UserGenerator userGenerator)
        {
            _logger = logger;
            _userGenerator = userGenerator;
        }

        public IActionResult Index()
        {
            _userGenerator.Reset();
            UserVM userVM = new()
            {
                Region = _userGenerator.Region,
                Seed = _userGenerator.Seed,
                Users = _userGenerator.Generate(20)
            };
            return View(userVM);
        }
        
        [HttpPost]
        public IActionResult Index(UserVM userVm)
        {
            if (ModelState.IsValid)
            {
                _userGenerator.Region = userVm.Region;
                _userGenerator.Seed = userVm.Seed;
            }

            return Index();
        }
        
        public IActionResult LoadData(int count)
        {
            var users = _userGenerator.Generate(count);
            return PartialView("_UsersPartial", users);
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
        
        #region API_CALLS

        [HttpPost]
        public IActionResult GenerateSeed()
        {
            var random = new Random();
            var seed = random.Next(0, int.MaxValue);
            return Json(seed);
        }

        #endregion
    }
}
