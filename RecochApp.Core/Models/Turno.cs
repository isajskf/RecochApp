using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents a turn assigned to a participant within a room.
    /// </summary>
    /// <remarks>A turn associates a participant with a room and defines the order and status of their
    /// participation. The related Sala and Participante navigation properties are ignored during JSON
    /// serialization.</remarks>
    public class Turno
    {
        /// <summary>
        /// Gets or sets the unique identifier for the shift.
        /// </summary>
        public int IdTurno { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the room.    
        /// </summary>
        public int IdSala { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the participant.
        /// </summary>
        public int IdParticipante { get; set; }

        /// <summary>
        /// Gets or sets the order index for the item.
        /// </summary>
        public int Orden { get; set; }

        /// <summary>
        /// Gets or sets the current status of the entity.
        /// </summary>
        public string Estado { get; set; } = "pendiente";

        /// <summary>
        /// Gets or sets the associated room entity.
        /// </summary>
        [JsonIgnore]
        public Sala? Sala { get; set; }

        /// <summary>
        /// Gets or sets the participant associated with this entity.   
        /// </summary>
        [JsonIgnore]
        public Participante? Participante { get; set; }

    }
}