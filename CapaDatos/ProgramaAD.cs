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
    public class ProgramaAD
    {
        public static ProgramaAD _instancia = null;

        private ProgramaAD()
        {

        }

        public static ProgramaAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new ProgramaAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Programa>> ObtenerListaProgramaAsync()
        {
            List<Programa> rptListaPrograma = new List<Programa>();
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
                        rptListaPrograma.Add(new Programa()
                        {
                            IdPrograma = Convert.ToInt32(dr["IdPrograma"]),
                            Duracion = Convert.ToInt32(dr["Duracion"]),
                            Carrera = dr["Carrera"].ToString()
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

        public async Task<bool> CrearProgramaAsync(Programa programa)
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
