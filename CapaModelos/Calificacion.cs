using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Calificacion
    {
        public int IdCalificacion { get; set; } // Código de calificación
        public int IdEstudiante { get; set; }   // Código de estudiante
        public int IdSeccion { get; set; }      // Código de sección
        public string Tipo { get; set; }        // Tipo de calificación
        public int ValorCalificacion { get; set; } // Calificación en número

        public Estudiante _Estudiante { get; set; } // Objeto Estudiante
        public Seccion _Seccion { get; set; }       // Objeto Sección
    }
}
