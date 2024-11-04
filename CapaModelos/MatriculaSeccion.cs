using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class MatriculaSeccion
    {
        public int Codigo { get; set; }
        public int CodigoMatricula { get; set; }
        public int CodigoSeccion { get; set; }
        public int Estado { get; set; }

        public Matricula _Matricula { get; set; }
        public Seccion _Seccion { get; set; }
    }
}
