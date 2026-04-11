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
            var salas = await _context.Salas
                .OrderByDescending(s => s.IdSala)
                .Select(s => new SalaResponse
                {
                    IdSala = s.IdSala,
                    Codigo = s.Codigo,
                    IdAnfitrion = s.IdAnfitrion,
                    IdModo = s.IdModo,
                    Estado = s.Estado,
                    MaxParticipantes = s.MaxParticipantes,
                    DuracionMinutos = s.DuracionMinutos,
                    CantidadParticipantes = _context.Participantes.Count(p => p.IdSala == s.IdSala)
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
                .FirstOrDefaultAsync(s => s.Codigo == codigo);

            if (sala == null)
                return NotFound(new { mensaje = "Sala no encontrada" });

            return Ok(await MapearSalaAsync(sala));
        }

        [HttpPost]
        [ProducesResponseType(typeof(SalaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SalaResponse>> CrearSala([FromBody] CrearSalaRequest request)
        {
            var anfitrionExiste = await _context.Usuarios.AnyAsync(u => u.IdUsuario == request.IdAnfitrion);
            if (!anfitrionExiste)
                return BadRequest(new { mensaje = "El anfitrion no existe" });

            var modoExiste = await _context.ModosJuego.AnyAsync(m => m.IdModo == request.IdModo);
            if (!modoExiste)
                return BadRequest(new { mensaje = "El modo de juego no existe" });

            var sala = new Sala
            {
                Codigo = await GenerarCodigoAsync(),
                IdAnfitrion = request.IdAnfitrion,
                IdModo = request.IdModo,
                MaxParticipantes = request.MaxParticipantes,
                DuracionMinutos = request.DuracionMinutos,
                Estado = "Espera"
            };

            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalaPorCodigo), new { codigo = sala.Codigo }, await MapearSalaAsync(sala));
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

        private async Task<SalaResponse> MapearSalaAsync(Sala sala)
        {
            var cantidadParticipantes = await _context.Participantes.CountAsync(p => p.IdSala == sala.IdSala);

            return new SalaResponse
            {
                IdSala = sala.IdSala,
                Codigo = sala.Codigo,
                IdAnfitrion = sala.IdAnfitrion,
                IdModo = sala.IdModo,
                Estado = sala.Estado,
                MaxParticipantes = sala.MaxParticipantes,
                DuracionMinutos = sala.DuracionMinutos,
                CantidadParticipantes = cantidadParticipantes
            };
        }

        private async Task<string> GenerarCodigoAsync()
        {
            string codigo;

            do
            {
                codigo = new string(Enumerable.Range(0, LongitudCodigoSala)
                    .Select(_ => CaracteresCodigo[Random.Shared.Next(CaracteresCodigo.Length)])
                    .ToArray());
            }
            while (await _context.Salas.AnyAsync(s => s.Codigo == codigo));

            return codigo;
        }
    }
}
