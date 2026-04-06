using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class Contenido
    {
        public int IdContenido { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int IdCategoria { get; set; }

        public Categoria? Categoria { get; set; }
        public ICollection<ModoContenido> ModosContenidos { get; set; } = new List<ModoContenido>();
    }
}
