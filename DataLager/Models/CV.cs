using DataLager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class CV
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        [Required]
        public string Kompetenser { get; set; }
        [Required]
        public string Utbildningar { get; set; } 
        [Required]
        public string TidigareErfarenhet { get; set; }
        [Required]
        public string ImagePath { get; set; }

        public ICollection<Project> SkapadeProjekt { get; set; }  // This line was added

        [ForeignKey(nameof(ApplicationUser))]
        public string UserID { get; set; }
       
        public ApplicationUser User { get; set; }

    }
}
