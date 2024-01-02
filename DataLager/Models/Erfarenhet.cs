using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLager.Models
{
    public class Erfarenhet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Position { get; set; }
        public string FöretagsNamn { get; set; }
        public DateOnly StartDatum { get; set; } 
        public DateOnly SlutDatum { get; set; }
        public bool AktuellJobb { get; set; }

        [ForeignKey(nameof(CV))]
        public string CVID { get; set; }
        public CV ettCV { get; set; }

    }
}
