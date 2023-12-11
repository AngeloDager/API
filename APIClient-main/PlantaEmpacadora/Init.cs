using PlantaEmpacadora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantaEmpacadora
{
    class Init
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Ejecutando....!");
            /*conexion a planta*/
            DescargarPlanta();
            /*conexion a tropack*/
           // DescargarTropack();
            //Console.ReadLine();
        }

        public static void DescargarPlanta()
        {
            var Host = @"10.51.17.135\SQLDeveloper";
            var Database = "Basestil";
            var Username = "lectura";
            var Password = "L3ctura99";


            ConnectPlanta planta = new ConnectPlanta();

            try
            {
                planta.Connect(Host, Database, Username, Password);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException + "\n" + e.StackTrace);

            }
        }

        public static void DescargarTropack()
        {
            var Host = @"10.51.17.135\SQLDeveloper";
            var Database = "myad001";
            var Username = "profremar_bi";
            var Password = "profremar_bi!ipsp";


            ConnectTropack tropack = new ConnectTropack();

            try
            {
                tropack.Connect(Host, Database, Username, Password);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException + "\n" + e.StackTrace);

            }
        }
    }
}
