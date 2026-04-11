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
    // Controlador para gestionar las operaciones CRUD de la entidad Turno.
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurnoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Turnoes
        // Devuelve una lista de todos los turnos disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurnos()
        {
            return await _context.Turnos.ToListAsync();
        }

        // GET: api/Turnoes/5
        // Devuelve un turno específico basado en su ID. Si el turno no existe, devuelve un error 404.
        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> GetTurno(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);

            // Si no se encuentra el turno, se devuelve un error 404 Not Found.
            if (turno == null)
            {
                return NotFound();
            }

            return turno;
        }

        // PUT: api/Turnoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un turno existente basado en su ID. Si el ID proporcionado no coincide con el ID del turno, devuelve un error 400. Si el turno no existe, devuelve un error 404.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurno(int id, Turno turno)
        {
            // Verifica que el ID proporcionado en la URL coincida con el ID del turno en el cuerpo de la solicitud. Si no coinciden, se devuelve un error 400 Bad Request.
            if (id != turno.IdTurno)
            {
                return BadRequest();
            }

            _context.Entry(turno).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos. Si ocurre una excepción de concurrencia, verifica si el turno existe. Si no existe, devuelve un error 404 Not Found. Si existe, vuelve a lanzar la excepción.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TurnoExists(id))
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

        // POST: api/Turnoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo turno en la base de datos. El método recibe un objeto Turno en el cuerpo de la solicitud. Si la creación es exitosa, devuelve un código de estado 201 Created junto con el nuevo turno creado.
        [HttpPost]
        public async Task<ActionResult<Turno>> PostTurno(Turno turno)
        {
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTurno", new { id = turno.IdTurno }, turno);
        }

        // DELETE: api/Turnoes/5
        // Elimina un turno específico basado en su ID. Si el turno no existe, devuelve un error 404. Si la eliminación es exitosa, devuelve un código de estado 204 No Content.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurno(int id)
        {
            // Busca el turno en la base de datos utilizando su ID. Si no se encuentra, se devuelve un error 404 Not Found.
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un turno existe en la base de datos basado en su ID. Devuelve true si el turno existe, de lo contrario, devuelve false.
        private bool TurnoExists(int id)
        {
            return _context.Turnos.Any(e => e.IdTurno == id);
        }
    }
}
