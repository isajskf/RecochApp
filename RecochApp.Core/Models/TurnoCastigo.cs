using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecochApp.Core.Models
{
    public class TurnoCastigo
    {

        // Esta clase representa la relación entre un turno y un castigo en el contexto de un juego o aplicación. Cada instancia de TurnoCastigo asocia un turno específico con un castigo determinado, permitiendo gestionar las penalizaciones o consecuencias que se aplican durante el desarrollo del juego. La clase incluye propiedades para almacenar los identificadores del turno y del castigo, así como referencias a las entidades relacionadas, facilitando la navegación y manipulación de los datos en la aplicación.
        [Key]
        public int IdTurno { get; set; }
        public int IdCastigo { get; set; }
        public Turno? Turno { get; set; }
        public Castigo? Castigo { get; set; }
    }
}
