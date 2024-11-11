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
    public class SubmenuAD
    {
        public static SubmenuAD _instancia = null;

        private SubmenuAD() { }

        public static SubmenuAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new SubmenuAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Submenu>> ObtenerListaSubmenuAsync()
        {
            List<Submenu> rptListaSubmenu = new List<Submenu>();
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
                        rptListaSubmenu.Add(new Submenu()
                        {
                            Codigo = Convert.ToInt32(dr["sub_iCodigo"]),
                            _Menu = (await MenuAD.Instancia.ObtenerListaMenuAsync()).FirstOrDefault(x => x.Codigo == Convert.ToInt32(dr["men_iCodigo"])),
                            Nombre = dr["sub_nvcNombre"].ToString(),
                            Controlador = dr["sub_nvcControlador"].ToString(),
                            Vista = dr["sub_nvcVista"].ToString(),
                            Icono = dr["sub_nvcIcono"].ToString(),
                            Activo = Convert.ToBoolean(dr["sub_bActivo"]),
                            FechaRegistro = Convert.ToDateTime(dr["sub_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaSubmenu;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<Submenu> ObtenerListaSubmenu()
        {
            List<Submenu> rptListaSubmenu = new List<Submenu>();
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
                        rptListaSubmenu.Add(new Submenu()
                        {
                            Codigo = Convert.ToInt32(dr["sub_iCodigo"]),
                            _Menu = MenuAD.Instancia.ObtenerListaMenu().FirstOrDefault(x => x.Codigo == Convert.ToInt32(dr["men_iCodigo"])),
                            Nombre = dr["sub_nvcNombre"].ToString(),
                            Controlador = dr["sub_nvcControlador"].ToString(),
                            Vista = dr["sub_nvcVista"].ToString(),
                            Icono = dr["sub_nvcIcono"].ToString(),
                            Activo = Convert.ToBoolean(dr["sub_bActivo"]),
                            FechaRegistro = Convert.ToDateTime(dr["sub_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaSubmenu;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
