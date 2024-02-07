using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.DataAccess.Repository.IRepository;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Utility;

namespace WebApplication1.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var applicationUsers = _unitOfWork.ApplicationUser.GetAll();
            return View(applicationUsers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsers(List<string> selectedUsers, string action)
        {
            if (selectedUsers != null && selectedUsers.Count > 0)
            {
                foreach (var userId in selectedUsers)
                {
                    var user = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, tracked: true);
                    if (user != null)
                    {
                        switch (action)
                        {
                            case "Block":
                            {
                                user.Status = "Blocked";
                                await _userManager.UpdateSecurityStampAsync(user);
                                TempData["success"] = "Selected users have been blocked!";
                                break;
                            }
                            case "Unlock":
                            {
                                user.Status = "Active";
                                TempData["success"] = "Selected users have been unlocked!";
                                break;
                            }
                            case "Delete":
                            {
                                await _userManager.UpdateSecurityStampAsync(user);
                                await _userManager.DeleteAsync(user);
                                TempData["success"] = "Selected users have been deleted!";
                                break;
                            }
                        }

                        _unitOfWork.Save();
                    }
                    else
                    {
                        return View("Error");
                    }
                }

                var currentUserId = _userManager.GetUserId(User);
                if (action != "Unlock" && selectedUsers.Exists(u => u == currentUserId))
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToPage("/Account/Login", new { Area = "Identity" });
                }
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
