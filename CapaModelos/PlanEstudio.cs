using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class PlanEstudio
    {
        public int Codigo { get; set; }
        public int CodigoEscuela { get; set; }
        public string Descripcion { get; set; }
        public Escuela _Escuela { get; set; }
    }
}
