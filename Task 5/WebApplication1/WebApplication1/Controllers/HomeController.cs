using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using FileHelpers;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Utility.Faker;

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
            var userVm = new UserVM
            {
                Region = _userGenerator.Region,
                Seed = _userGenerator.Seed,
                FieldValue = _userGenerator.ErrorFactor,
                Users = _userGenerator.Users = _userGenerator.Generate(20).ToList()
            };
            return View(userVm);
        }
        
        [HttpPost]
        public IActionResult Index(UserVM userVm)
        {
            if (ModelState.IsValid)
            {
                _userGenerator.SetValues(userVm.Region, 
                    userVm.Seed,
                    userVm.FieldValue,
                    userVm.Seed + 1);
            }
            else
            {
                _userGenerator.Reset();
            }

            return Index();
        }
        
        public IActionResult LoadData(int count, int pageNumber)
        {
            _userGenerator.ChangeSeed(pageNumber);
            var users = _userGenerator.Generate(count);
            _userGenerator.Users.AddRange(users);
            return PartialView("_UsersPartial", users);
        }

        public IActionResult ExportToCSV()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Number,Id,FullName,FullAddress,PhoneNumber");
            foreach (var user in _userGenerator.Users)
            {
                builder.AppendLine($"{user.Number},{user.Id},{user.Name},{user.FullAddress},{user.PhoneNumber}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Users.csv");
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
