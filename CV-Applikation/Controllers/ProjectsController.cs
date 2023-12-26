using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
using DataLager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV_Applikation.Controllers
{
    public class ProjectsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public ProjectsController(UserManager<ApplicationUser> userManager, ApplicationDbContext cntx)
        {
            this._userManager = userManager;
            _context = cntx;
        }

        public IActionResult Add()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            return View(new Project());
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult LaggTill(Project ettProject, string returnUrl)
        {
            _context.Projects.Add(ettProject);
            _context.SaveChanges();
            return Redirect("/CV/Projects");
        }


        public IActionResult Add2()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            return View(new Project());
        }

        [HttpPost]
        [ActionName("Add2")]
        public IActionResult LaggTill2(Project ettProject, string returnUrl)
        {
            _context.Projects.Add(ettProject);
            _context.SaveChanges();
            return Redirect("/CV/ProjectsUpdate");
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;

            var projektList = _context.Projects
               .OrderByDescending(projekt => projekt.SkapadDen)
               .ToList();
            return View(projektList);
        }

    }
}
