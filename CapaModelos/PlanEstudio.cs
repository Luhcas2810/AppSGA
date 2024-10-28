using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class PlanEstudio
    {
        public int IdPlan { get; set; }
        public int IdPrograma { get; set; }
        public int Semestre { get; set; }
        public int Estado { get; set; } // 1 para habilitado, 0 para deshabilitado
    }
}

