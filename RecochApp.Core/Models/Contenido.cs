using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents a content item with associated text, type, and category information.
    /// </summary>
    /// <remarks>This class is typically used to model content entities within a content management or
    /// categorization system. The associated category can be accessed through the Categoria property, which may be null
    /// if no category is assigned.</remarks>
    public class Contenido
    {
        /// <summary>
        /// Gets or sets the unique identifier for the content.
        /// </summary>
        public int IdContenido { get; set; }

        /// <summary>
        /// Gets or sets the text content associated with this instance.
        /// </summary>
        public string Texto { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type associated with the current instance.
        /// </summary>
        public string Tipo { get; set; } = string.Empty;

        public int IdCategoria { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
