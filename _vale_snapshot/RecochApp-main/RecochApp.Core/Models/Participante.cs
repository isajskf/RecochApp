using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class Participante
    {
        public int IdParticipante { get; set; }
        public int IdUsuario { get; set; }
        public int IdSala { get; set; }

        public Usuario? Usuario { get; set; }
        public Sala? Sala { get; set; }
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
