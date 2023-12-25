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

        public class UserViewModel
        {
            public List<CV> Bcv { get; set; }
            public List<Project> projekt { get; set; }
            public Dictionary<string, int> ProjectUserCounts { get; set; }

        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var projektList = _context.Projects
               .OrderByDescending(projekt => projekt.SkapadDen)
               .ToList();
            var cvList = _context.CV
                .Include(cv => cv.User)
                .Where(cv => !cv.User.Privat)
                .Distinct().ToList();

            var viewModel = new UserViewModel
            {
                Bcv = cvList,
                projekt = projektList,
                ProjectUserCounts = new Dictionary<string, int>(),
            };

            foreach (var projekt in projektList)
            {
                var count = _context.ProjektDeltagare.Count(pd => pd.ProjectId == projekt.Id);
                viewModel.ProjectUserCounts[projekt.Id] = count;
            }

            return View(viewModel);
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
