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
    public class DepartamentoAD
    {
        public static DepartamentoAD _instancia = null;

        private DepartamentoAD()
        {

        }

        public static DepartamentoAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new DepartamentoAD();
                }
                return _instancia;
            }
        }

        public async Task<List<Departamento>> ObtenerListaDepartamentoAsync()
        {
            List<Departamento> rptListaDepartamento = new List<Departamento>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ListaDepartamento", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaDepartamento.Add(new Departamento()
                        {
                            Codigo = Convert.ToInt32(dr["dep_iCodigo"]),
                            Descripcion = dr["dep_nvcDescripcion"].ToString()
                        });
                    }
                    oConexion.Close();
                    return rptListaDepartamento;
                }
                catch
                {
                    return null;
                }
            }
        }

        public async Task<bool> CrearDepartamentoAsync(Departamento departamento)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CrearDepartamento", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", departamento.Descripcion);
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
