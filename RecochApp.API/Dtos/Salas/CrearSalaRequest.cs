using System.ComponentModel.DataAnnotations;

namespace RecochApp.API.Dtos.Salas
{
    /// <summary>
    /// DTO para la creación de una nueva sala.
    /// </summary>
    public class CrearSalaRequest
    {
        [Required]
        public int IdAnfitrion { get; set; }

        [Required]
        public int IdModo { get; set; }

        public int MaxParticipantes { get; set; } = 8;

        public int? DuracionMinutos { get; set; }
    }
}
