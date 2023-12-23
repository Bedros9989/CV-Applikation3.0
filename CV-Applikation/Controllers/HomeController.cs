using Core.Models;
using CV_Applikation.Models;
using DataLager.Areas.Identity.Data;
using DataLager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CV_Applikation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext cntx)
        {
            _logger = logger;
            this._userManager = userManager;
            _context = cntx;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            if (User.Identity.IsAuthenticated)
            {
                var cvList = _context.CV
                .Include(cv => cv.User)
                .Where(cv => cv.UserID != userId)
                .Distinct().ToList();
                return View(cvList);
            }

            else

            {
                var cvList = _context.CV
                    .Include(cv => cv.User)
                    .Where(cv => !cv.User.Privat)
                    .Distinct().ToList();
                return View(cvList);
            }
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
