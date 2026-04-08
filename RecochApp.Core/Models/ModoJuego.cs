using System;
using System.Collections.Generic;
using System.Text;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents a game mode, including its identifier, name, player count, and duration.
    /// </summary>
    public class ModoJuego
    {
        /// <summary>
        /// Gets or sets the unique identifier for the mode.
        /// </summary>
        public int IdModo { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of players.
        /// </summary>
        public int CantidadJugadores { get; set; }

        /// <summary>
        /// Gets or sets the duration in minutes.
        /// </summary>
        public int TiempoMinutos { get; set; }
    }
}
