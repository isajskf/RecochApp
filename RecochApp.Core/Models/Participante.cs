using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents a participant in a room, associating a user with a specific room context.
    /// </summary>
    /// <remarks>A participant links a user to a room, enabling tracking of user membership and participation
    /// within that room. The class provides references to both the user and the room entities. The associated room
    /// property is ignored during JSON serialization.</remarks>
    public class Participante
    {
        /// <summary>
        /// Gets or sets the unique identifier for the participant.
        /// </summary>
        public int IdParticipante { get; set; }
        
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the room.
        /// </summary>
        public int IdSala { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the current context.
        /// </summary>
        public Usuario? Usuario { get; set; }

        /// <summary>
        /// Gets or sets the associated room entity.
        /// </summary>
        [JsonIgnore]
        public Sala? Sala { get; set; }
    }
}
