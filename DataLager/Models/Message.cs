using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Innehåll { get; set; }
        public DateOnly DatumOchTid { get; set; }

    }
}
