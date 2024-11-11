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
    public class MenuAD
    {
        public static MenuAD _instancia = null;

        private MenuAD() { }

        public static MenuAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new MenuAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Menu>> ObtenerListaMenuAsync()
        {
            List<Menu> rptListaMenu = new List<Menu>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaMenu", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaMenu.Add(new Menu()
                        {
                            Codigo = Convert.ToInt32(dr["men_iCodigo"]),
                            Nombre = dr["men_nvcNombre"].ToString(),
                            Icono = dr["men_nvcIcono"].ToString(),
                            Activo = Convert.ToBoolean(dr["men_bActivo"]),
                            FechaRegistro = Convert.ToDateTime(dr["men_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaMenu;
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<Menu> ObtenerListaMenu()
        {
            List<Menu> rptListaMenu = new List<Menu>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaMenu", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptListaMenu.Add(new Menu()
                        {
                            Codigo = Convert.ToInt32(dr["men_iCodigo"]),
                            Nombre = dr["men_nvcNombre"].ToString(),
                            Icono = dr["men_nvcIcono"].ToString(),
                            Activo = Convert.ToBoolean(dr["men_bActivo"]),
                            FechaRegistro = Convert.ToDateTime(dr["men_dtFechaRegistro"])
                        });
                    }
                    oConexion.Close();
                    return rptListaMenu;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
