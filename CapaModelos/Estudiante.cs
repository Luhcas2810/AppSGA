using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Estudiante
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoEscuela { get; set; }
        public Escuela _Escuela { get; set; }
        public Usuario _Usuario { get; set; }
    }
}
