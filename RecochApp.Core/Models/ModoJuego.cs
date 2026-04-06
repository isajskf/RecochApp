using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class ModoJuego
    {
        public int IdModo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int CantidadJugadores { get; set; }
        public int TiempoMinutos { get; set; }

        public ICollection<ModoContenido> ModosContenidos { get; set; } = new List<ModoContenido>();
    }
}
