namespace WebApplicationAPI1.Data.Entities
{
    public class Location
    {
        public long Id { get; set; }
        public string Nom { get; set; }
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string Pays { get; set; }
    }
}
