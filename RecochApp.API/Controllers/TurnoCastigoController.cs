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
    // Controlador para gestionar las operaciones CRUD de la entidad TurnoCastigo.
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoCastigoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurnoCastigoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TurnoCastigoes
        // Devuelve una lista de todos los turnos de castigo disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnoCastigo>>> GetTurnoCastigo()
        {
            return await _context.TurnoCastigo.ToListAsync();
        }

        // GET: api/TurnoCastigoes/5
        // Devuelve un turno de castigo específico basado en su ID. Si no se encuentra, devuelve un error 404.
        [HttpGet("{id}")]
        public async Task<ActionResult<TurnoCastigo>> GetTurnoCastigo(int id)
        {
            var turnoCastigo = await _context.TurnoCastigo.FindAsync(id);

            if (turnoCastigo == null)
            {
                return NotFound();
            }

            return turnoCastigo;
        }

        // PUT: api/TurnoCastigoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un turno de castigo existente. Verifica que el ID en la URL coincida con el ID del objeto enviado. Si no coincide, devuelve un error 400. Si el turno de castigo no existe, devuelve un error 404.

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurnoCastigo(int id, TurnoCastigo turnoCastigo)
        {
            if (id != turnoCastigo.IdTurno)
            {
                return BadRequest();
            }

            _context.Entry(turnoCastigo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoCastigoExists(id))
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

        // POST: api/TurnoCastigoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo turno de castigo. El objeto TurnoCastigo se recibe en el cuerpo de la solicitud. Si la creación es exitosa, devuelve un código de estado 201 Created junto con el nuevo turno de castigo.
        [HttpPost]
        public async Task<ActionResult<TurnoCastigo>> PostTurnoCastigo(TurnoCastigo turnoCastigo)
        {
            _context.TurnoCastigo.Add(turnoCastigo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurnoCastigo", new { id = turnoCastigo.IdTurno }, turnoCastigo);
        }

        // DELETE: api/TurnoCastigoes/5
        // Elimina un turno de castigo específico basado en su ID. Si el turno de castigo no existe, devuelve un error 404. Si la eliminación es exitosa, devuelve un código de estado 204 No Content.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurnoCastigo(int id)
        {
            var turnoCastigo = await _context.TurnoCastigo.FindAsync(id);
            if (turnoCastigo == null)
            {
                return NotFound();
            }

            _context.TurnoCastigo.Remove(turnoCastigo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TurnoCastigoExists(int id)
        {
            return _context.TurnoCastigo.Any(e => e.IdTurno == id);
        }
    }
}
