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
    /// <summary>
    /// Represents an API controller that provides endpoints for managing Categoria entities, including operations to
    /// retrieve, create, update, and delete categories.
    /// </summary>
    /// <remarks>This controller exposes RESTful endpoints for interacting with the Categoria data in the
    /// underlying data store. All actions require appropriate HTTP verbs and route parameters as specified. The
    /// controller returns standard HTTP status codes to indicate the result of each operation. Thread safety and
    /// concurrency are managed by the underlying Entity Framework Core context.</remarks>
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Retrieves all categories from the data store.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see
        /// cref="ActionResult{T}">ActionResult</see> with a collection of <see cref="Categoria"/> objects representing
        /// all categories. Returns an empty collection if no categories exist.</returns>
        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        /// <summary>
        /// Retrieves the category with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to retrieve.</param>
        /// <returns>An ActionResult containing the category with the specified identifier if found; otherwise, a NotFound
        /// result.</returns>

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        /// <summary>
        /// Updates an existing Categoria entity with the specified values.
        /// </summary>
        /// <remarks>This method requires that the id parameter matches the IdCategoria property of the
        /// categoria parameter. If the Categoria does not exist, a NotFound result is returned. Concurrency conflicts
        /// may result in an exception being thrown.</remarks>
        /// <param name="id">The identifier of the Categoria to update. Must match the IdCategoria property of the provided categoria.</param>
        /// <param name="categoria">The Categoria entity containing the updated values. The IdCategoria property must match the id parameter.</param>
        /// <returns>An IActionResult indicating the result of the operation. Returns NoContent if the update is successful,
        /// BadRequest if the id does not match, or NotFound if the Categoria does not exist.</returns>
        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        /// <summary>
        /// Creates a new Categoria entity and adds it to the data store.
        /// </summary>
        /// <remarks>If the operation succeeds, the response includes a 201 Created status code and the
        /// created entity. The Location header points to the endpoint for retrieving the created Categoria by its
        /// identifier.</remarks>
        /// <param name="categoria">The Categoria entity to add. Must not be null.</param>
        /// <returns>An ActionResult containing the created Categoria entity and a Location header with the URI of the newly
        /// created resource.</returns>

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }

        /// <summary>
        /// Deletes the category with the specified identifier. 
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>An <see cref="IActionResult"/> that represents the result of the delete operation. Returns <see
        /// cref="NotFoundResult"/> if the category does not exist; otherwise, returns <see cref="NoContentResult"/>
        /// upon successful deletion.</returns>

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Determines whether a category with the specified identifier exists in the data store.
        /// </summary>
        /// <param name="id">The identifier of the category to locate.</param>
        /// <returns>true if a category with the specified identifier exists; otherwise, false.</returns>

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id);
        }
    }
}
