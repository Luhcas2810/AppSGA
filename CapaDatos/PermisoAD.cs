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
    public class PermisoAD
    {
        public static PermisoAD _instancia = null;

        private PermisoAD() { }

        public static PermisoAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PermisoAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Permiso>> ObtenerListaPermisoAsync()
        {
            List<Permiso> rptListaPermiso = new List<Permiso>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaSubmenu", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaPermiso.Add(new Permiso()
                        {
                            Codigo = Convert.ToInt32(dr["per_iCodigo"]),
                            _Rol = (await RolAD.Instancia.ObtenerListaRolAsync()).FirstOrDefault(x => x.Codigo == Convert.ToInt32(dr["rol_iCodigo"])),
                            _Submenu = (await SubmenuAD.Instancia.ObtenerListaSubmenuAsync()).FirstOrDefault(x => x.Codigo == Convert.ToInt32(dr["sub_iCodigo"])),
                            Activo = Convert.ToBoolean(dr["per_bActivo"]),
                            FechaRegistro = Convert.ToDateTime(dr["per_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaPermiso;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<Permiso> ObtenerListaPermiso()
        {
            List<Permiso> rptListaPermiso = new List<Permiso>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaSubmenu", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptListaPermiso.Add(new Permiso()
                        {
                            Codigo = Convert.ToInt32(dr["per_iCodigo"]),
                            _Rol = RolAD.Instancia.ObtenerListaRol().FirstOrDefault(x => x.Codigo == Convert.ToInt32(dr["rol_iCodigo"])),
                            _Submenu = SubmenuAD.Instancia.ObtenerListaSubmenu().FirstOrDefault(x => x.Codigo == Convert.ToInt32(dr["sub_iCodigo"])),
                            Activo = Convert.ToBoolean(dr["per_bActivo"]),
                            FechaRegistro = Convert.ToDateTime(dr["per_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaPermiso;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
