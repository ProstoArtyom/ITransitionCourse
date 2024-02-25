using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Net.Http;
using WebApplication1.DataAccess.Repository;
using WebApplication1.DataAccess.Repository.IRepository;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Utility.Hubs;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<GameHub> _hubContext;
        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            IHubContext<GameHub> hubContext)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var nickname = GetNickname();
            if (nickname == null) { return RedirectToAction(nameof(Nickname)); }

            var homeWm = new HomeVM
            {
                Nickname = nickname,
                Boards = _unitOfWork.Board.GetAll()
            };

            return View(nameof(Index), homeWm);
        }

        public IActionResult Nickname()
        {
            var nickname = GetNickname();
            if (nickname != null) { return RedirectToAction(nameof(Index)); }
            return View(new HomeVM());
        }

        public async Task<IActionResult> AddBoard()
        {
            var board = new Board
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Drawing board"
            };
            _unitOfWork.Board.Add(board);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Nickname(HomeVM obj)
        {
            HttpContext.Session.SetString("Nickname", obj.Nickname);
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

        #region API_CALLS

        [HttpGet]
        public IActionResult GetBoards()
        {
            var boards = _unitOfWork.Board.GetAll();
            return Json(boards);
        }

        #endregion
    }
}
