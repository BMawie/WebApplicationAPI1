using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI1.Models
{
    public class EtiquetteModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public String Name { get; set; }
        [Required]
        public String CodeBarre { get; set; }

        [DefaultValue(false)]
        public bool IsComplete { get; set; }
        public float Poids { get; set; }
        public int Quantite { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReception { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;

        public String ZoneCode { get; set; }

        public LocationModel Location { get; set; }
    }
}
