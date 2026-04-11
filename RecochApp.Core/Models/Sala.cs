using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents a room with a unique identifier, code, and associated host user.
    /// </summary>
    public class Sala
    {
        /// <summary>
        /// Gets or sets the unique identifier for the room.
        /// </summary>
        public int IdSala { get; set; }

        /// <summary>
        /// Gets or sets the code associated with the entity.   
        /// </summary>
        public string Codigo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier for the host.
        /// </summary>
        public int IdAnfitrion { get; set; }

        /// <summary>
        /// Gets or sets the host user associated with the entity.
        /// </summary>
        public Usuario? Anfitrion { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the game mode.
        /// </summary>
        public int IdModo { get; set; }

        /// <summary>
        /// Gets or sets the current state of the room.
        /// </summary>
        public string Estado { get; set; } = "Espera";

        /// <summary>
        /// Gets or sets the maximum number of participants allowed in the room.
        /// </summary>
        public int MaxParticipantes { get; set; }

        /// <summary>
        /// Gets or sets the duration of the room in minutes.
        /// </summary>
        public int? DuracionMinutos { get; set; }

        /// <summary>
        /// Gets or sets the game mode associated with the room.
        /// </summary>
        public ModoJuego? ModoJuego { get; set; }

    }
}
