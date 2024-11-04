using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Calificacion
    {
        public int Codigo { get; set; }
        public int CodigoEstudiante { get; set; }
        public int CodigoSeccion { get; set; }
        public string Tipo { get; set; }
        public int ValorCalificacion { get; set; }

        public Estudiante _Estudiante { get; set; }
        public Seccion _Seccion { get; set; }
    }
}
