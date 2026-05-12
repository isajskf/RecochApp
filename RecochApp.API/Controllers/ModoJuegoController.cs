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
    // Controlador para gestionar las operaciones CRUD de la entidad ModoJuego.
    [Route("api/[controller]")]
    [ApiController]

    public class ModoJuegoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModoJuegoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ModoJuegoes
        // Devuelve una lista de todos los modos de juego disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModoJuego>>> GetModosJuego()
        {
            return await _context.ModosJuego.ToListAsync();
        }

        // GET: api/ModoJuegoes/5
        // Devuelve un modo de juego específico basado en su ID. Si el modo de juego no existe, devuelve un error 404.
        [HttpGet("{id}")]
        public async Task<ActionResult<ModoJuego>> GetModoJuego(int id)
        {
            var modoJuego = await _context.ModosJuego.FindAsync(id);

            // Si no se encuentra el modo de juego, devuelve un error 404 Not Found.
            if (modoJuego == null)
            {
                return NotFound();
            }

            // Si se encuentra el modo de juego, lo devuelve en la respuesta.
            return modoJuego;
        }

        // PUT: api/ModoJuegoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un modo de juego existente basado en su ID. Si el ID proporcionado no coincide con el ID del modo de juego, devuelve un error 400. Si el modo de juego no existe, devuelve un error 404.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModoJuego(int id, ModoJuego modoJuego)
        {

            // Verifica que el ID proporcionado en la URL coincida con el ID del modo de juego en el cuerpo de la solicitud. Si no coinciden, devuelve un error 400 Bad Request.
            if (id != modoJuego.IdModo)
            {
                return BadRequest();
            }

            _context.Entry(modoJuego).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos. Si ocurre una excepción de concurrencia, verifica si el modo de juego existe. Si no existe, devuelve un error 404 Not Found. Si existe, vuelve a lanzar la excepción.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModoJuegoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Si la actualización es exitosa, devuelve un código de estado 204 No Content, indicando que la solicitud se ha procesado correctamente pero no hay contenido para devolver.
            return NoContent();
        }

        // POST: api/ModoJuegoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo modo de juego. El modo de juego se agrega a la base de datos y se devuelve una respuesta con el nuevo recurso creado.
        [HttpPost]
        public async Task<ActionResult<ModoJuego>> PostModoJuego(ModoJuego modoJuego)
        {
            _context.ModosJuego.Add(modoJuego);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModoJuego", new { id = modoJuego.IdModo }, modoJuego);
        }

        // DELETE: api/ModoJuegoes/5
        // Elimina un modo de juego específico basado en su ID. Si el modo de juego no existe, devuelve un error 404. Si la eliminación es exitosa, devuelve un código de estado 204 No Content.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModoJuego(int id)
        {
            // Busca el modo de juego en la base de datos utilizando su ID. Si no se encuentra, devuelve un error 404 Not Found.
            var modoJuego = await _context.ModosJuego.FindAsync(id);
            if (modoJuego == null)
            {
                return NotFound();
            }

            // Si se encuentra el modo de juego, lo elimina de la base de datos y guarda los cambios. Luego devuelve un código de estado 204 No Content, indicando que la eliminación fue exitosa pero no hay contenido para devolver.
            _context.ModosJuego.Remove(modoJuego);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un modo de juego existe en la base de datos basado en su ID. Devuelve true si el modo de juego existe, de lo contrario devuelve false.
        private bool ModoJuegoExists(int id)
        {
            return _context.ModosJuego.Any(e => e.IdModo == id);
        }
    }
}
