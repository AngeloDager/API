using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ApiClient.AQ1
{
    public class Init
    {

        private readonly static bool isSSLHostEndpoint = true;
        private readonly static string HostDatabase = "192.168.1.160";
        private readonly static string DatabaseName = "BDEtc";

        public static async Task Main(string[] args)
        {
            Console.WriteLine("Api.AQ1.Client => ::");

           // await DescargarAsync();

       
            await DescargarXRango();


        }

        public static async Task DescargarAsync()
        {
            //Constants
            string Path = "customer-api";
            string Username = "read_only";
            string Password = "ipspapi23";
            string Password1 = "ComplyMurkiness8";

            // Downloaded zones...
            ConnectEndpoints Taura; // Taura => 10.62.10.115
            ConnectEndpoints Cuba; // Cuba => 10.61.10.181
            ConnectEndpoints Churute; // Churute => 10.46.10.241
            ConnectEndpoints California; // California => 10.10.36.241
            ConnectEndpoints Japon; // Japón => 10.49.10.108
            ConnectEndpoints Mexico; // Mexico => 10.48.11.192

            Taura = new ConnectEndpoints(nameof(Taura), isSSLHostEndpoint, "10.62.10.115", HostDatabase, DatabaseName);
            Cuba = new ConnectEndpoints(nameof(Cuba), isSSLHostEndpoint, "10.61.10.181", HostDatabase, DatabaseName);
            Churute = new ConnectEndpoints(nameof(Churute), isSSLHostEndpoint, "10.46.10.241", HostDatabase, DatabaseName);
            California = new ConnectEndpoints(nameof(California), isSSLHostEndpoint, "10.10.36.241", HostDatabase, DatabaseName);
            Japon = new ConnectEndpoints(nameof(Japon), isSSLHostEndpoint, "10.49.10.108", HostDatabase, DatabaseName);
            Mexico = new ConnectEndpoints(nameof(Mexico), isSSLHostEndpoint, "10.48.11.192", HostDatabase, DatabaseName);
            /*try
            {
                Taura.DownloadedZones(Path, Username, Password);
                Cuba.DownloadedZones(Path, Username, Password);
                Churute.DownloadedZones(Path, Username, Password);
                California.DownloadedZones(Path, Username, Password);
                Japon.DownloadedZones(Path, Username, Password);
                Mexico.DownloadedZones(Path, Username, Password1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo( To, "Notificacion Api Aq1", e.InnerException + "\n" + e.StackTrace);
            }*/

            // Downloaded metrics...
            DateTime MetricDay = DateTime.Today.AddDays(-1);
            try
            {
               
                List<string> errores = new List<string>();
                var tasks = new List<Task>();
                tasks.Add(Task.Run(() => { try{ Taura.Connect(Path, Username, Password, MetricDay, true, false); }
                                           catch (Exception ex) {   errores.Add("Error TAURA :" + ex.Message);  }}));
                tasks.Add(Task.Run(() => { try { Cuba.Connect(Path, Username, Password, MetricDay, false, false); }
                                          catch (Exception ex) { errores.Add("Error CUBA :" + ex.Message); }
                }));
                tasks.Add(Task.Run(() => { try { Churute.Connect(Path, Username, Password, MetricDay, true, false); }
                                           catch (Exception ex) { errores.Add("Error CHURUTE :" + ex.Message); }
                }));
                tasks.Add(Task.Run(() => { try { California.Connect(Path, Username, Password, MetricDay, false, false); }
                                           catch (Exception ex) { errores.Add("Error CALIFORNIA :" + ex.Message); }
                }));
                tasks.Add(Task.Run(() => { try { Japon.Connect(Path, Username, Password, MetricDay, false, false); }
                                           catch (Exception ex) { errores.Add("Error JAPON :" + ex.Message); }
                }));
                tasks.Add(Task.Run(() => { try { Mexico.Connect(Path, Username, Password1, MetricDay, true, false); }
                                           catch (Exception ex) { errores.Add("Error MEXICO :" + ex.Message); }
                }));

                Task t = Task.WhenAll(tasks);
                try
                {
                    t.Wait();

                    if (errores.Count > 0)
                    {
                        foreach (var error in errores)
                        {
                            Console.WriteLine(error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            /* try
             {
                 Taura.Connect(Path, Username, Password, MetricDay, true, false);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.InnerException + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 Taura", e.InnerException + "\n" + e.StackTrace);
            }

             try
             {
                 Cuba.Connect(Path, Username, Password, MetricDay, false, false);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 Cuba", e.InnerException + "\n" + e.StackTrace);
            }

             try
             {
                  Churute.Connect(Path, Username, Password, MetricDay, true, false);
              }
              catch (Exception e)
              {
                  Console.WriteLine(e.Message + "\n" + e.StackTrace);
                 ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 Churute", e.InnerException + "\n" + e.StackTrace);
             }

              try
              {
                  California.Connect(Path, Username, Password, MetricDay, false, false);
              }
              catch (Exception e)
              {
                  Console.WriteLine(e.Message + "\n" + e.StackTrace);
                 ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 California", e.InnerException + "\n" + e.StackTrace);
             }

            try
            {
                 Japon.Connect(Path, Username, Password, MetricDay, false, false);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 Japon", e.InnerException + "\n" + e.StackTrace);
            }

            try
            {
                 Mexico.Connect(Path, Username, Password1, MetricDay, true, false);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 Mexico", e.InnerException + "\n" + e.StackTrace);
            }*/
        }

        public static async Task DescargarXRango()
        {
            //Constants
            string Path = "customer-api";
            string Username = "read_only";
            string Password = "ipspapi23";
            string Password1 = "ComplyMurkiness8";

            // Downloaded zones...
            ConnectEndpoints Taura; // Taura => 10.62.10.115
            ConnectEndpoints Cuba; // Cuba => 10.61.10.181
            ConnectEndpoints Churute; // Churute => 10.46.10.241
            ConnectEndpoints California; // California => 10.10.36.241
            ConnectEndpoints Japon; // Japón => 10.49.10.108
            ConnectEndpoints Mexico; // Mexico => 10.48.11.192

            Taura = new ConnectEndpoints(nameof(Taura), isSSLHostEndpoint, "10.62.10.115", HostDatabase, DatabaseName);
            Cuba = new ConnectEndpoints(nameof(Cuba), isSSLHostEndpoint, "10.61.10.181", HostDatabase, DatabaseName);
            Churute = new ConnectEndpoints(nameof(Churute), isSSLHostEndpoint, "10.46.10.241", HostDatabase, DatabaseName);
            California = new ConnectEndpoints(nameof(California), isSSLHostEndpoint, "10.10.36.241", HostDatabase, DatabaseName);
            Japon = new ConnectEndpoints(nameof(Japon), isSSLHostEndpoint, "10.49.10.108", HostDatabase, DatabaseName);
            Mexico = new ConnectEndpoints(nameof(Mexico), isSSLHostEndpoint, "10.48.11.192", HostDatabase, DatabaseName);
            /*try
            {
                Taura.DownloadedZones(Path, Username, Password);
                Cuba.DownloadedZones(Path, Username, Password);
                Churute.DownloadedZones(Path, Username, Password);
                California.DownloadedZones(Path, Username, Password);
                Japon.DownloadedZones(Path, Username, Password);
                Mexico.DownloadedZones(Path, Username, Password1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException + "\n" + e.StackTrace);
            }*/

            // Downloaded metrics...
            DateTime MetricDay = new DateTime(2023, 6, 6, 00, 00, 00);
            DateTime EndMetricDay = new DateTime(2023, 6, 6, 00, 00, 00);
            while (MetricDay.CompareTo(EndMetricDay) <= 0)
            {

                try
                {

                    List<string> errores = new List<string>();
                    var tasks = new List<Task>();
                    /*tasks.Add(Task.Run(() => { try{ Taura.Connect(Path, Username, Password, MetricDay, true, false); }
                                                catch (Exception ex) {   errores.Add("Error TAURA :" + ex.Message);  }}));
                     tasks.Add(Task.Run(() => { try { Cuba.Connect(Path, Username, Password, MetricDay, false, false); }
                                               catch (Exception ex) { errores.Add("Error CUBA :"+ex.Message); }
                     }));*/
                     tasks.Add(Task.Run(() => { try { Churute.Connect(Path, Username, Password, MetricDay, true, false); }
                                                catch (Exception ex) { errores.Add("Error CHURUTE :" + ex.Message); }
                     }));/*
                     tasks.Add(Task.Run(() => { try { California.Connect(Path, Username, Password, MetricDay, false, false); }
                                                catch (Exception ex) { errores.Add("Error CALIFORNIA :" + ex.Message); }
                     }));
                     tasks.Add(Task.Run(() => { try { Japon.Connect(Path, Username, Password, MetricDay, false, false); }
                                                catch (Exception ex) { errores.Add("Error JAPON :" + ex.Message); }
                     }));
                    tasks.Add(Task.Run(() => { try { Mexico.Connect(Path, Username, Password1, MetricDay, true, false); }
                                                catch (Exception ex) { errores.Add("Error MEXICO :" + ex.Message); }
                    }));*/

                    Task t = Task.WhenAll(tasks);
                    try
                    {
                        t.Wait();

                        if (errores.Count > 0)
                        {
                            foreach (var error in errores)
                            {
                                Console.WriteLine(error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Console.ReadLine();
                    }

                    /*if(t.Status == TaskStatus.Faulted)
                    {*/
                 
                    /* }
                     else
                     {
                         Console.WriteLine(t.Status.ToString());
                     }*/
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

                /*try
                {
                    Taura.Connect(Path, Username, Password, MetricDay, true, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException + "\n" + e.StackTrace);
                }

                try
                {
                    Cuba.Connect(Path, Username, Password, MetricDay, false, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }

                try
                {
                    Churute.Connect(Path, Username, Password, MetricDay, true, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }

                try
                {
                    California.Connect(Path, Username, Password, MetricDay, false, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }

                try
                {
                    Japon.Connect(Path, Username, Password, MetricDay, false, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }

                try
                {
                    Mexico.Connect(Path, Username, Password1, MetricDay, true, false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                }*/

                MetricDay = MetricDay.AddDays(1);
            }
        }

    }
}
