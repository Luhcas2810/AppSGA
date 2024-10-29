using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Curso
    {
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdPlan { get; set; }
        public int Creditos { get; set; }
        public PlanEstudio _Plan { get; set; }
    }
}
