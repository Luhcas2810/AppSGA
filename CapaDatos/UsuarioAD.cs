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

        private UsuarioAD() { }

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

                    // Obtenemos todos los roles para relacionarlos luego con los usuarios
                    List<Rol> listaRoles = await RolAD.Instancia.ObtenerListaRolAsync();

                    while (dr.Read())
                    {
                        // Obtener el rol asociado al usuario
                        int idRol = Convert.ToInt32(dr["IdRol"]);
                        Rol rolAsociado = listaRoles.FirstOrDefault(r => r.IdRol == idRol);
                        
                        rptListaUsuario.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            _Usuario = dr["Usuario"].ToString(),
                            Contrasenia = dr["Contrasenia"].ToString(),
                            Estado = Convert.ToInt32(dr["Estado"]),
                            IdRol = idRol,
                            _Rol = rolAsociado // Asignamos el rol completo
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
                cmd.Parameters.AddWithValue("@Usuario", usuario._Usuario);
                cmd.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                cmd.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
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
