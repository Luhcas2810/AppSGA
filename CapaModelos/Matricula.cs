using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Matricula
    {
        public int Codigo { get; set; }
        public int CodigoEstudiante { get; set; }
        public int CodigoPeriodo { get; set; }

        public Estudiante _Estudiante { get; set; }
        public Periodo _Periodo { get; set; }
    }
}
