using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecochApp.Infrastructure.Data;

namespace RecochApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TurnosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("actual/{idSala}")]
        public async Task<IActionResult> ObtenerTurnoActual(int idSala)
        {
            var turno = await _context.Turnos
                .Where(t => t.IdSala == idSala && t.Estado == "pendiente")
                .OrderBy(t => t.Orden)
                .FirstOrDefaultAsync();

            if (turno == null)
                return NotFound("No hay turnos");

            return Ok(turno);
        }

        [HttpPost("siguiente/{idTurno}")]
        public async Task<IActionResult> SiguienteTurno(int idTurno)
        {
            var turno = await _context.Turnos.FindAsync(idTurno);

            if (turno == null)
                return NotFound();

            turno.Estado = "completado";

            await _context.SaveChangesAsync();

            return Ok("Turno completado");
        }
    }
}