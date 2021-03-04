using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplicationAPI1.Controllers.Filter;
using WebApplicationAPI1.Controllers.Wrapper;
using WebApplicationAPI1.Data;
using WebApplicationAPI1.Data.Entities;
using WebApplicationAPI1.Models;

namespace WebApplicationAPI1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EtiquettesController : ControllerBase
    {
        private readonly EtiquetteRepository _repository;
        private readonly IMapper _mapper;

        public EtiquettesController(EtiquetteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Etiquettes
        [HttpGet]
        public async Task<ActionResult<EtiquetteModel[]>> GetEtiquettes([FromQuery] PaginationFilter filter, String pays = null)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            // Pays est un optional query string => /api/etiquettes?pays=Chine
            var results = await _repository.GetEtiquettesByPaysAsync(pays, validFilter);

            EtiquetteModel[] models = _mapper.Map<EtiquetteModel[]>(results);

            return Ok(new PagedResponse<EtiquetteModel[]>(models, models.Length, filter.PageNumber, filter.PageSize));
        }

        // GET: api/Etiquettes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EtiquetteModel>> GetEtiquette(long id)
        {
            var Etiquette = await _repository.GetEtiquettesByIdAsync(id);

            if (Etiquette == null)
            {
                return NotFound();
            }

            return _mapper.Map<EtiquetteModel>(Etiquette);
        }

        // PUT: api/Etiquettes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtiquette(long id, EtiquetteModel model)
        {
            var oldEtiquette = await _repository.GetEtiquettesByIdAsync(id);
            if (id != model.Id || oldEtiquette == null)
            {
                return BadRequest();
            }

            try
            {
                _mapper.Map(model, oldEtiquette);
                await _repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.EtiquetteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/Etiquettes
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EtiquetteModel>> PostEtiquette(EtiquetteModel model)
        {
            var etiquette = _mapper.Map<Etiquette>(model);
            _repository.Add(etiquette);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEtiquette), new { id = etiquette.Id }, _mapper.Map<EtiquetteModel>(etiquette));
        }

        /// <summary>
        /// Supprime une Etiquette à partir de son identifiant.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Etiquettes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEtiquette(long id)
        {
            var Etiquette = await _repository.GetEtiquettesByIdAsync(id);
            if (Etiquette == null)
            {
                return NotFound();
            }

            _repository.Delete(Etiquette);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
