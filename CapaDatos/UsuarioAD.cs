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
    public class UsuarioAD
    {
        public static UsuarioAD _instancia = null;

        private UsuarioAD()
        {

        }

        public static UsuarioAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new UsuarioAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Usuario>> ObtenerListaUsuarioAsync()
        {
            List<Usuario> rptListaUsuario = new List<Usuario>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ObtenerListaUsuario", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaUsuario.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            _Usuario = dr["Usuario"].ToString(),
                            _Rol = (await RolAD.Instancia.ObtenerListaRolAsync()).FirstOrDefault(x => x.IdRol == Convert.ToInt32(dr["IdRol"])),
                            Contrasenia = dr["Contrasenia"].ToString()
                        });
                    }
                    oConexion.Close();
                    return rptListaUsuario;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearUsuarioAsync(Usuario usuario)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CrearUsuario", oConexion);
                cmd.Parameters.AddWithValue("@IdRol", usuario._Rol.IdRol);
                cmd.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                cmd.Parameters.AddWithValue("@Usuario", usuario._Usuario);
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

        public async Task<bool> ModificarUsuarioAsync(Usuario usuario)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ModificarUsuario", oConexion);
                cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@IdRol", usuario._Rol.IdRol);
                cmd.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                cmd.Parameters.AddWithValue("@Usuario", usuario._Usuario);
                cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
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
