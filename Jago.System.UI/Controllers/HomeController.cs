using Jago.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}