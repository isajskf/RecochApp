using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }

        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();
    }
}
