using PlantaEmpacadora.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PlantaEmpacadora.Services
{
    public class Tropack
    {
     

        public void Eliminar(SqlConnection oConexion, string IdDist)
        {
        
            using (oConexion)
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("delete from view_defectos_analisis_t where format(hora_llegada,'yyyy-MM')  >= '" + IdDist + "';", oConexion);
                    SqlCommand cmd = new SqlCommand("delete from view_defectos_analisis_t where CONVERT(DATE, hora_llegada)  >= '" + IdDist + "';", oConexion);

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<ViewDefectosAnalisis> Consultar(SqlConnection oConexion, string IdDist)
        {
            List<ViewDefectosAnalisis> rptListaDefecto = new List<ViewDefectosAnalisis>();

            using (oConexion)
            {
                //SqlCommand cmd = new SqlCommand("select  * from myad001 where format(hora_llegada,'yyyy-MM')  >= '" + IdDist + "';", oConexion);
                SqlCommand cmd = new SqlCommand("select * from myad001 where CONVERT(DATE,hora_llegada) >= '" + IdDist + "' order  by  hora_llegada;", oConexion);
                cmd.CommandTimeout = 3600;
                // cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                   
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                            while (dr.Read())
                            {

                                rptListaDefecto.Add(new ViewDefectosAnalisis()
                                {

                                    id_dist = Convert.ToInt32(dr["id_dist"].ToString()),
                                    lote = Convert.ToInt32(dr["lote"].ToString()),
                                    guia = Convert.ToInt32(dr["guia"].ToString()),
                                    camaronera_logistica = dr["camaronera_logistica"].ToString(),
                                    piscina_logistica = dr["piscina_logistica"].ToString(),
                                    cam_logistica_propia = dr["cam_logistica_propia"].ToString(),
                                    camaronera_recepcion = dr["camaronera_recepcion"].ToString(),
                                    piscina_recepcion = dr["piscina_recepcion"].ToString(),
                                    cam_recepcion_propia = dr["cam_recepcion_propia"].ToString(),
                                    hora_llegada = dr["hora_llegada"].ToString(),
                                    lugar_llegada = dr["lugar_llegada"].ToString(),
                                    hora_analisis = dr["hora_analisis"].ToString(),
                                    gramaje = dr["gramaje"] != DBNull.Value ? Convert.ToDecimal(dr["gramaje"].ToString()) : 0,
                                    porc_bueno_entero = dr["porc_bueno_entero"] != DBNull.Value ? Convert.ToDecimal(dr["porc_bueno_entero"].ToString()) : 0,
                                    so2_ppm = dr["so2_ppm"] != DBNull.Value ? Convert.ToDecimal(dr["so2_ppm"].ToString()) : 0,
                                    cabeza_floja = dr["cabeza_floja"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_floja"].ToString()) : 0,
                                    hepatopancreas_reventado = dr["hepatopancreas_reventado"] != DBNull.Value ? Convert.ToDecimal(dr["hepatopancreas_reventado"].ToString()) : 0,
                                    branquias_sucias = dr["branquias_sucias"] != DBNull.Value ? Convert.ToDecimal(dr["branquias_sucias"].ToString()) : 0,
                                    flacidez = dr["flacidez"] != DBNull.Value ? Convert.ToDecimal(dr["flacidez"].ToString()) : 0,
                                    mudado = dr["mudado"] != DBNull.Value ? Convert.ToDecimal(dr["mudado"].ToString()) : 0,
                                    cabeza_descolgada = dr["cabeza_descolgada"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_descolgada"].ToString()) : 0,
                                    cabeza_roja = dr["cabeza_roja"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_roja"].ToString()) : 0,
                                    cabeza_anaranjada = dr["cabeza_anaranjada"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_anaranjada"].ToString()) : 0,
                                    quebrado = dr["quebrado"] != DBNull.Value ? Convert.ToDecimal(dr["quebrado"].ToString()) : 0,
                                    picado_fuerte = dr["picado_fuerte"] != DBNull.Value ? Convert.ToDecimal(dr["picado_fuerte"].ToString()) : 0,
                                    picado_leve = dr["picado_leve"] != DBNull.Value ? Convert.ToDecimal(dr["picado_leve"].ToString()) : 0,
                                    deforme = dr["deforme"] != DBNull.Value ? Convert.ToDecimal(dr["deforme"].ToString()) : 0,
                                    deshidratado = dr["deshidratado"] != DBNull.Value ? Convert.ToDecimal(dr["deshidratado"].ToString()) : 0,
                                    melanosis = dr["melanosis"] != DBNull.Value ? Convert.ToDecimal(dr["melanosis"].ToString()) : 0,
                                    otra_especie = dr["otra_especie"] != DBNull.Value ? Convert.ToDecimal(dr["otra_especie"].ToString()) : 0,
                                    juveniles = dr["juveniles"] != DBNull.Value ? Convert.ToDecimal(dr["juveniles"].ToString()) : 0,
                                    cocido_europa = dr["cocido_europa"].ToString(),
                                    cabeza_amargo_f = dr["cabeza_amargo_f"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_amargo_f"].ToString()) : 0,
                                    cabeza_amargo_m = dr["cabeza_amargo_m"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_amargo_m"].ToString()) : 0,
                                    cabeza_amargo_l = dr["cabeza_amargo_l"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_amargo_l"].ToString()) : 0,
                                    cabeza_aceptable = dr["cabeza_aceptable"] != DBNull.Value ? Convert.ToDecimal(dr["cabeza_aceptable"].ToString()) : 0,
                                    cola_amargo_f = dr["cola_amargo_f"] != DBNull.Value ? Convert.ToDecimal(dr["cola_amargo_f"].ToString()) : 0,
                                    cola_amargo_m = dr["cola_amargo_m"] != DBNull.Value ? Convert.ToDecimal(dr["cola_amargo_m"].ToString()) : 0,
                                    cola_amargo_l = dr["cola_amargo_l"] != DBNull.Value ? Convert.ToDecimal(dr["cola_amargo_l"].ToString()) : 0,
                                    cola_aceptable = dr["cola_aceptable"] != DBNull.Value ? Convert.ToDecimal(dr["cola_aceptable"].ToString()) : 0,
                                    otros_sabores = dr["otros_sabores"].ToString(),
                                    establecimiento_analisis = dr["establecimiento_analisis"].ToString(),
                                    observaciones = dr["observaciones"].ToString(),
                                    camaronera = dr["camaronera"].ToString(),
                                    piscina = dr["piscina"].ToString(),
                                    cam_propia = dr["cam_propia"].ToString()

                                });
                            }
               
                  
                    dr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    rptListaDefecto = null;
                }
            }
            return rptListaDefecto.OrderBy(x=> x.id_dist).ToList();
        }

        public string  ObtenerId(SqlConnection oConexion)
        {
            string PIdDist="" ;
            using (oConexion)
            {
                //SqlCommand cmd = new SqlCommand("select isnull((select max(format(hora_llegada,'yyyy-MM')) from view_defectos_analisis_t ),'') Id_dist;", oConexion);
                SqlCommand cmd = new SqlCommand("select isnull((select max(CONVERT(DATE, hora_llegada)) from view_defectos_analisis_t),cast('0001-01-01' as date)) Id_dist ;", oConexion);
                
                // cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PIdDist = dr["Id_dist"].ToString();
                            // PIdDist = DateTime.Parse(dr["Id_dist"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PIdDist = "";
                }
            }
            return PIdDist;
        }
    }
}
