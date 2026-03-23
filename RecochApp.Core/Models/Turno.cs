using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class Turno
    {
        public int IdTurno { get; set; }
        public int IdSala { get; set; }
        public int IdParticipante { get; set; }
        public int Orden { get; set; }
        public string Estado { get; set; } = "pendiente";

        public Sala? Sala { get; set; }
        public Participante? Participante { get; set; }
    }
}
