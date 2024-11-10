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
    public class EscuelaAD
    {
        public static EscuelaAD _instancia = null;

        private EscuelaAD()
        {

        }

        public static EscuelaAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new EscuelaAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Escuela>> ObtenerListaEscuelaAsync()
        {
            List<Escuela> rptListaEscuela = new List<Escuela>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaEscuela", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaEscuela.Add(new Escuela()
                        {
                            Codigo = Convert.ToInt32(dr["esc_iCodigo"]),
                            Carrera = dr["esc_nvcCarrera"].ToString(),
                            Duracion = Convert.ToInt32(dr["esc_iDuracion"]),
                            FechaRegistro = Convert.ToDateTime(dr["esc_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaEscuela;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearEscuelaAsync(Escuela escuela)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CrearPrograma", oConexion);
                cmd.Parameters.AddWithValue("@Carrera", escuela.Carrera);
                cmd.Parameters.AddWithValue("@Duracion", escuela.Duracion);
                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
