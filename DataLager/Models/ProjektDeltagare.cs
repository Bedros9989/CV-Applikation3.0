using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataLager.Areas.Identity.Data;

namespace Core.Models
{
    public class ProjektDeltagare
    {
        public string UserId { get; set; }
        public string ProjectId { get; set; }

        public ApplicationUser User { get; set; }
        public Project Project { get; set; }
    }
}
