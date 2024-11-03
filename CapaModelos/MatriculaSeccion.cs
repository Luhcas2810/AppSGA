using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class MatriculaSeccion
    {
        public int IdMatriculaSeccion { get; set; } // Código de matrícula-sección
        public int IdMatricula { get; set; }        // Código de matrícula
        public int IdSeccion { get; set; }          // Código de sección

        public Matricula _Matricula { get; set; }   // Objeto Matricula
        public Seccion _Seccion { get; set; }       // Objeto Sección (AUN NO CREADO)
    }
}
