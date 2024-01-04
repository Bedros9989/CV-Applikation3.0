using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
using DataLager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static CV_Applikation.Controllers.CVController;

namespace CV_Applikation.Controllers
{
    public class MessagesController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;

        public class ConversationViewModel
        {
            public string AvsändarId { get; set; }
            public string AvsändarNamn { get; set; }
            public string Innehåll { get; set; }
            public DateTime DatumOchTid { get; set; }
            public string MottagarID { get; set; }
            public bool Läst { get; set; }
            public List<Message> SentMessages { get; set; }
            public List<Message> ReceivedMessages { get; set; }

        }
        public class ConversationUsersViewModel
        {
            public string Id { get; set; }
            public string Namn { get; set; }
            public string Efternamn { get; set; }
            public string ImagePath { get; set; }
        }
        public MessagesController(UserManager<ApplicationUser> userManager, ApplicationDbContext cntx)
        {
            this._userManager = userManager;
            _context = cntx;
        }
        [Authorize]
        public IActionResult Index()
        {
            var myUserID = _userManager.GetUserId(User);

            var conversationUsers = _context.Messages
                .Where(m => (m.AvsändarId == myUserID && m.MottagarID != null) || m.MottagarID == myUserID)
                .Select(m => m.AvsändarId == myUserID ? m.MottagarID : m.AvsändarId)
                .Distinct()
                .ToList();

            var usersWithInfo = _context.Users
                .Join(
                    _context.CV,
                    user => user.Id,
                    cv => cv.UserID,
                    (user, cv) => new ConversationUsersViewModel
                    {
                        Id = user.Id,
                        Namn = user.Namn,
                        Efternamn = user.Efternamn,
                        ImagePath = cv.ImagePath
                    }
                )
                .Where(u => conversationUsers.Contains(u.Id))
                .ToList();

            var messagesWithoutUser = _context.Messages
                .Where(m => m.AvsändarId == "Anonym" && m.MottagarID == myUserID)
                .ToList();

            foreach (var message in messagesWithoutUser)
            {
                var placeholderUser = new ConversationUsersViewModel
                {
                    Id = "Anonym",
                    Namn = message.AvsändarNamn, 
                    Efternamn = " ",
                    ImagePath = "/src/default.jpeg" 
                };

                usersWithInfo.Add(placeholderUser);
            }

            usersWithInfo = usersWithInfo
            .OrderByDescending(u => _context.Messages
            .Where(m => (m.AvsändarId == myUserID && m.MottagarID == u.Id) ||
                        (m.MottagarID == myUserID && m.AvsändarId == u.Id))
            .Max(m => m.DatumOchTid))
            .ToList();
            return View(usersWithInfo);
        }
        public IActionResult LoadConversation(string id)
        {
            var myUserID = _userManager.GetUserId(User);

            var conversationMessages = _context.Messages
                .Where(m => (m.AvsändarId == myUserID && m.MottagarID == id) || (m.AvsändarId == id && m.MottagarID == myUserID))
                .OrderBy(m => m.DatumOchTid)
                .ToList();
            var viewModel = new ConversationViewModel
            {
                SentMessages = conversationMessages.Where(m => m.AvsändarId == myUserID).ToList(),
                ReceivedMessages = conversationMessages.Where(m => m.AvsändarId == id && m.MottagarID == myUserID).ToList(),
            };

            ViewData["MottagarID"] = id;
            return PartialView("_ConversationPartial", viewModel);
        }
        public IActionResult LoadAnonymConversation(string id)
        {
            var myUserID = _userManager.GetUserId(User);

            var conversationMessages = _context.Messages
                .Where(m => (m.AvsändarNamn == myUserID && m.MottagarID == id) || (m.AvsändarNamn == id && m.MottagarID == myUserID))
                .OrderBy(m => m.DatumOchTid)
                .ToList();

            var viewModel = new ConversationViewModel
            {
                SentMessages = conversationMessages.Where(m => m.AvsändarNamn == myUserID).ToList(),
                ReceivedMessages = conversationMessages.Where(m => m.AvsändarNamn == id && m.MottagarID == myUserID).ToList(),

            };

            ViewData["MottagarID"] = id;
            return PartialView("_ConversationPartialAnonym", viewModel);
        }
        [HttpPost]
        [Authorize]
        public IActionResult SendMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Json(new { innehall = message.Innehåll });

        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SendMessage2(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Json(new { innehall = message.Innehåll });
        }
        [HttpPost]
        [Authorize]
        public IActionResult MarkMessagesAsRead(string avsändarNamn)
        {
            var myUserID = _userManager.GetUserId(User);
            _context.Messages
                .Where(m => m.MottagarID == myUserID && m.AvsändarNamn == avsändarNamn && !m.Läst)
                .ToList()
                .ForEach(m => m.Läst = true);
            _context.SaveChanges();
            return Ok();
        }
    }
}
