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
    // Controlador para gestionar las operaciones CRUD de la entidad Participante.
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Participantes
        // Devuelve una lista de todos los participantes disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/Participantes/5
        // Devuelve un participante específico basado en su ID. Si el participante no existe, devuelve un error 404.
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);

            // Si no se encuentra el participante, se devuelve un error 404 Not Found.
            if (participante == null)
            {
                return NotFound();
            }

            return participante;
        }

        // PUT: api/Participantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un participante existente basado en su ID. Si el ID proporcionado no coincide con el ID del participante, devuelve un error 400. Si el participante no existe, devuelve un error 404.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipante(int id, Participante participante)
        {
            // Verifica que el ID proporcionado en la URL coincida con el ID del participante en el cuerpo de la solicitud. Si no coinciden, devuelve un error 400 Bad Request.
            if (id != participante.IdParticipante)
            {
                return BadRequest();
            }

            // Marca el participante como modificado en el contexto de la base de datos para que se actualice en la próxima llamada a SaveChangesAsync.
            _context.Entry(participante).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos. Si ocurre una excepción de concurrencia, verifica si el participante aún existe. Si no existe, devuelve un error 404 Not Found. Si existe, vuelve a lanzar la excepción.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(id))
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

        // POST: api/Participantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo participante en la base de datos. Después de crear el participante, devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipante", new { id = participante.IdParticipante }, participante);
        }

        // DELETE: api/Participantes/5
        // Elimina un participante específico basado en su ID. Si el participante no existe, devuelve un error 404. Si la eliminación es exitosa, devuelve un código de estado 204 No Content.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            // Busca el participante por su ID. Si no se encuentra, devuelve un error 404 Not Found.
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // Método auxiliar para verificar si un participante existe en la base de datos basado en su ID. Devuelve true si el participante existe, de lo contrario devuelve false.
        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.IdParticipante == id);
        }
    }
}
