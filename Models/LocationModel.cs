using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI1.Models
{
    public class LocationModel
    {
        public long Id { get; set; }
        public string Nom { get; set; }
        [Required]
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Pays { get; set; }
    }
}
