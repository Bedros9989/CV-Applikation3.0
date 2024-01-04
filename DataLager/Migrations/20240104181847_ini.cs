using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLager.Migrations
{
    /// <inheritdoc />
    public partial class ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Efternamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Privat = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileVisitCount = table.Column<int>(type: "int", nullable: false),
                    RecentSearchQueries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitedProfiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ämnesområde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDatumSkola = table.Column<DateOnly>(type: "date", nullable: true),
                    SlutDatumSkola = table.Column<DateOnly>(type: "date", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.id);
                    table.ForeignKey(
                        name: "FK_CV_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvsändarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvsändarNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Innehåll = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumOchTid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MottagarID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvsändareId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MottagareId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Läst = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_AvsändareId",
                        column: x => x.AvsändareId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_MottagareId",
                        column: x => x.MottagareId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Erfarenhet",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FöretagsNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    SlutDatum = table.Column<DateOnly>(type: "date", nullable: false),
                    AktuellJobb = table.Column<bool>(type: "bit", nullable: false),
                    CVID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erfarenhet", x => x.id);
                    table.ForeignKey(
                        name: "FK_Erfarenhet_CV_CVID",
                        column: x => x.CVID,
                        principalTable: "CV",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kompetenser",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompetenser", x => x.id);
                    table.ForeignKey(
                        name: "FK_Kompetenser_CV_CVID",
                        column: x => x.CVID,
                        principalTable: "CV",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Startdatum = table.Column<DateOnly>(type: "date", nullable: false),
                    Slutdatum = table.Column<DateOnly>(type: "date", nullable: false),
                    SkapadDen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SkapadAv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkapareId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CVid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_SkapareId",
                        column: x => x.SkapareId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_CV_CVid",
                        column: x => x.CVid,
                        principalTable: "CV",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProjektDeltagare",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjektDeltagare", x => new { x.UserId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjektDeltagare_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjektDeltagare_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Adress", "ConcurrencyStamp", "Efternamn", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Namn", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Privat", "ProfileVisitCount", "RecentSearchQueries", "RegistrationDate", "SecurityStamp", "TwoFactorEnabled", "UserName", "VisitedProfiles" },
                values: new object[,]
                {
                    { "1", 0, "Uppsalavägen 28, 75 321 Uppsala", "8e8cd93c-d589-4b0e-8e5e-3a5142397d3d", "Mandrén", "martin@mail.com", false, false, null, "Martin", null, null, null, "111", false, false, 12, "[]", new DateTime(2023, 5, 21, 16, 20, 31, 845, DateTimeKind.Unspecified), "262c14a6-e6fd-470b-a5bb-ce3a9ced0c19", false, "martin@mail.com", "[]" },
                    { "2", 0, "Örebrovägen 17, 702 14 Örebro", "31a99515-deab-4df7-90d0-9b520c54713e", "Rustby", "sofie@mail.com", false, false, null, "Sofie", null, null, null, "777", false, true, 18, "[]", new DateTime(2023, 6, 1, 11, 20, 31, 845, DateTimeKind.Unspecified), "9adae827-4571-4959-9c19-c1207c34bf9a", false, "sofie@mail.com", "[]" },
                    { "3", 0, "Åstadalsvägen 3C, 702 81 Örebro", "abcb78e9-2e5a-40e1-8900-61bcee11a103", "Butros", "bedros@mail.com", false, false, null, "Bedros", null, null, null, "0734019685", false, false, 5, "[]", new DateTime(2023, 3, 21, 11, 20, 31, 845, DateTimeKind.Unspecified), "3c9516bf-4ed9-43e5-bdb9-53f281519066", false, "bedros@mail.com", "[]" },
                    { "4", 0, "Storgatan 5, 702 99 Örebro", "636019f3-050d-462c-a8f5-24344bfa651e", "Sevinik", "rodan@mail.com", false, false, null, "Rodan", null, null, null, "0706785432", false, false, 18, "[]", new DateTime(2023, 11, 18, 11, 20, 31, 845, DateTimeKind.Unspecified), "31ac5bb1-fb67-431e-b13f-b1a3c0b0087f", false, "rodan@mail.com", "[]" },
                    { "5", 0, "Skolgatan 121, 701 23 Örebro", "bd416611-2182-4dd7-8eaf-c3ab98d87988", "Wedeby", "hannes@mail.com", false, false, null, "Hannes", null, null, null, "0767182456", false, false, 14, "[]", new DateTime(2023, 12, 2, 11, 20, 31, 845, DateTimeKind.Unspecified), "9b9bc4a3-11b0-4808-a502-f31ed827dd72", false, "hannes@mail.com", "[]" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Beskrivning", "CVid", "SkapadAv", "SkapadDen", "SkapareId", "Slutdatum", "Startdatum", "Titel" },
                values: new object[,]
                {
                    { "1", "Utveckling av en responsiv e-handelswebbplats med betalningsgateway.", null, "2", new DateTime(2023, 3, 21, 11, 20, 31, 845, DateTimeKind.Unspecified), null, new DateOnly(2020, 10, 31), new DateOnly(2020, 5, 15), "E-handelsplattform" },
                    { "2", "Omdesign av företagets webbplats för att förbättra användarupplevelsen och varumärkesrepresentation.", null, "1", new DateTime(2023, 3, 21, 11, 25, 45, 932, DateTimeKind.Unspecified), null, new DateOnly(2021, 1, 31), new DateOnly(2020, 8, 1), "Företagswebbplats Redesign" },
                    { "3", "Implementering av ett system för att hantera och spåra marknadsföringskampanjer.", null, "3", new DateTime(2023, 11, 21, 11, 30, 12, 721, DateTimeKind.Unspecified), null, new DateOnly(2020, 4, 30), new DateOnly(2019, 11, 10), "Kampanjhanteringssystem" },
                    { "4", "Utveckling av en mobilapplikation med agila metoder och snabba iterationer.", null, "4", new DateTime(2023, 9, 11, 11, 35, 58, 512, DateTimeKind.Unspecified), null, new DateOnly(2021, 8, 31), new DateOnly(2021, 3, 1), "Agilt Projekt - Mobilapp" },
                    { "5", "Skapande av ett system för att hantera och distribuera digitala skolmaterial.", null, "5", new DateTime(2023, 4, 16, 11, 40, 23, 633, DateTimeKind.Unspecified), null, new DateOnly(2019, 4, 30), new DateOnly(2018, 9, 15), "Skolmaterialhanteringssystem" }
                });

            migrationBuilder.InsertData(
                table: "CV",
                columns: new[] { "id", "Beskrivning", "ImagePath", "Skola", "SlutDatumSkola", "StartDatumSkola", "UserID", "Ämnesområde" },
                values: new object[,]
                {
                    { "1", "Erfaren mjukvaruutvecklare med fokus på webbutveckling.", "/images/profilbildmartin.jpg", "Tekniska Högskolan", new DateOnly(2019, 6, 30), new DateOnly(2015, 9, 1), "1", "Datavetenskap" },
                    { "2", "Kreativ UX-designer med passion för användarcentrerad design.", "/images/profilbildsofie.jpg", "Konst- och Designskolan", new DateOnly(2020, 5, 25), new DateOnly(2016, 3, 15), "2", "Interaktionsdesign" },
                    { "3", "Engagerad marknadsförare med stark analytisk förmåga.", "/images/profilbildbedros.jpg", "Ekonomihögskolan", new DateOnly(2018, 6, 20), new DateOnly(2014, 8, 10), "3", "Marknadsföring" },
                    { "4", "Erfaren projektledare inom IT-branschen.", "/images/profilbildrodan.jpg", "Projektledningsskolan", new DateOnly(2016, 12, 15), new DateOnly(2012, 10, 5), "4", "IT-projektledning" },
                    { "5", "Passionerad lärare med inriktning mot naturvetenskap.", "/images/profilbildhannes.jpg", "Lärarhögskolan", new DateOnly(2014, 6, 30), new DateOnly(2010, 9, 1), "5", "Naturvetenskap" }
                });

            migrationBuilder.InsertData(
                table: "ProjektDeltagare",
                columns: new[] { "ProjectId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "1" },
                    { "1", "2" },
                    { "2", "2" },
                    { "1", "3" },
                    { "2", "3" },
                    { "3", "4" },
                    { "5", "4" },
                    { "4", "5" }
                });

            migrationBuilder.InsertData(
                table: "Erfarenhet",
                columns: new[] { "id", "AktuellJobb", "CVID", "FöretagsNamn", "Position", "SlutDatum", "StartDatum" },
                values: new object[,]
                {
                    { "1", false, "1", "Product Innovations Inc.", "Product Manager", new DateOnly(2023, 1, 31), new DateOnly(2019, 8, 15) },
                    { "10", false, "5", "CodeCrafters Ltd", "Full Stack Developer", new DateOnly(2022, 8, 31), new DateOnly(2020, 4, 1) },
                    { "11", false, "5", "Social Sphere Inc.", "Social Media Manager", new DateOnly(2020, 5, 15), new DateOnly(2017, 1, 15) },
                    { "2", false, "1", "Tech Solutions AB", "Frontend Developer", new DateOnly(2018, 8, 31), new DateOnly(2016, 5, 1) },
                    { "3", false, "2", "Creative Innovations Ltd", "UX/UI Designer", new DateOnly(2021, 6, 30), new DateOnly(2019, 2, 15) },
                    { "4", false, "2", "Global Marketing Agency", "Marketing Specialist", new DateOnly(2020, 12, 15), new DateOnly(2017, 9, 1) },
                    { "5", false, "2", "Innovate IT Solutions", "IT Project Manager", new DateOnly(2016, 12, 31), new DateOnly(2013, 1, 10) },
                    { "6", false, "3", "City High School", "Science Teacher", new DateOnly(2014, 6, 30), new DateOnly(2011, 9, 1) },
                    { "7", false, "3", "Digital Dynamics Agency", "Digital Marketing Specialist", new DateOnly(2018, 10, 30), new DateOnly(2015, 5, 1) },
                    { "8", false, "4", "Projects R Us", "Project Coordinator", new DateOnly(2019, 7, 15), new DateOnly(2016, 2, 1) },
                    { "9", false, "4", "City High School", "Biology Teacher", new DateOnly(2017, 6, 30), new DateOnly(2014, 9, 1) }
                });

            migrationBuilder.InsertData(
                table: "Kompetenser",
                columns: new[] { "id", "CVID", "Namn" },
                values: new object[,]
                {
                    { "10", "2", "Biology Education" },
                    { "11", "2", "Full Stack Development" },
                    { "12", "2", "React.js" },
                    { "13", "2", "UI/UX Prototyping" },
                    { "14", "2", "SEO Optimization" },
                    { "15", "2", "Data Analysis" },
                    { "16", "3", "Python Programming" },
                    { "17", "3", "Content Creation" },
                    { "18", "3", "Scrum Framework" },
                    { "19", "3", "Physics Education" },
                    { "2", "1", "Web Development" },
                    { "20", "3", "JavaScript" },
                    { "21", "4", "Social Media Marketing" },
                    { "22", "4", "Angular" },
                    { "23", "4", "Wireframing" },
                    { "24", "4", "Email Marketing" },
                    { "25", "5", "Database Management" },
                    { "26", "5", "C# Programming" },
                    { "27", "5", "Graphic Design" },
                    { "28", "5", "Agile Project Management" },
                    { "29", "5", "Chemistry Education" },
                    { "3", "1", "User Experience (UX) Design" },
                    { "4", "1", "Digital Marketing" },
                    { "5", "1", "Project Management" },
                    { "6", "1", "Java Programming" },
                    { "7", "2", "Product Management" },
                    { "8", "2", "Social Media Strategy" },
                    { "9", "2", "Agile Methodologies" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CV_UserID",
                table: "CV",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Erfarenhet_CVID",
                table: "Erfarenhet",
                column: "CVID");

            migrationBuilder.CreateIndex(
                name: "IX_Kompetenser_CVID",
                table: "Kompetenser",
                column: "CVID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AvsändareId",
                table: "Messages",
                column: "AvsändareId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MottagareId",
                table: "Messages",
                column: "MottagareId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CVid",
                table: "Projects",
                column: "CVid");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SkapareId",
                table: "Projects",
                column: "SkapareId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjektDeltagare_ProjectId",
                table: "ProjektDeltagare",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Erfarenhet");

            migrationBuilder.DropTable(
                name: "Kompetenser");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProjektDeltagare");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
