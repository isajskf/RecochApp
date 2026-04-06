using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecochApp.Core.Models
{
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("correo")]
        public string Correo { get; set; } = string.Empty;

        [Column("password_hash")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();
        public ICollection<Sala> SalasCreadas { get; set; } = new List<Sala>();
    }
}
