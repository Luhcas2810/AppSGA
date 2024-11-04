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
    public class AulaAD
    {
        public static AulaAD _instancia = null;

        private AulaAD()
        {

        }

        public static AulaAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new AulaAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Aula>> ObtenerListaAulaAsync()
        {
            List<Aula> rptListaAula = new List<Aula>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ObtenerListaAula", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaAula.Add(new Aula()
                        {
                            Codigo = Convert.ToInt32(dr["aul_iCodigo"]),
                            Nombre = dr["aul_nvcNombre"].ToString(),
                            Capacidad = Convert.ToInt32(dr["aul_iCapacidad"])
                        });
                    }
                    oConexion.Close();
                    return rptListaAula;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearAulaAsync(Aula aula)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CrearAula", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", aula.Nombre);
                cmd.Parameters.AddWithValue("@Capacidad", aula.Capacidad);
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

        public async Task<bool> ModificarAulaAsync(Aula aula)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ModificarAula", oConexion);
                cmd.Parameters.AddWithValue("@IdAula", aula.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", aula.Nombre);
                cmd.Parameters.AddWithValue("@Capacidad", aula.Capacidad);
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
