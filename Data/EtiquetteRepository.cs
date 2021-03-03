using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI1.Controllers.Filter;
using WebApplicationAPI1.Data.Entities;

namespace WebApplicationAPI1.Data
{
    public class EtiquetteRepository
    {
        private readonly EtiquetteContext _context;
        private readonly ILogger<EtiquetteRepository> _logger;

        public EtiquetteRepository(EtiquetteContext context, ILogger<EtiquetteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Etiquette[]> GetAllEtiquettesByDateCreation(DateTime dateTime)
        {
            _logger.LogInformation($"Getting all Etiquettes");

            IQueryable<Etiquette> query = _context.Etiquettes.Include(c => c.Location);

            // Tri
            query = query.OrderByDescending(c => c.DateCreation)
              .Where(c => c.DateCreation.Date == dateTime.Date);

            return await query.ToArrayAsync();
        }

        public async Task<Etiquette[]> GetAllEtiquettesAsync()
        {
            _logger.LogInformation($"Getting all Etiquettes");

            IQueryable<Etiquette> query = _context.Etiquettes.Include(c => c.Location);

            // Tri par défaut
            query = query.OrderByDescending(c => c.DateCreation);

            return await query.ToArrayAsync();
        }


        public async Task<Etiquette> GetEtiquettesByIdAsync(long id)
        {
            _logger.LogInformation($"Getting all Etiquette par ID");

            IQueryable<Etiquette> query = _context.Etiquettes.Include(c => c.Location);

            // Add Query
            query = query
              .Where(t => t.Id.Equals(id));

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Etiquette[]> GetEtiquettesByCodeBarreAsync(string codeBarre)
        {
            _logger.LogInformation($"Getting all Etiquette par code barre");

            IQueryable<Etiquette> query = _context.Etiquettes.Include(c => c.Location);

            // Add Query
            query = query
              .Where(t => t.CodeBarre == codeBarre)
              .OrderByDescending(t => t.DateCreation);

            return await query.ToArrayAsync();
        }
        public async Task<Etiquette[]> GetEtiquettesByPaysAsync(string pays, PaginationFilter filter)
        {
            _logger.LogInformation($"Getting all Etiquette par pays de location");

            IQueryable<Etiquette> query = _context.Etiquettes.Include(c => c.Location);

            // Add Query
            if (pays == null)
                query = query.OrderByDescending(t => t.DateCreation);
            else
                query = query
                  .Where(t => t.Location.Pays == pays)
                    .OrderByDescending(t => t.DateCreation);

            return await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToArrayAsync();
        }

        public bool EtiquetteExists(long id)
        {
            return _context.Etiquettes.Any(e => e.Id == id);
        }
    }
}
