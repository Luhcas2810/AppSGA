using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Matricula
    {
        public int IdMatricula { get; set; } // Código de matrícula
        public int IdEstudiante { get; set; } // Código de estudiante
        public int IdPeriodo { get; set; }    // Código de período

        public Estudiante _Estudiante { get; set; } // para llamar su nombre
        public Periodo _Periodo { get; set; }       // Objeto Periodo (AUN NO CREADO)
    }
}
