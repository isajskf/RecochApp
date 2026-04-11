using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecochApp.Core.Models;
using RecochApp.Infrastructure.Data;

namespace RecochApp.API.Controllers
{
    // Controlador para gestionar las operaciones CRUD de la entidad Castigo.
    [Route("api/[controller]")]
    [ApiController]
    public class CastigoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CastigoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Castigo
        // Devuelve una lista de todos los castigos disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Castigo>>> GetCastigos()
        {
            return await _context.Castigos.ToListAsync();
        }

        // GET: api/Castigo/5
        // Devuelve un castigo específico basado en su ID. Si el castigo no existe, devuelve un error 404.
        [HttpGet("{id}")]
        public async Task<ActionResult<Castigo>> GetCastigo(int id)
        {
            var castigo = await _context.Castigos.FindAsync(id);

            if (castigo == null)
            {
                return NotFound();
            }

            return castigo;
        }

        // PUT: api/Castigo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un castigo existente basado en su ID. Si el ID del castigo no coincide con el ID proporcionado, devuelve un error 400.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCastigo(int id, Castigo castigo)
        {
            if (id != castigo.IdCastigo)
            {
                return BadRequest();
            }

            _context.Entry(castigo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastigoExists(id))
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

        // POST: api/Castigoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo castigo en la base de datos. Devuelve el castigo creado junto con su ID asignado.
        [HttpPost]
        public async Task<ActionResult<Castigo>> PostCastigo(Castigo castigo)
        {
            _context.Castigos.Add(castigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCastigo", new { id = castigo.IdCastigo }, castigo);
        }

        // DELETE: api/Castigoes/5
        // Elimina un castigo específico basado en su ID. Si el castigo no existe, devuelve un error 404.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCastigo(int id)
        {
            var castigo = await _context.Castigos.FindAsync(id);
            if (castigo == null)
            {
                return NotFound();
            }

            _context.Castigos.Remove(castigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un castigo existe en la base de datos basado en su ID.
        private bool CastigoExists(int id)
        {
            return _context.Castigos.Any(e => e.IdCastigo == id);
        }
    }
}
