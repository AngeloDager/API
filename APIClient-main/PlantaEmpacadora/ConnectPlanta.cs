using PlantaEmpacadora.Model;
using PlantaEmpacadora.Services;
using System;
using System.Data.SqlClient;

namespace PlantaEmpacadora
{
    public class ConnectPlanta
    {
        //*conexion administracion
        string Host = "192.168.8.51";
        string Database = "IPSPAdministracion";
        string Username = "AdministracionIpsp";
        string Password= "AdministracionIpsp";

        /*string Host = "192.168.1.160,1433";
        string Database = "ProduccionBI";
        string Username = "Planta";
        string Password = "Planta123";*/


        public void Connect(string HostP, string DatabaseP, string UsernameP, string PasswordP)
        {
            Console.WriteLine("Connecting host Planta...");

            //Obtengo Id de la tabla de IpspAdministracion
            SqlConnection oConexionID = ConnectDB.Conectar(Host, Database, Username, Password);
            var (IdVenta, IdInventario, Idliq) = new Planta().ObtenerId(oConexionID);
            oConexionID.Close();
            DateTime fechaConsultaV = IdVenta == DateTime.MinValue.ToString() ? DateTime.Parse("01/01/2022") : DateTime.Parse(IdVenta).AddDays(-15);
            //DateTime fechaConsultaI = IdInventario == DateTime.MinValue.ToString()? DateTime.Parse("01/01/2022"): DateTime.Parse(IdInventario).AddDays(-15);

            //string fechaInventario = $"{fechaConsultaI.Month:D2}-{fechaConsultaI.Day:D2}-{fechaConsultaI.Year}";
            string fechaVenta = $"{fechaConsultaV.Month:D2}-{fechaConsultaV.Day:D2}-{fechaConsultaV.Year}";

            //Consulto  los datos de la tabla de Planta
            // ConnectDB.Connect(@"10.51.17.135\SQLDeveloper", "Basestil", "lectura", "L3ctura99");
            SqlConnection oConexion = ConnectDB.Conectar(HostP, DatabaseP, UsernameP, PasswordP);
            //List<Rendimiento> lsRendimmiento = new Planta().ConsultarRendimiento(oConexion);
            var (Rendimiento, Catalogo, Ventas, Inventario,liq) = new Planta().Consultar(oConexion, fechaVenta, IdInventario, Idliq);
            oConexion.Close();

            //Elimino los datos de la tabla de IpspAdministracion
            SqlConnection oConexionAdmin = ConnectDB.Conectar(Host, Database, Username, Password);
            new Planta().Eliminar(oConexionAdmin, fechaVenta, IdInventario, Idliq);
            oConexionAdmin.Close();

            //Inserto los datos de la tabla de IpspAdministracion
            ConnectDB.Connect(Host, Database, Username, Password);
            ConnectDB.BulkCopy<Rendimiento>("rendimiento_t", Rendimiento);
            ConnectDB.BulkCopy<Catalogo>("catalogo_t", Catalogo);
            ConnectDB.BulkCopy<Ventas>("ventas_t", Ventas);
            ConnectDB.BulkCopy<Inventario>("inventario_t", Inventario);
            ConnectDB.BulkCopy<Liq>("liq_t", liq);
            ConnectDB.Close();
            Console.WriteLine("Ejecutado...");
        }
    }
}
