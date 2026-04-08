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

    // Controlador para gestionar las operaciones CRUD de la entidad ModoContenido.
    [Route("api/[controller]")]
    [ApiController]

    public class ModoContenidoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModoContenidoesController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/ModoContenidoes
        // Devuelve una lista de todos los modos de contenido disponibles en la base de datos.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModoContenido>>> GetModosContenidos()
        {
            return await _context.ModosContenidos.ToListAsync();
        }

        // GET: api/ModoContenidoes/5
        // Devuelve un modo de contenido específico basado en su ID. Si el modo de contenido no existe, devuelve un error 404.
        [HttpGet("{id}")]

        // El método recibe el ID del modo de contenido a obtener. Si el modo de contenido no se encuentra, devuelve un error 404 Not Found. Si se encuentra, devuelve el objeto ModoContenido correspondiente.
        public async Task<ActionResult<ModoContenido>> GetModoContenido(int id)
        {
            var modoContenido = await _context.ModosContenidos.FindAsync(id);

            if (modoContenido == null)
            {
                return NotFound();
            }

            return modoContenido;
        }

        // PUT: api/ModoContenidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un modo de contenido existente. El método recibe el ID del modo de contenido a actualizar y el objeto ModoContenido con los datos actualizados. Si el ID no coincide con el ID del objeto, devuelve un error 400 Bad Request. Si el modo de contenido no existe, devuelve un error 404 Not Found. Si la actualización es exitosa, devuelve un código de estado 204 No Content.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModoContenido(int id, ModoContenido modoContenido)
        {
            // Verifica que el ID proporcionado en la URL coincida con el ID del objeto ModoContenido. Si no coinciden, devuelve un error 400 Bad Request.
            if (id != modoContenido.IdModo)
            {
                return BadRequest();
            }

            // Marca el objeto ModoContenido como modificado en el contexto de la base de datos para que Entity Framework sepa que debe actualizarlo.
            _context.Entry(modoContenido).State = EntityState.Modified;

            // Intenta guardar los cambios en la base de datos. Si ocurre una excepción de concurrencia (DbUpdateConcurrencyException), verifica si el modo de contenido aún existe. Si no existe, devuelve un error 404 Not Found. Si existe, vuelve a lanzar la excepción para que sea manejada por el middleware de excepciones.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModoContenidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Si la actualización es exitosa, devuelve un código de estado 204 No Content para indicar que la operación se completó correctamente pero no hay contenido que devolver.
            return NoContent();
        }

        // POST: api/ModoContenidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo modo de contenido. El método recibe un objeto ModoContenido con los datos del nuevo modo de contenido a crear. Si la creación es exitosa, devuelve un código de estado 201 Created junto con el objeto ModoContenido creado.
        [HttpPost]
        public async Task<ActionResult<ModoContenido>> PostModoContenido(ModoContenido modoContenido)
        {
            _context.ModosContenidos.Add(modoContenido);

            // Intenta guardar los cambios en la base de datos. Si ocurre una excepción de actualización (DbUpdateException), verifica si el modo de contenido ya existe. Si existe, devuelve un error 409 Conflict. Si no existe, vuelve a lanzar la excepción para que sea manejada por el middleware de excepciones.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ModoContenidoExists(modoContenido.IdModo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // Si la creación es exitosa, devuelve un código de estado 201 Created junto con el objeto ModoContenido creado. El método CreatedAtAction se utiliza para generar una respuesta que incluye la ubicación del nuevo recurso creado.
            return CreatedAtAction("GetModoContenido", new { id = modoContenido.IdModo }, modoContenido);
        }

        // DELETE: api/ModoContenidoes/5
        // Elimina un modo de contenido existente. El método recibe el ID del modo de contenido a eliminar. Si el modo de contenido no existe, devuelve un error 404 Not Found. Si la eliminación es exitosa, devuelve un código de estado 204 No Content.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModoContenido(int id)
        {
            // Busca el modo de contenido en la base de datos utilizando el ID proporcionado. Si no se encuentra, devuelve un error 404 Not Found.
            var modoContenido = await _context.ModosContenidos.FindAsync(id);
            if (modoContenido == null)
            {
                return NotFound();
            }

            _context.ModosContenidos.Remove(modoContenido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un modo de contenido existe en la base de datos. Recibe el ID del modo de contenido y devuelve true si existe, o false si no existe.
        private bool ModoContenidoExists(int id)
        {
            return _context.ModosContenidos.Any(e => e.IdModo == id);
        }
    }
}
