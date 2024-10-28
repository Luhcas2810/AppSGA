using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string _Usuario { get; set; }
        public string Contrasenia { get; set; }
        public int Estado { get; set; }
        public int IdRol { get; set; }
        public Rol _Rol { get; set; }
    }
}
