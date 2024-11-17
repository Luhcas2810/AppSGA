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
        public bool Estado { get; set; }
        public string Color
        {
            get
            {
                if (Estado)
                {
                    return "danger";
                }
                else
                {
                    return "success";
                }
            }
        }
        public string Icono
        {
            get
            {
                if (Estado)
                {
                    return "lock";
                }
                else
                {
                    return "unlock";
                }
            }
        }
        public Escuela _Escuela { get; set; }
    }
}
