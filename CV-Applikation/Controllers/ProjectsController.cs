using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
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

        public class ProjectViewModel
        {
            public Project projekt { get; set; }
            public List<ApplicationUser> användare  { get; set; }

        }

        public IActionResult Add()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            var newProject = new Project();
            newProject.Startdatum = DateOnly.FromDateTime(DateTime.Now);
            newProject.Slutdatum = DateOnly.FromDateTime(DateTime.Now);
            return View(newProject);
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult LaggTill(Project ettProject)
        {
            _context.Projects.Add(ettProject);
            _context.SaveChanges();
            return Redirect("/CV/Projects");
        }

        public IActionResult Add2()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            var newProject = new Project();
            newProject.Startdatum = DateOnly.FromDateTime(DateTime.Now);
            newProject.Slutdatum = DateOnly.FromDateTime(DateTime.Now);
            return View(newProject);
        }

        [HttpPost]
        [ActionName("Add2")]
        public IActionResult LaggTill2(Project ettProject)
        {
            _context.Projects.Add(ettProject);
            _context.SaveChanges();
            return Redirect("/CV/ProjectsUpdate");
        }

        public IActionResult Add3()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            var newProject = new Project();
            newProject.Startdatum = DateOnly.FromDateTime(DateTime.Now);
            newProject.Slutdatum = DateOnly.FromDateTime(DateTime.Now);
            return View(newProject);
        }

        [HttpPost]
        [ActionName("Add3")]
        public IActionResult LaggTill3(Project ettProject)
        {
            _context.Projects.Add(ettProject);
            _context.SaveChanges();
            return Redirect("/Projects/Index");
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

        public IActionResult View(string id)
        {
            var projektet = _context.Projects
        .FirstOrDefault(p => p.Id == id);

            if (projektet == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var viewModel = new ProjectViewModel
                {
                    projekt = projektet,
                    användare = _context.ProjektDeltagare
                        .Where(pd => pd.ProjectId == id)
                        .Join(
                            _context.CV,
                            pd => pd.UserId,
                            cv => cv.UserID,
                            (pd, cv) => new ApplicationUser
                            {
                                Id = cv.UserID,
                                Namn = cv.User.Namn,
                                Efternamn = cv.User.Efternamn,
                                Privat = cv.User.Privat
                            }
                        )
                        .ToList()
                };

                return View(viewModel);
            }
            else
            {
                var viewModel = new ProjectViewModel
                {
                    projekt = projektet,
                    användare = _context.ProjektDeltagare
                        .Where(pd => pd.ProjectId == id)
                        .Join(
                            _context.CV,
                            pd => pd.UserId,
                            cv => cv.UserID,
                            (pd, cv) => new ApplicationUser
                            {
                                Id = cv.UserID,
                                Namn = cv.User.Namn,
                                Efternamn = cv.User.Efternamn,
                                Privat = cv.User.Privat
                            }
                        )
                        .Where(cv => !cv.Privat)
                        .ToList()
                };

                return View(viewModel);
            }

        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            Project ettProjekt = _context.Projects.Find(id);
            ViewData["SkapadAv"] = ettProjekt.SkapadAv;
            ViewData["SkapadDen"] = ettProjekt.SkapadDen;
            return View(ettProjekt);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(Project projekt)
        {
            _context.Projects.Update(projekt);
            _context.SaveChanges();
            return RedirectToAction("Projects", "CV");
        }

        [HttpGet]
        public IActionResult Update2(string id)
        {
            Project ettProjekt = _context.Projects.Find(id);
            ViewData["SkapadAv"] = ettProjekt.SkapadAv;
            ViewData["SkapadDen"] = ettProjekt.SkapadDen;
            return View(ettProjekt);
        }

        [HttpPost]
        [ActionName("Edit2")]
        public IActionResult Edit2(Project projekt)
        {
            _context.Projects.Update(projekt);
            _context.SaveChanges();
            return RedirectToAction("ProjectsUpdate", "CV");
        }

        [HttpGet]
        public IActionResult Update3(string id)
        {
            Project ettProjekt = _context.Projects.Find(id);
            ViewData["SkapadAv"] = ettProjekt.SkapadAv;
            ViewData["SkapadDen"] = ettProjekt.SkapadDen;
            return View(ettProjekt);
        }

        [HttpPost]
        [ActionName("Edit3")]
        public IActionResult Edit3(Project projekt)
        {
            _context.Projects.Update(projekt);
            _context.SaveChanges();
            return RedirectToAction("Index", "Projects");
        }
    }
}
