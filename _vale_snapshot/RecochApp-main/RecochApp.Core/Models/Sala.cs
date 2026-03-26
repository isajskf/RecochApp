using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class Sala
    {
        public int IdSala { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public int IdAnfitrion { get; set; }

        public Usuario? Anfitrion { get; set; }
        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}

