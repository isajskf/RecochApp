using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Contenido> Contenidos { get; set; } = new List<Contenido>();
    }
}