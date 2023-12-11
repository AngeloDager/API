using PlantaEmpacadora.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PlantaEmpacadora.Services
{
    public class ConnectTropack
    {
        //*conexion administracion
        string Host = "192.168.8.51";
        string Database = "IPSPAdministracion";
        string Username = "AdministracionIpsp";
        string Password = "AdministracionIpsp";

          /*string Host = "192.168.1.160,1433";
          string Database = "ProduccionBI";
          string Username = "Planta";
          string Password = "Planta123";*/


        public void Connect(string HostP, string DatabaseP, string UsernameP, string PasswordP)
        {
            Console.WriteLine("Connecting host Tropack...");

            //Obtengo Id de la tabla de IpspAdministracion
            SqlConnection oConexionID = ConnectDB.Conectar(Host, Database, Username, Password);
            var IdDist= new Tropack().ObtenerId(oConexionID);
            oConexionID.Close();
            DateTime fechaConsulta = IdDist == DateTime.MinValue.ToString()? DateTime.Parse("01/01/2022"): DateTime.Parse(IdDist).AddDays(-15);
            string fecha = $"{fechaConsulta.Month:D2}-{fechaConsulta.Day:D2}-{fechaConsulta.Year}";

            //Consulto  los datos de la tabla de Planta
            SqlConnection oConexion = ConnectDB.Conectar(HostP, DatabaseP, UsernameP, PasswordP);
            List <ViewDefectosAnalisis> lsViewDefectosAnalisis = new Tropack().Consultar(oConexion, fecha);
            oConexion.Close();

            //Elimino los datos de la tabla de IpspAdministracion
            SqlConnection oConexionAdmin = ConnectDB.Conectar(Host, Database, Username, Password);
            new Tropack().Eliminar(oConexionAdmin, fecha);
            oConexionAdmin.Close();

            //Inserto los datos de la tabla de IpspAdministracion
            ConnectDB.Connect(Host, Database, Username, Password);
            ConnectDB.BulkCopy<ViewDefectosAnalisis>("view_defectos_analisis_t", lsViewDefectosAnalisis);
            ConnectDB.Close();
            Console.WriteLine("Ejecutado...");
        }
    }
}
