using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RecochApp.Core.Models
{
    public class Turno
    {
        [Key]
        [Column("id_turno")]
        public int IdTurno { get; set; }

        [Column("id_sala")]
        public int IdSala { get; set; }

        [Column("id_participante")]
        public int IdParticipante { get; set; }

        [Column("orden")]
        public int Orden { get; set; }

        [Column("estado")]
        public string Estado { get; set; } = "pendiente";

        public Sala? Sala { get; set; }
        public Participante? Participante { get; set; }
    }
}