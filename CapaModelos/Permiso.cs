using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Permiso
    {
        public int Codigo { get; set; }
        public int CodigoRol { get; set; }
        public int CodigoSubmenu { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Rol _Rol { get; set; }
        public Submenu _Submenu { get; set; }
    }
}
