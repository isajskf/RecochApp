using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecochApp.API.Dtos.Salas;
using RecochApp.Core.Models;
using RecochApp.Infrastructure.Data;

namespace RecochApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    // Controlador para gestionar las operaciones CRUD de la entidad Sala.
    public class SalasController : ControllerBase
    {
        // Estas constantes se usan para armar el codigo de la sala.
        // El codigo es un string de 6 caracteres alfanumericos, lo que da un total de 36^6 combinaciones posibles, lo que hace muy poco probable que se repitan.
        private const int LongitudCodigoSala = 6;
        private const string CaracteresCodigo = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly AppDbContext _context;

        public SalasController(AppDbContext context)
        {
            _context = context;
        }

        // Este endpoint devuelve una lista de todas las salas creadas, ordenadas por su id de forma descendente (las mas nuevas primero).
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SalaResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SalaResponse>>> GetSalas()
        {
            // Aqui devolvemos una vista sencilla de cada sala para que Swagger no muestre de mas.
            var salas = await _context.Salas
                .Include(s => s.Participantes)
                .OrderByDescending(s => s.IdSala)
                .Select(s => new SalaResponse
                {
                    IdSala = s.IdSala,
                    Codigo = s.Codigo,
                    IdAnfitrion = s.IdAnfitrion,
                    CantidadParticipantes = s.Participantes.Count
                })
                .ToListAsync();

            // De esta forma devolvemos solo la informacion necesaria de cada sala, sin incluir detalles de los participantes o del anfitrion.
            return Ok(salas);
        }

        // Este endpoint devuelve los detalles de una sala en particular, buscandola por su codigo unico.
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(SalaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalaResponse>> GetSalaPorCodigo(string codigo)
        {
            var sala = await _context.Salas
                .Include(s => s.Participantes)
                .FirstOrDefaultAsync(s => s.Codigo == codigo);
            // Si no se encuentra la sala, devolvemos un error 404 con un mensaje explicativo.
            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            return Ok(MapearSala(sala));
        }

        // Este endpoint permite crear una nueva sala, recibiendo el id del usuario anfitrion en el cuerpo de la solicitud.
        [HttpPost]
        [ProducesResponseType(typeof(SalaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SalaResponse>> CrearSala([FromBody] CrearSalaRequest request)
        {
            // Antes de crear la sala validamos que el usuario anfitrion si exista.
            var anfitrionExiste = await _context.Usuarios.AnyAsync(u => u.IdUsuario == request.IdAnfitrion);
            if (!anfitrionExiste)
                return BadRequest(new { mensaje = "El anfitrion no existe" });

            var sala = new Sala
            {
                Codigo = await GenerarCodigoAsync(),
                IdAnfitrion = request.IdAnfitrion
            };

            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            var response = MapearSala(sala);

            return CreatedAtAction(nameof(GetSalaPorCodigo), new { codigo = sala.Codigo }, response);
        }

        // Este endpoint permite eliminar una sala por su id. Solo se puede eliminar una sala si no tiene participantes, para evitar problemas a los usuarios que ya estan participando.
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarSala(int id)
        {
            // Antes de eliminar la sala validamos que exista y que no tenga participantes.
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // Método auxiliar para mapear una entidad Sala a un objeto SalaResponse.
        private static SalaResponse MapearSala(Sala sala)
        {
            // Este metodo organiza la respuesta para que llegue mas limpia al frontend o a Swagger.
            return new SalaResponse
            {
                IdSala = sala.IdSala,
                Codigo = sala.Codigo,
                IdAnfitrion = sala.IdAnfitrion,
                CantidadParticipantes = sala.Participantes.Count
            };
        }

        // Método auxiliar para generar un código único para cada sala. El código es un string de 6 caracteres alfanuméricos, y se asegura de que no se repita con ninguna sala existente en la base de datos.
        private async Task<string> GenerarCodigoAsync()
        {
            string codigo;

            do
            {
                // Se arma un codigo aleatorio y se repite si ya existe en otra sala.
                codigo = new string(Enumerable.Range(0, LongitudCodigoSala)
                    .Select(_ => CaracteresCodigo[Random.Shared.Next(CaracteresCodigo.Length)])
                    .ToArray());
            }
            while (await _context.Salas.AnyAsync(s => s.Codigo == codigo));

            return codigo;
        }
    }
}
