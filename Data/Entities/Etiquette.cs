using System;

namespace WebApplicationAPI1.Data.Entities
{
    public class Etiquette
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String CodeBarre { get; set; }
        public bool IsExpedited { get; set; }
        public String Reference { get; set; }
        public String Profile { get; set; }
        public float Poids { get; set; }
        public int Quantite { get; set; }
        public DateTime DateReception { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public Location Location { get; set; }
        public Zone Zone { get; set; }
    }
}
