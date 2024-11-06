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
                SqlCommand cmd = new SqlCommand("proc_ListaUsuario", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaUsuario.Add(new Usuario()
                        {
                            Codigo = Convert.ToInt32(dr["usu_iCodigo"]),
                            _Usuario = dr["usu_nvcUsuario"].ToString(),
                            Contrasenia = dr["usu_nvcContrasenia"].ToString(),
                            Nombre = dr["usu_nvcNombre"].ToString(),
                            Apellido = dr["usu_nvcApellido"].ToString(),
                            Identificacion = dr["usu_cIdentificacion"].ToString(),
                            Correo = dr["usu_nvcCorreo"].ToString(),
                            Telefono = dr["usu_nvcTelefono"].ToString(),
                            Direccion = dr["usu_nvcDireccion"].ToString(),
                            FechaNacimiento = Convert.ToDateTime(dr["usu_dtFechaNacimiento"]),
                            _Rol = (await RolAD.Instancia.ObtenerListaRolAsync()).FirstOrDefault(r => r.Codigo == Convert.ToInt32(dr["rol_iCodigo"]))
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

        public async Task<bool> AgregarUsuarioAsync(Usuario usuario)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_AgregarUsuario", oConexion);
                cmd.Parameters.AddWithValue("@Rol", usuario.CodigoRol);
                cmd.Parameters.AddWithValue("@Contrasenia", usuario.Contrasenia);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Identificacion", usuario.Identificacion);
                cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("@Nacimiento", usuario.FechaNacimiento);
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
