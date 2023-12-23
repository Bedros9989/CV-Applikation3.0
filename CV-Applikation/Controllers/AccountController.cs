using DataLager;
using DataLager.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var hasExistingCV = _context.CV.Any(c => c.UserID == userId);
            ViewData["HasExistingCV"] = hasExistingCV;
            ViewData["UserID"] = userId;


            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            else
            {
                return View();
            }
        }
    }
}
