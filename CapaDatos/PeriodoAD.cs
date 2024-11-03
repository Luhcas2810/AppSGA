using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaConexion;
using CapaModelos;

using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class PeriodoAD
    {
        public static PeriodoAD _instancia = null;

        private PeriodoAD() { }

        public static PeriodoAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PeriodoAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Periodo>> ObtenerListaPeriodoAsync()
        {
            List<Periodo> listaPeriodos = new List<Periodo>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaPeriodo", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        listaPeriodos.Add(new Periodo()
                        {
                            IdPeriodo = Convert.ToInt32(dr["per_iCodigo"]),
                            Nombre = dr["per_nvcPeriodo"].ToString(),
                            FechaInicio = Convert.ToDateTime(dr["per_dtFechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["per_dtFechaFin"])
                        });
                    }
                    oConexion.Close();
                    return listaPeriodos;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearPeriodoAsync(Periodo periodo)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_AgregarPeriodo", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", periodo.Nombre);
                cmd.Parameters.AddWithValue("@FechaInicio", periodo.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", periodo.FechaFin);
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

        public async Task<bool> ModificarPeriodoAsync(Periodo periodo)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ModificarPeriodo", oConexion);
                cmd.Parameters.AddWithValue("@Codigo", periodo.IdPeriodo);
                cmd.Parameters.AddWithValue("@Nombre", periodo.Nombre);
                cmd.Parameters.AddWithValue("@FechaInicio", periodo.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", periodo.FechaFin);
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

