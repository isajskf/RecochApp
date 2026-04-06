using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecochApp.Core.Models
{
    public class Sala
    {
        [Key]
        [Column("id_sala")]
        public int IdSala { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; } = string.Empty;

        [Column("id_anfitrion")]
        public int IdAnfitrion { get; set; }

        public Usuario? Anfitrion { get; set; }
        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
