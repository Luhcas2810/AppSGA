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
            List<Escuela> rptListaPrograma = new List<Escuela>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListarEscuela", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaPrograma.Add(new Escuela()
                        {
                            Codigo = Convert.ToInt32(dr["esc_iCodigo"]),
                            Carrera = dr["esc_nvcCarrera"].ToString(),
                            Duracion = Convert.ToInt32(dr["esc_iDuracion"])
                        });
                    }
                    oConexion.Close();
                    return rptListaPrograma;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearEscuelaAsync(Escuela programa)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CrearPrograma", oConexion);
                cmd.Parameters.AddWithValue("@Carrera", programa.Carrera);
                cmd.Parameters.AddWithValue("@Duracion", programa.Duracion);
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
