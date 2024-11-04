using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Docente
    {
        public int CodigoDocente { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoDepartamento { get; set; }

        public Usuario _Usuario { get; set; }
        public Departamento _Departamento { get; set; }
    }
}
