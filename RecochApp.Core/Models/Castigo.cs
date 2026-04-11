using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RecochApp.Core.Models
{
    // Represents a penalty or punishment that can be associated with a turn in the game. Each Castigo has a unique identifier and a description of the penalty.
    public class Castigo
        {
            [Key]
            public int IdCastigo { get; set; }
            public string Descripcion { get; set; } = string.Empty;
        }
}
