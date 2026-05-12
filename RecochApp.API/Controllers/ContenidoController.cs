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

    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de la entidad Contenido.
    /// </summary>
    public class ContenidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContenidoController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Contenidoes
        // Devuelve una lista de todos los contenidos disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contenido>>> GetContenidos()
        {
            return await _context.Contenidos.ToListAsync();
        }

        // GET: api/Contenidoes/5
        // Devuelve un contenido específico basado en su ID. Si el contenido no existe, devuelve un error 404.
        [HttpGet("{id}")]
        public async Task<ActionResult<Contenido>> GetContenido(int id)
        {
            var contenido = await _context.Contenidos.FindAsync(id);

            // Si el contenido no se encuentra, devuelve un error 404 Not Found.
            if (contenido == null)
            {
                return NotFound();
            }

            return contenido;
        }

        // PUT: api/Contenidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un contenido existente basado en su ID. Si el ID del contenido no coincide con el ID proporcionado, devuelve un error 400. Si el contenido no existe, devuelve un error 404.
        [HttpPut("{id}")]

        // El método recibe el ID del contenido a actualizar y el objeto Contenido con los nuevos datos. Si el ID no coincide, devuelve un error 400 Bad Request. Si el contenido no existe, devuelve un error 404 Not Found. Si la actualización es exitosa, devuelve un código de estado 204 No Content.
        public async Task<IActionResult> PutContenido(int id, Contenido contenido)
        {

            // Verifica si el ID proporcionado coincide con el ID del contenido a actualizar. Si no coinciden, devuelve un error 400 Bad Request.
            if (id != contenido.IdContenido)
            {
                return BadRequest();
            }

            // Marca el contenido como modificado en el contexto de la base de datos para que se actualice en la próxima llamada a SaveChangesAsync.
            _context.Entry(contenido).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos. Si ocurre una excepción de concurrencia, verifica si el contenido existe. Si no existe, devuelve un error 404 Not Found. Si existe, vuelve a lanzar la excepción.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContenidoExists(id))
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

        // POST: api/Contenidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo contenido en la base de datos. Recibe un objeto Contenido en el cuerpo de la solicitud. Si la creación es exitosa, devuelve un código de estado 201 Created junto con el contenido creado.
        [HttpPost]
        public async Task<ActionResult<Contenido>> PostContenido(Contenido contenido)
        {
            // Agrega el nuevo contenido al contexto de la base de datos y guarda los cambios. Luego, devuelve un código de estado 201 Created junto con el contenido creado.
            _context.Contenidos.Add(contenido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContenido", new { id = contenido.IdContenido }, contenido);
        }

        // DELETE: api/Contenidoes/5
        // Elimina un contenido específico basado en su ID. Si el contenido no existe, devuelve un error 404. Si la eliminación es exitosa, devuelve un código de estado 204 No Content.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContenido(int id)
        {
            // Busca el contenido a eliminar en la base de datos. Si no se encuentra, devuelve un error 404 Not Found. Si se encuentra, elimina el contenido y guarda los cambios en la base de datos. Luego, devuelve un código de estado 204 No Content.
            var contenido = await _context.Contenidos.FindAsync(id);
            if (contenido == null)
            {
                return NotFound();
            }

            // Elimina el contenido del contexto de la base de datos y guarda los cambios.
            _context.Contenidos.Remove(contenido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un contenido existe en la base de datos basado en su ID. Devuelve true si el contenido existe, de lo contrario, devuelve false.
        private bool ContenidoExists(int id)
        {
            return _context.Contenidos.Any(e => e.IdContenido == id);
        }
    }
}
