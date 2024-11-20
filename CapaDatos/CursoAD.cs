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
    public class CursoAD
    {
        public static CursoAD _instancia = null;

        private CursoAD() { }

        public static CursoAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CursoAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Curso>> ObtenerListaCursoAsync()
        {
            List<Curso> listaCursos = new List<Curso>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaCurso", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        listaCursos.Add(new Curso()
                        {
                            Codigo = Convert.ToInt32(dr["cur_iCodigo"]),
                            Nombre = dr["cur_nvcNombre"].ToString(),
                            CodigoPlan = Convert.ToInt32(dr["pla_iCodigo"]),
                            Creditos = Convert.ToInt32(dr["cur_iCreditos"]),
                            
                        });
                    }
                    oConexion.Close();
                    return listaCursos;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<Resultado> CrearCursoAsync(Curso curso)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CrearCurso", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
                cmd.Parameters.AddWithValue("@IdCurso", curso.Codigo);
                cmd.Parameters.AddWithValue("@IdPlan", curso.CodigoPlan);
                cmd.Parameters.AddWithValue("@Creditos", curso.Creditos);
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
                        Mensaje = cmd.Parameters["Mensaje"].Value.ToString(),
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

        public async Task<Resultado> ModificarCursoAsync(Curso curso)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ModificarCurso", oConexion);
                cmd.Parameters.AddWithValue("@IdCurso", curso.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", curso.Nombre);
                //cmd.Parameters.AddWithValue("@Codigo", curso.Codigo);
                cmd.Parameters.AddWithValue("@IdPlan", curso.CodigoPlan);
                cmd.Parameters.AddWithValue("@Creditos", curso.Creditos);
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
    }
}
