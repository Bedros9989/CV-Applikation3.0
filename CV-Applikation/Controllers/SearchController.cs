using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CV_Applikation.Controllers
{
    public class SearchController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private const int MaxRecentSearchQueries = 6;

        public SearchController(ApplicationDbContext cntx, UserManager<ApplicationUser> userManager)
        {
            _context = cntx;
            _userManager = userManager;

        }


        [HttpGet]
        public IActionResult Search(string query)
        {

            var results = GetSearchResults(query);
            UpdateRecentSearchQueries(query);
            return PartialView("_SearchResultsPartial", results);
        }

        private List<CV> GetSearchResults(string query)
        {
                var userIds = _context.Users
                    .Where(user => user.Namn.ToLower().Contains(query.ToLower()) || user.Efternamn.ToLower().Contains(query.ToLower()))
                    .Select(user => user.Id)
                    .ToList();

                if (User.Identity.IsAuthenticated)
                {
                    var currentUserId = _userManager.GetUserId(User);
                    userIds.Remove(currentUserId);
                }

                var results = _context.CV
                    .Include(cv => cv.User)
                    .Where(cv => userIds.Contains(cv.UserID))
                    .ToList();

                if (!User.Identity.IsAuthenticated)
                {
                    var usersWithPrivatKonto = _context.Users.Where(user => user.Privat).Select(user => user.Id).ToList();
                    results = results.Where(cv => !usersWithPrivatKonto.Contains(cv.UserID)).ToList();
                }

                return results;
        }

        private void UpdateRecentSearchQueries(string query)
        {
            var userId = _userManager.GetUserId(User);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(query))
            {
                var user = _context.Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                if (user != null)
                {
                    user.RecentSearchQueries.Insert(0, query);

                    if (user.RecentSearchQueries.Count > MaxRecentSearchQueries)
                    {
                        user.RecentSearchQueries.RemoveAt(MaxRecentSearchQueries);
                    }

                    _context.SaveChanges();
                }
            }
        }
    }
}
