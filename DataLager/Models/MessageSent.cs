using DataLager.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class MessageSent
    {
        //public int id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string MottagareID { get; set; }
        [ForeignKey(nameof(Message))]
        public string MeddelandeID { get; set; }
        public string Avsändare { get; set; }
        public bool Läst { get; set; }
        public ApplicationUser User { get; set; }
        public Message Message { get; set; }


    }
}
