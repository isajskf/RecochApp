using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class ModoContenido
    {
        public int IdModo { get; set; }
        public int IdContenido { get; set; }

        public ModoJuego? ModoJuego { get; set; }
        public Contenido? Contenido { get; set; }
    }
}
