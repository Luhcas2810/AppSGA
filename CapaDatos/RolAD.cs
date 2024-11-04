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
    public class RolAD
    {
        public static RolAD _instancia = null;

        private RolAD()
        {

        }

        public static RolAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new RolAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Rol>> ObtenerListaRolAsync()
        {
            List<Rol> rptListaRol = new List<Rol>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaRol", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaRol.Add(new Rol()
                        {
                            Codigo = Convert.ToInt32(dr["rol_iCodigo"]),
                            Descripcion = dr["rol_nvcDescripcion"].ToString()
                        });
                    }
                    oConexion.Close();
                    return rptListaRol;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearRolAsync(Rol rol)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_AgregarRol", oConexion);
                cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
                cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
                catch(SqlException ex)
                {
                    string mensaje = ex.Message;
                    return false;
                }
            }
        }
    }
}
