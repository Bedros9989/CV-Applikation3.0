using DataLager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string AvsändarId { get; set; } 
        public string AvsändarNamn { get; set; } 
        public string Innehåll { get; set; }
        public DateTime DatumOchTid { get; set; }
        public string MottagarID { get; set; }
        public ApplicationUser Avsändare { get; set; }
        public ApplicationUser Mottagare { get; set; }
        public bool Läst { get; set; }

    }
}
