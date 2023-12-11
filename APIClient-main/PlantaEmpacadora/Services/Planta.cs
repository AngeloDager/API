using PlantaEmpacadora.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PlantaEmpacadora.Services
{
    public class Planta
    {
     

        public void Eliminar(SqlConnection oConexion, string IdVenta, int IdInventario, int idLiq)
        {
        
            using (oConexion)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from rendimiento_t;" +
                                                     "delete from catalogo_t;"+
                                                     //"delete from ventas_t  where id >" + IdVenta + ";" +
                                                     "delete from ventas_t where CONVERT(DATE, fecha) >= '" + IdVenta + "'; "+
                                                     "delete from inventario_t where id >" + IdInventario + ";"+
                                                     //"delete from inventario_t where CONVERT(DATE,fcorte) >='" + IdInventario + "';" +
                                                     "delete from liq_t where lote >" + idLiq + ";", oConexion);
              

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public (List<Rendimiento>, List<Catalogo>, List<Ventas>, List<Inventario>, List<Liq>) Consultar(SqlConnection oConexion, string IdVenta, int IdInventario, int idLiq)
        {
            List<Rendimiento> rptListaRendimiento = new List<Rendimiento>();
            List<Catalogo> rptListaCatalogo = new List<Catalogo>();
            List<Ventas> rptListaVentas = new List<Ventas>();
            List<Inventario> rptListaInventario = new List<Inventario>();
            List<Liq> rptListaLiq= new List<Liq>();

            using (oConexion)
            {
                SqlCommand cmd = new SqlCommand("select * from rendimiento;" +
                                                "select * from catalogo;"+
                                                "select * from ventas where CONVERT(DATE,fecha) >= '" + IdVenta + "';"+
                                                "select * from inventario where id >" + IdInventario +";"+
                                                //"select * from inventario where CONVERT(DATE,fcorte) >='" + IdInventario + "';" +
                                                "select * from liq where LOTE >" + idLiq + ";",  oConexion);

                // cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    int contador = 1;
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    bool resultado = true;
                    while (resultado)
                    {
                      
                        if (contador == 1)
                        {
                            while (dr.Read())
                            {
                               
                                rptListaRendimiento.Add(new Rendimiento()
                                {

                                    codigo = dr["codigo"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                    rendimiento = float.Parse(dr["rendimiento"].ToString().Replace('.', ','))
                                });
                            }
                        }
                        else if(contador == 2)
                        {
                            while (dr.Read())
                            {
                                rptListaCatalogo.Add(new Catalogo()
                                {

                                    codigo = dr["codigo"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                    talla = dr["talla"].ToString()
                                });
                            }

                        }
                        else if (contador == 3)
                        {
                            while (dr.Read())
                            {
                                rptListaVentas.Add(new Ventas()
                                {

                                    id = Convert.ToInt32(dr["id"].ToString()),
                                    fecha = dr["fecha"].ToString(),
                                    cantidad = float.Parse(dr["cantidad"].ToString().Replace('.', ',')),
                                    codigo = dr["codigo"].ToString(),
                                    producto = dr["producto"].ToString(),
                                    mercado = dr["mercado"].ToString(),
                                    tipo = dr["tipo"].ToString()
                                });
                            }

                        }
                        else if (contador == 4)
                        {
                            while (dr.Read())
                            {
                                rptListaInventario.Add(new Inventario()
                                {

                                    id = Convert.ToInt32(dr["id"].ToString()),
                                    articulo = dr["articulo"].ToString(),
                                    categoria = dr["categoria"].ToString(),
                                    descripcion = dr["descripcion"].ToString(),
                                    unidad = dr["unidad"].ToString(),
                                    cantidad = float.Parse(dr["cantidad"].ToString().Replace('.', ',')),
                                    fcorte = dr["fcorte"].ToString()
                                });
                            }

                        }
                        else if (contador == 5)
                        {
                            while (dr.Read())
                            {
                                rptListaLiq.Add(new Liq()
                                {
                                    Fecha =  DateTime.Parse(dr["FECHA"].ToString()),
                                    Lote = int.Parse(dr["LOTE"].ToString()),
                                    Tanqueros = int.Parse(dr["TANQUEROS"].ToString()),
                                    Rangofiletes = int.Parse(dr["RANGO FILETES"].ToString()),
                                    PecesTotalesRecibidos = float.Parse(dr["PECES TOTALES RECIBIDOS"].ToString()),
                                    PecesFiletear = int.Parse(dr["PECES A FILETEAR"].ToString()),
                                    Finca = float.Parse(dr["FINCA"].ToString()),
                                    Marel = float.Parse(dr["MAREL"].ToString()),
                                    Filetable = dr["FILETEABLE"] ==DBNull.Value?0:  decimal.Parse( dr["FILETEABLE"].ToString()),
                                    DeclaradasA = float.Parse(dr["DECLARADAS_A"].ToString()),
                                    RecibidasB = float.Parse(dr["RECIBIDAS_B"].ToString()),
                                    DiferenciaAB = dr["DIFRENCIA_A_B"] == DBNull.Value ? 0 : decimal.Parse(dr["DIFRENCIA_A_B"].ToString()),                                   
                                    DescarteMuertos = float.Parse(dr["DESCARTE_muertos"].ToString()),
                                    Recepcion = dr["RECEPCION"] == DBNull.Value ? 0 : decimal.Parse(dr["RECEPCION"].ToString()),
                                    Total = dr["TOTAL"] == DBNull.Value ? 0 : decimal.Parse(dr["TOTAL"].ToString()),
                                });
                            }

                        }
                        contador++;
                        if (!dr.NextResult())
                            resultado = false;
                    }
                  
                    dr.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    rptListaRendimiento = null;
                    rptListaCatalogo = null;
                    rptListaVentas = null;
                    rptListaInventario = null;
                    rptListaLiq = null;
                }
            }
            return (rptListaRendimiento, rptListaCatalogo, rptListaVentas, rptListaInventario, rptListaLiq);
        }

        public (string IdVenta, int IdInventario, int IdLiq) ObtenerId(SqlConnection oConexion)
        {
            string PIdVenta = null;
            int PIdInventario = 0;
            int PIdLiq = 0;

            using (oConexion)
            {
                SqlCommand cmd = new SqlCommand(//"select isnull((select max(id) from ventas_t ),0) IdVenta," +
                                                "select isnull((select max(CONVERT(DATE, fecha)) from ventas_t), cast('0001-01-01' as DATE)) IdVenta," +
                                                "isnull((select max(id) from inventario_t ),0) IdInventario," +
                                                //"isnull((select max(CONVERT(DATE, fcorte)) from inventario_t), cast('0001-01-01' as DATE)) IdInventario," +
                                                "isnull((select max(lote) from liq_t ),0) Idliq;"
                                                , oConexion);
                cmd.CommandTimeout = 3600;
                // cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                 

                            while (dr.Read())
                            {
                               /* PIdVenta = dr["IdVenta"].ToString();
                                PIdInventario = dr["IdInventario"].ToString();*/
                              PIdVenta =dr["IdVenta"].ToString();
                              PIdInventario = int.Parse(dr["IdInventario"].ToString());
                              PIdLiq = int.Parse(dr["Idliq"].ToString());
                            }
                    

                    dr.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    PIdVenta = null;
                    PIdInventario = 0;
                    PIdLiq=0;
                }
            }
            return (PIdVenta, PIdInventario, PIdLiq);
        }
    }
}
