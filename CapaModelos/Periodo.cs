using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Periodo
    {
        public int IdPeriodo { get; set; }      // Código del período
        public string Nombre { get; set; }      // Nombre del período (ej. 2024_2)
        public DateTime FechaInicio { get; set; } // Fecha de inicio del período
        public DateTime FechaFin { get; set; }    // Fecha de fin del período
    }
}
