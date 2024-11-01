﻿using CapaConexion;
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
                SqlCommand cmd = new SqlCommand("proc_ObtenerListaDepartamento", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaDepartamento.Add(new Departamento()
                        {
                            IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                            Nombre = dr["Nombre"].ToString(),
                            Jefe = dr["Jefe"].ToString()
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
                cmd.Parameters.AddWithValue("@Nombre", departamento.Nombre);
                cmd.Parameters.AddWithValue("@Jefe", departamento.Jefe);
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
