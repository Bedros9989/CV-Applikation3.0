using Core.Models;
using DataLager.Areas.Identity.Data;
using DataLager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLager;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<CV> CV { get; set; }
    public DbSet<Erfarenhet> Erfarenhet { get; set; }
    public DbSet<Kompetenser> Kompetenser { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ProjektDeltagare> ProjektDeltagare { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProjektDeltagare>()
            .HasKey(pd => new { pd.UserId, pd.ProjectId });

        modelBuilder.Entity<ProjektDeltagare>()
            .HasOne(pd => pd.User)
            .WithMany(u => u.Deltagare)
            .HasForeignKey(pd => pd.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjektDeltagare>()
            .HasOne(pd => pd.Project)
            .WithMany(p => p.Deltagare)
            .HasForeignKey(pd => pd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
             
                Id= "1",
                Namn = "Martin",
                Efternamn = "Mandrén",
                Adress = "Uppsalavägen 28, 75 321 Uppsala",
                Privat = false,
                RegistrationDate = new DateTime(2023, 5, 21, 16, 20, 31, 845),
                ProfileVisitCount = 12,
                UserName = "martin@mail.com",
                Email = "martin@mail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "111",
                LockoutEnabled = false,
                AccessFailedCount = 0
            },
            new ApplicationUser
            {
                Id = "2",
                Namn = "Sofie",
                Efternamn = "Rustby",
                Adress = "Örebrovägen 17, 702 14 Örebro",
                Privat = true,
                RegistrationDate = new DateTime(2023, 6, 1, 11, 20, 31, 845),
                ProfileVisitCount = 18,
                UserName = "sofie@mail.com",
                Email = "sofie@mail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "777",
                LockoutEnabled = false,
                AccessFailedCount = 0
            },
            new ApplicationUser
            {
                Id = "3",
                Namn = "Bedros",
                Efternamn = "Butros",
                Adress = "Åstadalsvägen 3C, 702 81 Örebro",
                Privat = false,
                RegistrationDate = new DateTime(2023, 3, 21, 11, 20, 31, 845),
                ProfileVisitCount = 5,
                UserName = "bedros@mail.com",
                Email = "bedros@mail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "0734019685",
                LockoutEnabled = false,
                AccessFailedCount = 0
            },
            new ApplicationUser
            {
                Id = "4",
                Namn = "Rodan",
                Efternamn = "Sevinik",
                Adress = "Storgatan 5, 702 99 Örebro",
                Privat = false,
                RegistrationDate = new DateTime(2023, 11, 18, 11, 20, 31, 845),
                ProfileVisitCount = 18,
                UserName = "rodan@mail.com",
                Email = "rodan@mail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "0706785432",
                LockoutEnabled = false,
                AccessFailedCount = 0
            },
            new ApplicationUser
            {
                Id = "5",
                Namn = "Hannes",
                Efternamn = "Wedeby",
                Adress = "Skolgatan 121, 701 23 Örebro",
                Privat = false,
                RegistrationDate = new DateTime(2023, 12, 02, 11, 20, 31, 845),
                ProfileVisitCount = 14,
                UserName = "hannes@mail.com",
                Email = "hannes@mail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                PhoneNumber = "0767182456",
                LockoutEnabled = false,
                AccessFailedCount = 0
            }
            );

        modelBuilder.Entity<CV>().HasData(
            new CV 
            {
                id = "1",
                Beskrivning = "Erfaren mjukvaruutvecklare med fokus på webbutveckling.",
                ImagePath = "/images/profilbildmartin.jpg",
                Skola = "Tekniska Högskolan",
                Ämnesområde = "Datavetenskap",
                StartDatumSkola = new DateOnly(2015, 9, 1),
                SlutDatumSkola = new DateOnly(2019, 6,30),
                UserID = "1"
            },
            new CV
            {
                id = "2",
                Beskrivning = "Kreativ UX-designer med passion för användarcentrerad design.",
                ImagePath = "/images/profilbildsofie.jpg",
                Skola = "Konst- och Designskolan",
                Ämnesområde = "Interaktionsdesign",
                StartDatumSkola = new DateOnly(2016, 3, 15),
                SlutDatumSkola = new DateOnly(2020, 5, 25),
                UserID = "2"
            },
            new CV
            {
                id = "3",
                Beskrivning = "Engagerad marknadsförare med stark analytisk förmåga.",
                ImagePath = "/images/profilbildbedros.jpg",
                Skola = "Ekonomihögskolan",
                Ämnesområde = "Marknadsföring",
                StartDatumSkola = new DateOnly(2014, 8, 10),
                SlutDatumSkola = new DateOnly(2018, 6, 20),
                UserID = "3"
            }, 
            new CV
            {
                id = "4",
                Beskrivning = "Erfaren projektledare inom IT-branschen.",
                ImagePath = "/images/profilbildrodan.jpg",
                Skola = "Projektledningsskolan",
                Ämnesområde = "IT-projektledning",
                StartDatumSkola = new DateOnly(2012, 10, 5),
                SlutDatumSkola = new DateOnly(2016, 12, 15),
                UserID = "4"
            },
            new CV
            {
                id = "5",
                Beskrivning = "Passionerad lärare med inriktning mot naturvetenskap.",
                ImagePath = "/images/profilbildhannes.jpg",
                Skola = "Lärarhögskolan",
                Ämnesområde = "Naturvetenskap",
                StartDatumSkola = new DateOnly(2010, 9, 1),
                SlutDatumSkola = new DateOnly(2014, 6, 30),
                UserID = "5"
            }
            );

        modelBuilder.Entity<Erfarenhet>().HasData(
            new Erfarenhet
            {
                id = "1",
                Position = "Product Manager",
                FöretagsNamn = "Product Innovations Inc.",
                StartDatum = new DateOnly(2019, 8, 15),
                SlutDatum = new DateOnly(2023, 1, 31),
                AktuellJobb = false,
                CVID = "1"
            },
            new Erfarenhet
            {
                id = "2",
                Position = "Frontend Developer",
                FöretagsNamn = "Tech Solutions AB",
                StartDatum = new DateOnly(2016, 5, 1),
                SlutDatum = new DateOnly(2018, 8, 31),
                AktuellJobb = false,
                CVID = "1"
            },
            new Erfarenhet
            {
                id = "3",
                Position = "UX/UI Designer",
                FöretagsNamn = "Creative Innovations Ltd",
                StartDatum = new DateOnly(2019, 2, 15),
                SlutDatum = new DateOnly(2021, 6, 30),
                AktuellJobb = false,
                CVID = "2"
            },
            new Erfarenhet
            {
                id = "4",
                Position = "Marketing Specialist",
                FöretagsNamn = "Global Marketing Agency",
                StartDatum = new DateOnly(2017, 9, 1),
                SlutDatum = new DateOnly(2020, 12, 15),
                AktuellJobb = false,
                CVID = "2"
            },
            new Erfarenhet
            {
                id = "5",
                Position = "IT Project Manager",
                FöretagsNamn = "Innovate IT Solutions",
                StartDatum = new DateOnly(2013, 1, 10),
                SlutDatum = new DateOnly(2016, 12, 31),
                AktuellJobb = false,
                CVID = "2"
            },
            new Erfarenhet
            {
                id = "6",
                Position = "Science Teacher",
                FöretagsNamn = "City High School",
                StartDatum = new DateOnly(2011, 9, 1),
                SlutDatum = new DateOnly(2014, 6, 30),
                AktuellJobb = false,
                CVID = "3"
            },
            new Erfarenhet
            {
                id = "7",
                Position = "Digital Marketing Specialist",
                FöretagsNamn = "Digital Dynamics Agency",
                StartDatum = new DateOnly(2015, 5, 1),
                SlutDatum = new DateOnly(2018, 10, 30),
                AktuellJobb = false,
                CVID = "3"
            },
            new Erfarenhet
            {
                id = "8",
                Position = "Project Coordinator",
                FöretagsNamn = "Projects R Us",
                StartDatum = new DateOnly(2016, 2, 1),
                SlutDatum = new DateOnly(2019, 7, 15),
                AktuellJobb = false,
                CVID = "4"
            },
            new Erfarenhet
            {
                id = "9",
                Position = "Biology Teacher",
                FöretagsNamn = "City High School",
                StartDatum = new DateOnly(2014, 9, 1),
                SlutDatum = new DateOnly(2017, 6, 30),
                AktuellJobb = false,
                CVID = "4"
            }, 
            new Erfarenhet
            {
                id = "10",
                Position = "Full Stack Developer",
                FöretagsNamn = "CodeCrafters Ltd",
                StartDatum = new DateOnly(2020, 4, 1),
                SlutDatum = new DateOnly(2022, 8, 31),
                AktuellJobb = false,
                CVID = "5"
            },
            new Erfarenhet
            {
                id = "11",
                Position = "Social Media Manager",
                FöretagsNamn = "Social Sphere Inc.",
                StartDatum = new DateOnly(2017, 1, 15),
                SlutDatum = new DateOnly(2020, 5, 15),
                AktuellJobb = false,
                CVID = "5"
            }
            );

        modelBuilder.Entity<Kompetenser>().HasData(
          new Kompetenser
          {
              id = "2",
              Namn = "Web Development",
              CVID = "1"
          },
            new Kompetenser
            {
                id = "3",
                Namn = "User Experience (UX) Design",
                CVID = "1"
            },
            new Kompetenser
            {
                id = "4",
                Namn = "Digital Marketing",
                CVID = "1"
            },
            new Kompetenser
            {
                id = "5",
                Namn = "Project Management",
                CVID = "1"
            },
            new Kompetenser
            {
                id = "6",
                Namn = "Java Programming",
                CVID = "1"
            },
            new Kompetenser
            {
                id = "7",
                Namn = "Product Management",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "8",
                Namn = "Social Media Strategy",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "9",
                Namn = "Agile Methodologies",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "10",
                Namn = "Biology Education",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "11",
                Namn = "Full Stack Development",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "12",
                Namn = "React.js",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "13",
                Namn = "UI/UX Prototyping",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "14",
                Namn = "SEO Optimization",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "15",
                Namn = "Data Analysis",
                CVID = "2"
            },
            new Kompetenser
            {
                id = "16",
                Namn = "Python Programming",
                CVID = "3"
            },
            new Kompetenser
            {
                id = "17",
                Namn = "Content Creation",
                CVID = "3"
            },
            new Kompetenser
            {
                id = "18",
                Namn = "Scrum Framework",
                CVID = "3"
            },
            new Kompetenser
            {
                id = "19",
                Namn = "Physics Education",
                CVID = "3"
            },
            new Kompetenser
            {
                id = "20",
                Namn = "JavaScript",
                CVID = "3"
            },
            new Kompetenser
            {
                id = "21",
                Namn = "Social Media Marketing",
                CVID = "4"
            },
            new Kompetenser
            {
                id = "22",
                Namn = "Angular",
                CVID = "4"
            },
            new Kompetenser
            {
                id = "23",
                Namn = "Wireframing",
                CVID = "4"
            },
            new Kompetenser
            {
                id = "24",
                Namn = "Email Marketing",
                CVID = "4"
            },
            new Kompetenser
            {
                id = "25",
                Namn = "Database Management",
                CVID = "5"
            },
            new Kompetenser
            {
                id = "26",
                Namn = "C# Programming",
                CVID = "5"
            },
            new Kompetenser
            {
                id = "27",
                Namn = "Graphic Design",
                CVID = "5"
            },
            new Kompetenser
            {
                id = "28",
                Namn = "Agile Project Management",
                CVID = "5"
            },
            new Kompetenser
            {
                id = "29",
                Namn = "Chemistry Education",
                CVID = "5"
            }
           );

        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = "1",
                Titel = "E-handelsplattform",
                Beskrivning = "Utveckling av en responsiv e-handelswebbplats med betalningsgateway.",
                Startdatum = new DateOnly(2020, 5, 15),
                Slutdatum = new DateOnly(2020, 10, 31),
                SkapadDen = new DateTime(2023, 3, 21, 11, 20, 31, 845),
                SkapadAv = "2"
            },
            new Project
            {
                Id = "2",
                Titel = "Företagswebbplats Redesign",
                Beskrivning = "Omdesign av företagets webbplats för att förbättra användarupplevelsen och varumärkesrepresentation.",
                Startdatum = new DateOnly(2020, 8, 1),
                Slutdatum = new DateOnly(2021, 1, 31),
                SkapadDen = new DateTime(2023, 3, 21, 11, 25, 45, 932),
                SkapadAv = "1"
            },
            new Project
            {
                Id = "3",
                Titel = "Kampanjhanteringssystem",
                Beskrivning = "Implementering av ett system för att hantera och spåra marknadsföringskampanjer.",
                Startdatum = new DateOnly(2019, 11, 10),
                Slutdatum = new DateOnly(2020, 4, 30),
                SkapadDen = new DateTime(2023, 11, 21, 11, 30, 12, 721),
                SkapadAv = "3"
            },
            new Project
            {
                Id = "4",
                Titel = "Agilt Projekt - Mobilapp",
                Beskrivning = "Utveckling av en mobilapplikation med agila metoder och snabba iterationer.",
                Startdatum = new DateOnly(2021, 3, 1),
                Slutdatum = new DateOnly(2021, 8, 31),
                SkapadDen = new DateTime(2023, 9, 11, 11, 35, 58, 512),
                SkapadAv = "4"
            },
            new Project
            {
                Id = "5",
                Titel = "Skolmaterialhanteringssystem",
                Beskrivning = "Skapande av ett system för att hantera och distribuera digitala skolmaterial.",
                Startdatum = new DateOnly(2018, 9, 15),
                Slutdatum = new DateOnly(2019, 4, 30),
                SkapadDen = new DateTime(2023, 4, 16, 11, 40, 23, 633),
                SkapadAv = "5"
            }
          );

        modelBuilder.Entity<ProjektDeltagare>().HasData(
           new ProjektDeltagare
           {
               UserId = "1",
               ProjectId = "1"
           },
           new ProjektDeltagare
           {
               UserId = "2",
               ProjectId = "1"
           },
           new ProjektDeltagare
           {
               UserId = "3",
               ProjectId = "1"
           },
           new ProjektDeltagare
           {
               UserId = "1",
               ProjectId = "2"
           },
           new ProjektDeltagare
           {
               UserId = "2",
               ProjectId = "2"
           },
           new ProjektDeltagare
           {
               UserId = "3",
               ProjectId = "2"
           },
           new ProjektDeltagare
           {
               UserId = "4",
               ProjectId = "3"
           },
           new ProjektDeltagare
           {
               UserId = "5",
               ProjectId = "4"
           },
           new ProjektDeltagare
           {
               UserId = "4",
               ProjectId = "5"
           }
         );
    }
}
