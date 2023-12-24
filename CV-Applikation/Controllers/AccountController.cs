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
              


            var viewModel = new UserViewModel
            {
                Auser = user,
                Acv = CV,
                Bcv = cvList,
            };


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
                // Redirect to CV/ContactInfo action if no CV exists
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

    }
}
