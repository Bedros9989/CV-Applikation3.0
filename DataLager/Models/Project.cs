using DataLager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public DateOnly Startdatum { get; set; }
        public DateOnly Slutdatum { get; set; }
        public DateTime SkapadDen { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string SkapadAv { get; set; }
        public ApplicationUser Skapare { get; set; }

        public ICollection<ProjektDeltagare> Deltagare { get; set; }
    }
}

