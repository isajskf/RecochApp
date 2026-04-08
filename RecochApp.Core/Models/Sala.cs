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
        public List<Participante> Participantes { get; set; } = new List<Participante>();
    }
}
