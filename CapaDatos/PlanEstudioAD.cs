using CapaConexion;
using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class PlanEstudioAD
    {
        public static PlanEstudioAD _instancia = null;

        private PlanEstudioAD() { }

        public static PlanEstudioAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PlanEstudioAD();
                }
                return _instancia;
            }
        }

        // Método para obtener la lista de planes de estudio
        public async Task<List<PlanEstudio>> ObtenerListaPlanEstudioAsync()
        {
            List<PlanEstudio> rptListaPlanEstudio = new List<PlanEstudio>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaPlanEstudio", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaPlanEstudio.Add(new PlanEstudio()
                        {
                            Codigo = Convert.ToInt32(dr["pla_iCodigo"]),
                            //CodigoEscuela = Convert.ToInt32(dr["esc_iCodigo"]),
                            Descripcion = dr["pla_nvcDescripcion"].ToString(),
                            Estado = Convert.ToBoolean(dr["pla_bActivo"]),
                            _Escuela = (await EscuelaAD.Instancia.ObtenerListaEscuelaAsync()).FirstOrDefault(r => r.Codigo == Convert.ToInt32(dr["esc_iCodigo"]))
                        });
                    }
                    oConexion.Close();
                    return rptListaPlanEstudio;
                }
                catch
                {
                    return null;
                }
            }
        }

        // Método para crear un nuevo plan de estudio
        public async Task<Resultado> AgregarPlanEstudioAsync(PlanEstudio planEstudio)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_AgregarPlanEstudio", oConexion);
                cmd.Parameters.AddWithValue("@CodigoEscuela", planEstudio.CodigoEscuela);
                cmd.Parameters.AddWithValue("@Descripcion", planEstudio.Descripcion);
                cmd.Parameters.Add("Mensaje", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output; 
                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return new Resultado()
                    {
                        Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value),
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString()
                    };
                }
                catch (SqlException ex)
                {
                    return new Resultado()
                    {
                        Respuesta = false,
                        Mensaje = ex.Message
                    };
                }
            }
        }

        public async Task<Resultado> ModificarPlanAsync(PlanEstudio plan)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ModificarPlanEstudio", oConexion);
                cmd.Parameters.AddWithValue("@CodigoPlan", plan.Codigo);
                cmd.Parameters.AddWithValue("@Estado", plan.Estado ? 1 : 0);
                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return new Resultado()
                    {
                        Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value),
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString()
                    };
                }
                catch (SqlException ex)
                {
                    return new Resultado()
                    {
                        Respuesta = false,
                        Mensaje = ex.Message
                    };
                }
            }
        }
    }
}
