using Core.Models;
using DataLager;
using DataLager.Areas.Identity.Data;
using DataLager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CV_Applikation.Controllers
{
    public class CVController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private const int MaxRecentSearchQueries = 5;

        public CVController(UserManager<ApplicationUser> userManager, ApplicationDbContext cntx)
        {
            this._userManager = userManager;
            _context = cntx;
        }

        public class ContactInfoViewModel
        {
            public string Namn { get; set; }
            public string Efternamn { get; set; }
            public string Adress { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string ImagePath { get; set; }
            public IFormFile ImageFile { get; set; }
            public string UserID { get; set; }

        }

        public class ViewModel
        {
            public string UserID { get; set; }
            public string Namn { get; set; }
            public string Efternamn { get; set; }
            public string Adress { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string ImagePath { get; set; }
            public DateTime RegistrationDate { get; set; }

            public string Beskrivning { get; set; }
            public string Skola { get; set; }
            public string Ämnesområde { get; set; }
            public DateOnly StartDatumSkola { get; set; }
            public DateOnly SlutDatumSkola { get; set; }

            public List<Erfarenhet> ErfarenhetsLista { get; set; }
            public List<Project> ProjektLista { get; set; }
            public List<Kompetenser> KompetensLista { get; set; }
        }

        public class ExperienceViewModel
        {
            public Erfarenhet SingleExperience { get; set; }
            public List<Erfarenhet> ExperienceList { get; set; }
        }

        public class SkillsViewModel
        {
            public Kompetenser SingleSkill { get; set; }
            public List<Kompetenser> SkillList { get; set; }
        }

        public async Task<IActionResult> ContactInfo()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var viewModel = new ContactInfoViewModel
            {
                Namn = user.Namn,
                Efternamn = user.Efternamn,
                Email = user.Email,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
            };
            ViewData["UserID"] = userId;
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("AddContact")]
        public IActionResult ContactInfo(ContactInfoViewModel contactInfoViewModel)
        {

            if (contactInfoViewModel.ImageFile != null && contactInfoViewModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var fileName = Guid.NewGuid().ToString() + "_" + contactInfoViewModel.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    contactInfoViewModel.ImageFile.CopyTo(stream);
                }
                contactInfoViewModel.ImagePath = "/images/" + fileName;
            }
            TempData["ImagePath"] = contactInfoViewModel.ImagePath;
            return RedirectToAction("Profile");
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult ProfileUpdate()
        {
            var userId = _userManager.GetUserId(User);
            var cv = _context.CV
                .Where(cv => cv.UserID == userId).FirstOrDefault();

            ViewData["CvID"] = cv.id;
            return View();
        }

        [HttpPost]
        [ActionName("UpdateProfile")]
        public IActionResult ProfileUpdate(string id, string beskrivning)
        {
            var entityToUpdate = _context.CV.Find(id);

            if (entityToUpdate != null)
            {
                entityToUpdate.Beskrivning = beskrivning;
                _context.SaveChanges();
            }

            return RedirectToAction("MyProfile", "Account");
        }

        [HttpPost]
        [ActionName("AddDescription")]
        public IActionResult Profile(CV ettCV)
        {
            TempData["Beskrivning"] = ettCV.Beskrivning;
            return RedirectToAction("Education");
        }

        public IActionResult Education()
        {
            var userId = _userManager.GetUserId(User);
            ViewData["UserID"] = userId;
            var imagePath = TempData["ImagePath"] as string;
            ViewData["ImagePath"] = imagePath;
            var beskrivning = TempData["Beskrivning"] as string;
            ViewData["Beskrivning"] = beskrivning;
            return View();
        }

        [HttpPost]
        [ActionName("AddToCV")]
        public IActionResult Education(CV ettCV)
        {
            _context.CV.Add(ettCV);
            _context.SaveChanges();
            var newCvId = ettCV.id;
            return RedirectToAction("Experience", new { id = newCvId });
        }

        public IActionResult EducationUpdate()
        {
            var userId = _userManager.GetUserId(User);
            var cv = _context.CV
               .Where(cv => cv.UserID == userId).FirstOrDefault();

            ViewData["CvID"] = cv.id;
            return View();
        }

        [HttpPost]
        [ActionName("UpdateEducation")]
        public IActionResult EducationUpdate(string id, string skola, string ämnesområde, DateOnly startdatumSkola, DateOnly slutdatumSkola)
        {
            var entityToUpdate = _context.CV.Find(id);

            if (entityToUpdate != null)
            {
                entityToUpdate.Skola = skola;
                entityToUpdate.Ämnesområde = ämnesområde;
                entityToUpdate.StartDatumSkola = startdatumSkola;
                entityToUpdate.SlutDatumSkola = slutdatumSkola;
                _context.SaveChanges();
            }

            return RedirectToAction("MyProfile", "Account");
        }

        public IActionResult Experience(string id)
        {
            ViewData["CvId"] = id;
            return View();
        }

        [HttpPost]
        [ActionName("AddToExperience")]
        public IActionResult Experience(Erfarenhet enErfarenhet)
        {
            var userId = _userManager.GetUserId(User);

            if (enErfarenhet.AktuellJobb)
            {
                enErfarenhet.SlutDatum = enErfarenhet.StartDatum;
            }


            _context.Erfarenhet.Add(enErfarenhet);
            _context.SaveChanges();

            var userExperiences = _context.Erfarenhet
                .Where(e => e.ettCV.UserID == userId)
                .ToList();


            return PartialView("_ExperienceTablePartial", userExperiences);

        }

        public IActionResult ExperienceUpdate()
        {
            var userId = _userManager.GetUserId(User);
            var userExperience = _context.Erfarenhet
                .FirstOrDefault(e => e.ettCV.UserID == userId);
            var userExperiencesList = _context.Erfarenhet
                .Where(e => e.ettCV.UserID == userId)
                .ToList();

            var viewModel = new ExperienceViewModel
            {
                SingleExperience = userExperience,
                ExperienceList = userExperiencesList
            };

            string cvId = _context.CV
                .Where(cv => cv.UserID == userId)
                .Select(cv => cv.id)
                .FirstOrDefault();

            ViewData["CvId"] = cvId;
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("ExperienceUpdate")]
        public IActionResult ExperienceUpdate(Erfarenhet enErfarenhet)
        {
            var userId = _userManager.GetUserId(User);

            if (enErfarenhet.AktuellJobb)
            {
                enErfarenhet.SlutDatum = enErfarenhet.StartDatum;
            }
            _context.Erfarenhet.Add(enErfarenhet);
            _context.SaveChanges();
            var userExperiences = _context.Erfarenhet
                .Where(e => e.ettCV.UserID == userId)
                .ToList();
            return PartialView("_ExperienceTablePartial", userExperiences);
        }

        [HttpPost]
        [ActionName("ExperienceDelete")]
        public IActionResult ExperienceDelete(string experienceId, string cvId)
        {

            var experience = _context.Erfarenhet.Where(e => e.id == experienceId).FirstOrDefault();
            _context.Erfarenhet.Remove(experience);
            _context.SaveChanges();
            var updatedExperiences = _context.Erfarenhet.Where(e => e.CVID == cvId).ToList();

            return PartialView("_ExperienceTablePartial", updatedExperiences);

        }

        public IActionResult Skills()
        {
            var userId = _userManager.GetUserId(User);
            string cvId = _context.CV
                .Where(cv => cv.UserID == userId)
                .Select(cv => cv.id)
                .FirstOrDefault();

            ViewData["CvId"] = cvId;
            return View();
        }

        [HttpPost]
        [ActionName("AddToSkills")]
        public IActionResult Skills(Kompetenser enKompetens)
        {
            var userId = _userManager.GetUserId(User);

            _context.Kompetenser.Add(enKompetens);
            _context.SaveChanges();

            var userSkills = _context.Kompetenser
                .Where(k => k.ettCV.UserID == userId)
                .ToList();

            return PartialView("_SkillsTablePartial", userSkills);

        }

        public IActionResult SkillsUpdate()
        {
            var userId = _userManager.GetUserId(User);
            var userSkill = _context.Kompetenser
                .FirstOrDefault(e => e.ettCV.UserID == userId);
            var userSkillList = _context.Kompetenser
                .Where(e => e.ettCV.UserID == userId)
                .ToList();

            var viewModel = new SkillsViewModel
            {
                SingleSkill = userSkill,
                SkillList = userSkillList
            };

            string cvId = _context.CV
                .Where(cv => cv.UserID == userId)
                .Select(cv => cv.id)
                .FirstOrDefault();

            ViewData["CvId"] = cvId;
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("UpdateSkills")]
        public IActionResult SkillsUpdate(Kompetenser enKompetens)
        {
            var userId = _userManager.GetUserId(User);
            _context.Kompetenser.Add(enKompetens);
            _context.SaveChanges();
            var userSkills = _context.Kompetenser
                .Where(k => k.ettCV.UserID == userId)
                .ToList();
            return PartialView("_SkillsTablePartial", userSkills);
        }

        [HttpPost]
        public IActionResult RemoveSkill(string skillId)
        {
            var userId = _userManager.GetUserId(User);
            var kompetens = _context.Kompetenser.FirstOrDefault(k => k.id == skillId);
            if (kompetens != null)
            {
                _context.Kompetenser.Remove(kompetens);
                _context.SaveChanges();
            }

            var userSkills = _context.Kompetenser
                .Where(k => k.ettCV.UserID == userId)
                .ToList();

            return PartialView("_SkillsTablePartial", userSkills);
        }

        public IActionResult Projects()
        {
            List<Project> projects = _context.Projects.ToList();
            var userId = _userManager.GetUserId(User);
            var userProjects = _context.ProjektDeltagare
                .Where(pd => pd.UserId == userId)
                .Select(pd => pd.Project)
                .ToList();

            var availableProjects = _context.Projects
                .AsEnumerable()
                .Where(p => !userProjects.Any(up => up.Id == p.Id))
                .ToList();

            ViewData["UserID"] = userId;
            var viewModel = new Tuple<List<Project>, List<Project>>(userProjects, availableProjects);
            return View(viewModel);
        }

        public IActionResult ProjectsUpdate()
        {
            List<Project> projects = _context.Projects.ToList();
            var userId = _userManager.GetUserId(User);
            var userProjects = _context.ProjektDeltagare
                .Where(pd => pd.UserId == userId)
                .Select(pd => pd.Project)
                .ToList();

            var availableProjects = _context.Projects
                .AsEnumerable()
                .Where(p => !userProjects.Any(up => up.Id == p.Id))
                .ToList();

            ViewData["UserID"] = userId;
            var viewModel = new Tuple<List<Project>, List<Project>>(userProjects, availableProjects);
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("AddToProjectDeltagare")]
        public IActionResult LaggTillIProjectDeltagare(ProjektDeltagare deltagare)
        {
            var existingDeltagare = _context.ProjektDeltagare
                .FirstOrDefault(pd => pd.ProjectId == deltagare.ProjectId && pd.UserId == deltagare.UserId);

            if (existingDeltagare == null)
            {
                _context.ProjektDeltagare.Add(deltagare);
                _context.SaveChanges();
            }
            return RedirectToAction("Projects", "CV");
        }

        [HttpPost]
        [ActionName("AddToProjectDeltagare2")]
        public IActionResult LaggTillIProjectDeltagare2(ProjektDeltagare deltagare)
        {
            var existingDeltagare = _context.ProjektDeltagare
                .FirstOrDefault(pd => pd.ProjectId == deltagare.ProjectId && pd.UserId == deltagare.UserId);

            if (existingDeltagare == null)
            {
                _context.ProjektDeltagare.Add(deltagare);
                _context.SaveChanges();
            }
            return RedirectToAction("ProjectsUpdate", "CV");
        }

        [HttpPost]
        public IActionResult RemoveProjektDeltagare(string projectId)
        {
            var userId = _userManager.GetUserId(User);

            var projectParticipation = _context.ProjektDeltagare.FirstOrDefault(pd => pd.ProjectId == projectId && pd.UserId == userId);
            if (projectParticipation != null)
            {
                _context.ProjektDeltagare.Remove(projectParticipation);
                _context.SaveChanges();
            }

            return RedirectToAction("ProjectsUpdate", "CV");
        }

        [HttpPost]
        public IActionResult RemoveProjektDeltagare2(string projectId)
        {
            var userId = _userManager.GetUserId(User);

            var projectParticipation = _context.ProjektDeltagare.FirstOrDefault(pd => pd.ProjectId == projectId && pd.UserId == userId);
            if (projectParticipation != null)
            {
                _context.ProjektDeltagare.Remove(projectParticipation);
                _context.SaveChanges();
            }

            return RedirectToAction("Projects", "CV");
        }

        public  IActionResult View(string id)
        {
            var myID = _userManager.GetUserId(User);
            var myUser = _context.Users
                .FirstOrDefault(u => u.Id == myID);

            var user = _context.Users
                .FirstOrDefault(u => u.Id == id);

            var cv = _context.CV
                .Where(cv => cv.UserID == id).FirstOrDefault();

            user.ProfileVisitCount++;
            _context.SaveChanges();


            if (User.Identity.IsAuthenticated)
            {
                myUser.VisitedProfiles.Insert(0, id);

                if (myUser.VisitedProfiles.Count > MaxRecentSearchQueries)
                {
                    myUser.VisitedProfiles.RemoveAt(MaxRecentSearchQueries);
                }

                _context.SaveChanges();
            }

            var erfarenhet = _context.Erfarenhet
                .Where(e => e.CVID == cv.id).ToList();

            var kompetenser = _context.Kompetenser
                .Where(k => k.CVID == cv.id).ToList();

            var projects = _context.ProjektDeltagare
                .Where(pd => pd.UserId == id)
                .Select(pd => pd.Project)
                .ToList();


            var viewModel = new ViewModel
            {
                Namn = user.Namn,
                Efternamn = user.Efternamn,
                Email = user.Email,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
                ImagePath = cv.ImagePath,
                RegistrationDate = user.RegistrationDate,
                Beskrivning = cv.Beskrivning,
                Skola = cv.Skola,
                Ämnesområde = cv.Ämnesområde,
                StartDatumSkola = cv.StartDatumSkola,
                SlutDatumSkola = cv.SlutDatumSkola,
                ErfarenhetsLista = erfarenhet,
                ProjektLista = projects,
                UserID = user.Id,
                KompetensLista = kompetenser,
            };

            return View(viewModel);

        }

        [HttpPost]
        [ActionName("UpdatePicture")]
        public IActionResult UpdatePicture(string id, IFormFile imageFile)
        {
            var entityToUpdate = _context.CV.Find(id);

            if (entityToUpdate != null && imageFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                entityToUpdate.ImagePath = "/images/" + uniqueFileName;
                _context.SaveChanges();
            }

            return RedirectToAction("MyProfile", "Account");
        }

        public IActionResult PicturesUpdate()
        {
            var userId = _userManager.GetUserId(User);
            var CV = _context.CV
                .FirstOrDefault(e => e.UserID == userId);

            return View(CV);
        }

    }
}
