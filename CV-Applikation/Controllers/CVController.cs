using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV_Applikation.Controllers
{
    public class CVController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public CVController(UserManager<ApplicationUser> userManager, ApplicationDbContext cntx)
        {
            this._userManager = userManager;
            _context = cntx;
        }

        public IActionResult Add()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            return View(new CV());
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult LaggTill(CV ettCV)
        {
            _context.CV.Add(ettCV);
            _context.SaveChanges();
            return View(ettCV);
        }

    }
}
