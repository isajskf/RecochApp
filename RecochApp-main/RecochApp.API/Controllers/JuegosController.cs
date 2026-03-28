using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecochApp.Infrastructure.Data;
using RecochApp.Core.Models;

namespace RecochApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuegosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JuegosController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 UNIRSE A SALA
        [HttpPost("unirse")]
        public async Task<IActionResult> UnirseASala([FromQuery] string codigo, [FromQuery] int idUsuario)
        {
            var sala = await _context.Salas
                .FirstOrDefaultAsync(s => s.Codigo == codigo);

            if (sala == null)
                return NotFound("Sala no encontrada");

            var participante = new Participante
            {
                IdUsuario = idUsuario,
                IdSala = sala.IdSala
            };

            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();

            return Ok("Te uniste a la sala");
        }

        // 🔹 CREAR TURNOS
        [HttpPost("crear-turnos/{idSala}")]
        public async Task<IActionResult> CrearTurnos(int idSala)
        {
            var participantes = await _context.Participantes
                .Where(p => p.IdSala == idSala)
                .ToListAsync();

            if (!participantes.Any())
                return BadRequest("No hay participantes");

            int orden = 1;

            foreach (var p in participantes)
            {
                var turno = new Turno
                {
                    IdSala = idSala,
                    IdParticipante = p.IdParticipante,
                    Orden = orden,
                    Estado = "pendiente"
                };

                _context.Turnos.Add(turno);
                orden++;
            }

            await _context.SaveChangesAsync();

            return Ok("Turnos creados");
        }
    }
}