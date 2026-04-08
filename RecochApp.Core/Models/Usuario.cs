using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecochApp.Core.Models
{
    /// <summary>
    /// Represents a user account with identifying and contact information.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Gets or sets the name associated with the object.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address associated with the entity.
        /// </summary>

        public string Correo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed representation of the user's password.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        public DateTime FechaNacimiento { get; set; }
    }
}
