using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecochApp.API.Dtos.Salas;
using RecochApp.Core.Models;
using RecochApp.Infrastructure.Data;

namespace RecochApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalasController : ControllerBase
    {
        // Estas constantes se usan para armar el codigo de la sala.
        private const int LongitudCodigoSala = 6;
        private const string CaracteresCodigo = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly AppDbContext _context;

        public SalasController(AppDbContext context)
        {
            _context = context;
        }

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

            return Ok(salas);
        }

        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(SalaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SalaResponse>> GetSalaPorCodigo(string codigo)
        {
            var sala = await _context.Salas
                .Include(s => s.Participantes)
                .FirstOrDefaultAsync(s => s.Codigo == codigo);

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            return Ok(MapearSala(sala));
        }

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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

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
