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
                SqlCommand cmd = new SqlCommand("proc_ObtenerListaPrograma", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaEstudiante.Add(new Estudiante()
                        {
                            IdEstudiante = Convert.ToInt32(dr["IdEstudiante"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Direccion = dr["Direccion"].ToString(),
                            DNI = dr["DNI"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                            _Programa = (await ProgramaAD.Instancia.ObtenerListaProgramaAsync()).FirstOrDefault(x => x.IdPrograma == Convert.ToInt32(dr["IdPrograma"])),
                            Telefono = dr["Telefono"].ToString()
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
