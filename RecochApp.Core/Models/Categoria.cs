using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Modelo de categoría para clasificar los contenidos en la aplicación. Cada categoría puede tener múltiples contenidos asociados, lo que permite organizar y filtrar los contenidos de manera eficiente.
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int IdCategoria { get; set; }

        public string Nombre { get; set; } = string.Empty;
    }
}