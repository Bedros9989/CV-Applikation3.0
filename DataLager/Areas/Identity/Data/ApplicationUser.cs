using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace DataLager.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public string Namn { get; set; }
    public string Efternamn { get; set; }
    public string Adress { get; set; }
    public bool Privat { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int ProfileVisitCount { get; set; }
    public ICollection<Project> SkapadeProjekt { get; set; }
    public ICollection<ProjektDeltagare> Deltagare { get; set; }
    public List<string> RecentSearchQueries { get; set; } = new List<string>();
    public List<string> VisitedProfiles { get; set; } = new List<string>();

}

