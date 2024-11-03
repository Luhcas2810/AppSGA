using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Seccion
    {
        public int IdSeccion { get; set; }      // Código de la sección
        public int IdCurso { get; set; }        // Código del curso
        public int IdDocente { get; set; }      // Código del docente
        public int IdAula { get; set; }         // Código del aula
        public string Numero { get; set; }      // Número de la sección
        public string Horario { get; set; }     // Horario de la sección
        public string HoraInicio { get; set; }  // Hora de inicio de la sección

        public Curso _Curso { get; set; }       // Objeto Curso
        public Docente _Docente { get; set; }   // Objeto Docente
        public Aula _Aula { get; set; }         // Objeto Aula
    }
}
