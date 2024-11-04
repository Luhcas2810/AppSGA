using CapaConexion;
using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class EstudianteAD
    {
        public static EstudianteAD _instancia = null;

        private EstudianteAD()
        {

        }

        public static EstudianteAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new EstudianteAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Estudiante>> ObtenerListaEstudianteAsync()
        {
            List<Estudiante> rptListaEstudiante = new List<Estudiante>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListarEstudiante", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaEstudiante.Add(new Estudiante()
                        {
                            Codigo = Convert.ToInt32(dr["est_iCodigo"]),
                            CodigoUsuario = Convert.ToInt32(dr["usu_iCodigo"]),
                            CodigoEscuela = Convert.ToInt32(dr["esc_iCodigo"])
                        });
                    }
                    oConexion.Close();
                    return rptListaEstudiante;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
