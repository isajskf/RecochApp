using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RecochApp.Core.Models
{
    namespace RecochApp.Core.Models
    {
        public class Usuario
        {
            [Key]
            [Column("id_usuario")]
            public int IdUsuario { get; set; }

            [Column("nombre")]
            public string Nombre { get; set; }

            [Column("correo")]
            public string Correo { get; set; }

            [Column("password_hash")]
            public string PasswordHash { get; set; }

            [Column("fecha_nacimiento")]
            public DateTime FechaNacimiento { get; set; }
        }
    }
}