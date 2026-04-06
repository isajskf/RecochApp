using System.ComponentModel.DataAnnotations;

namespace RecochApp.API.Dtos.Salas
{
    public class CrearSalaRequest
    {
        [Required]
        public int IdAnfitrion { get; set; }
    }
}
