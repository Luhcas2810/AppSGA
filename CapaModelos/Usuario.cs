using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public int CodigoRol { get; set; }
        public string _Usuario { get; set; }
        public string Contrasenia { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Rol _Rol { get; set; }
    }
}
