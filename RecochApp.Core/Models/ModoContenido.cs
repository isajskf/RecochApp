using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents the association between a game mode and its related content.
    /// </summary>
    /// <remarks>This class is typically used to link a specific game mode to a particular content item,
    /// enabling scenarios where content is organized or filtered by mode. Both the game mode and content references are
    /// optional and may be null if not set.</remarks>
    public class ModoContenido
    {
        /// <summary>
        /// Gets or sets the unique identifier for the mode.
        /// </summary>
        public int IdModo { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the content.
        /// </summary>
        public int IdContenido { get; set; }

        /// <summary>
        /// Gets or sets the current game mode.
        /// </summary>
        public ModoJuego? ModoJuego { get; set; }

        /// <summary>
        /// Gets or sets the content associated with this instance.
        /// </summary>
        public Contenido? Contenido { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the category.
        /// </summary>
        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
