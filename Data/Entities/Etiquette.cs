using System;
using WebApplicationAPI1.Data.Entities;

namespace WebApplicationAPI1.Models
{
    public class Etiquette
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String CodeBarre { get; set; }
        public bool IsComplete { get; set; }
        public int Poids { get; set; }
        public int Quantite { get; set; }
        public DateTime DateReception { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public Location Location { get; set; }
    }
}
