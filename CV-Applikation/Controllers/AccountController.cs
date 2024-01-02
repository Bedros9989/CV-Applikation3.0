using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
using DataLager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CV_Applikation.Controllers.CVController;

namespace CV_Applikation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext cntx)
        {
            this._userManager = userManager;
            _context = cntx;
        }
        public class UserViewModel
        {
            public ApplicationUser Auser { get; set; }
            public CV Acv { get; set; }
            public List<CV> Bcv { get; set; }
            public List<Project> projekt { get; set; }
            public Dictionary<string, int> ProjectUserCounts { get; set; }
            public List<ApplicationUser> VisitedProfiles { get; set; }
            public int OlästaMeddelanden { get; set; }
        }
        public class ProfileModel
        {
            public ApplicationUser theUser { get; set; }
            public CV theCV { get; set; }
            public List<Erfarenhet> ErfarenhetsLista { get; set; }
            public List<Project> ProjektLista { get; set; }
            public List<Kompetenser> KompetensLista { get; set; }

        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var hasExistingCV = _context.CV.Any(c => c.UserID == userId);
            ViewData["HasExistingCV"] = hasExistingCV;
            ViewData["UserID"] = userId;
            var user = _context.Users.Where(c => c.Id == userId).FirstOrDefault();
            var CV = _context.CV.Where(c => c.UserID == userId).FirstOrDefault();
            var cvList = _context.CV
                .Include(cv => cv.User)
                .Where(cv => cv.UserID != userId)
                .Distinct().ToList();

            var projektList = _context.Projects
                .OrderByDescending(projekt => projekt.SkapadDen)
                .ToList();

            var visitedProfileUsers = _context.Users
                 .Where(u => user.VisitedProfiles.Contains(u.Id))
                 .ToList();

            var unreadMessageCount = _context.Messages
                 .Where(m => m.MottagarID == userId && m.Läst == false)
                 .Select(m => m.AvsändarNamn)
                 .Distinct()
                 .Count();


            var viewModel = new UserViewModel
            {
                Auser = user,
                VisitedProfiles = visitedProfileUsers,
                Acv = CV,
                Bcv = cvList,
                projekt = projektList,
                ProjectUserCounts = new Dictionary<string, int>(),
                OlästaMeddelanden = unreadMessageCount
            };

            foreach (var projekt in projektList)
            {
                var count = _context.ProjektDeltagare.Count(pd => pd.ProjectId == projekt.Id);
                viewModel.ProjectUserCounts[projekt.Id] = count;
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            else
            {
                return View(viewModel);
            }

        }
        public IActionResult MyProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {

                return Redirect("/Identity/Account/Login");
            }
            else
            {

            var id = _userManager.GetUserId(User);
            var user = _context.Users
                .Where(u => u.Id == id).FirstOrDefault();

            var cv = _context.CV
                .Where(cv => cv.UserID == id).FirstOrDefault();

            if (cv == null)
            {
                return Redirect("/CV/ContactInfo");
            }

            var erfarenhet = _context.Erfarenhet
                .Where(e => e.CVID == cv.id).ToList();

            var kompetenser = _context.Kompetenser
                .Where(k => k.CVID == cv.id).ToList();

            var projects = _context.ProjektDeltagare
                .Where(pd => pd.UserId == id)
                .Select(pd => pd.Project)
                .ToList();

            var viewModel = new ProfileModel
            {
                theUser = user,
                theCV = cv,
                ErfarenhetsLista = erfarenhet,
                ProjektLista = projects,
                KompetensLista = kompetenser,
            };

            return View(viewModel);
            }
        }
        [HttpGet]
        public IActionResult GetUnreadMessageCount()
        {
            var userId = _userManager.GetUserId(User);
            var unreadMessageCount = _context.Messages
                .Where(m => m.MottagarID == userId && !m.Läst)
                .Select(m => m.AvsändarNamn)
                .Distinct()
                .Count();

            return Json(new { UnreadMessageCount = unreadMessageCount });
        }

    }
}
