﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI1.Data;
using WebApplicationAPI1.Models;

namespace WebApplicationAPI1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EtiquettesController : ControllerBase
    {
        private readonly EtiquetteRepository _repository;

        public EtiquettesController(EtiquetteRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Etiquettes
        [HttpGet]
        public async Task<IActionResult> GetEtiquettes()
        {
            try
            {
                var results = await _repository.GetAllEtiquettesAsync();

                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error lors de la requête à la base de données");
            }
        }

        // GET: api/Etiquettes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Etiquette>> GetEtiquette(long id)
        {
            var Etiquette = await _repository.GetEtiquettesByIdAsync(id);

            if (Etiquette == null)
            {
                return NotFound();
            }

            return Ok(Etiquette);
        }

        // PUT: api/Etiquettes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEtiquette(long id, Etiquette Etiquette)
        {
            if (id != Etiquette.Id)
            {
                return BadRequest();
            }

            try
            {
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
        public async Task<ActionResult<Etiquette>> PostEtiquette(Etiquette Etiquette)
        {
            _repository.Add(Etiquette);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEtiquette), new { id = Etiquette.Id }, Etiquette);
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