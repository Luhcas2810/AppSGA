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
                SqlCommand cmd = new SqlCommand("proc_ObtenerListaRol", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaRol.Add(new Rol()
                        {
                            IdRol = Convert.ToInt32(dr["IdRol"]),
                            _Rol = dr["Rol"].ToString()
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
                SqlCommand cmd = new SqlCommand("proc_CrearRol", oConexion);
                cmd.Parameters.AddWithValue("@Rol", rol._Rol);
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
