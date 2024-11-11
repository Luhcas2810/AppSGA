using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Submenu
    {
        public int Codigo { get; set; }
        public int CodigoMenu { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string Vista { get; set; }
        public string Icono { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Menu _Menu { get; set; }
    }
}
