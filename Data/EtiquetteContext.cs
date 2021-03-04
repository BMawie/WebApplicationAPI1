using Microsoft.EntityFrameworkCore;
using WebApplicationAPI1.Data.Entities;

namespace WebApplicationAPI1.Data
{
    public class EtiquetteContext : DbContext
    {
        public EtiquetteContext(DbContextOptions<EtiquetteContext> options)
           : base(options)
        {
        }

        public DbSet<Etiquette> Etiquettes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}
