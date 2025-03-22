using Jago.Application.Interfaces.Services;
using Jago.System.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Jago.System.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserServices _userServices;

        public HomeController(ILogger<HomeController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }
        public async Task<IActionResult> Index()
        {
            bool isAdmin = await _userServices.GetCurrentUser(User);

            ViewBag.IsAdmin = isAdmin;
            return View();
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