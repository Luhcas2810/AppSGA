using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelos
{
    public class Docente
    {
        public int IdDocente { get; set; }       // Código del docente
        public int IdUsuario { get; set; }       // Código del usuario asociado
        public int IdDepartamento { get; set; }  // Código del departamento asociado

        public Usuario _Usuario { get; set; }         // Objeto Usuario
        public Departamento _Departamento { get; set; } // Objeto Departamento
    }
}
