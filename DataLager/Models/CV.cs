using DataLager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Core.Models
{
    public class CV
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Beskrivning { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Skola { get; set; }
        public string Ämnesområde { get; set; }
        public  DateOnly StartDatumSkola { get; set; }
        public DateOnly SlutDatumSkola { get; set; }
        public ICollection<Project> SkapadeProjekt { get; set; }  // This line was added

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

    }
}
