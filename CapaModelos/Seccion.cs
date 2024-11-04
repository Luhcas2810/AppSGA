using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Seccion
    {
        public int Codigo { get; set; }
        public int CodigoCurso { get; set; }
        public int CodigoDocente { get; set; }
        public int CodigoAula { get; set; }
        public string Numero { get; set; }
        public string Horario { get; set; }
        public string HoraInicio { get; set; }

        public Curso _Curso { get; set; }
        public Docente _Docente { get; set; }
        public Aula _Aula { get; set; }
    }
}
