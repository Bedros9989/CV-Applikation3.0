using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLager.Models
{
    public class Kompetenser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Namn { get; set; }

        [ForeignKey(nameof(CV))]
        public string CVID { get; set; }
        public CV ettCV { get; set; }

    }
}
