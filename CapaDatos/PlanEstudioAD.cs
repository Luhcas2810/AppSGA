﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaConexion;
using CapaModelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class PlanEstudioAD
    {
        public static PlanEstudioAD _instancia = null;

        private PlanEstudioAD() { }

        public static PlanEstudioAD Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new PlanEstudioAD();
                }
                return _instancia;
            }
        }

        //public async Task<bool> CrearPlanEstudioAsync(PlanEstudio planEstudio)
        //{
        //    using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
        //    {
        //        SqlCommand cmd = new SqlCommand("proc_CrearPlanEstudio", oConexion);
        //        cmd.Parameters.AddWithValue("@IdPrograma", planEstudio.IdPrograma);
        //        cmd.Parameters.AddWithValue("@Semestre", planEstudio.Semestre);
        //        cmd.Parameters.AddWithValue("@Estado", planEstudio.Estado);
        //        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        try
        //        {
        //            await oConexion.OpenAsync();
        //            await cmd.ExecuteNonQueryAsync();
        //            return Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}

        public async Task<bool> CambiarEstadoPlanEstudioAsync(int idPlan, bool habilitar)
        {
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_CambiarEstadoPlanEstudio", oConexion);
                cmd.Parameters.AddWithValue("@IdPlan", idPlan);
                cmd.Parameters.AddWithValue("@Estado", habilitar ? 1 : 0);
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

        public async Task<List<PlanEstudio>> ObtenerListaPlanEstudioAsync()
        {
            List<PlanEstudio> rptListaPlanEstudio = new List<PlanEstudio>();
            using (SqlConnection oConexion = new SqlConnection(ConexionSQL.conexionSQL))
            {
                SqlCommand cmd = new SqlCommand("proc_ObtenerListaPlanEstudio", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await oConexion.OpenAsync();
                    SqlDataReader dr = await cmd.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        rptListaPlanEstudio.Add(new PlanEstudio()
                        {
                            IdPlan = Convert.ToInt32(dr["IdPlan"]),
                            Semestre = Convert.ToInt32(dr["Semestre"]),
                            Estado = Convert.ToInt32(dr["Estado"]),
                            _Programa = (await ProgramaAD.Instancia.ObtenerListaProgramaAsync())
                                .FirstOrDefault(x => x.IdPrograma == Convert.ToInt32(dr["IdPrograma"]))
                        });
                    }
                    oConexion.Close();
                    return rptListaPlanEstudio;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}

