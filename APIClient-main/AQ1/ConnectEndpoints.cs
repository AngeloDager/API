using APIClient.AQ1.Mapper;
using APIClient.AQ1.Model;
using APIClient.CommonUtils.Resources;
using APIClient.CommonUtils.Services;
using APICliente.AQ1.EndPoint;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ApiClient.AQ1
{
    public class ConnectEndpoints
    {
        private String UsernameDB = "monitoreo";
        private String PasswordDB = "monitoreo";
        private string Zone;
        private bool isSSLHostEndpoint;
        private string HostEndpoint;
        private string HostDatabase;
        private string DatabaseName;

        public bool zonesDownloaded, amountFedDownloaded, dissolveOxygenDownloaded, temperatureWaterDownloaded = false;
        public bool zonesSaved, amountFedSaved, dissolveOxygenSaved, temperatureWaterSaved = false;

        public ConnectEndpoints(string zone, bool isSSLHostEndpoint, string HostEndpoint, string HostDatabase, string DatabaseName)
        {
            this.Zone = zone;
            this.isSSLHostEndpoint = isSSLHostEndpoint;
            this.HostEndpoint = HostEndpoint;
            this.HostDatabase = HostDatabase;
            this.DatabaseName = DatabaseName;
        }
        public void Connect(string Path,
            string Username,
            string Password,
            DateTime MetricDay,
            bool downloadSetting,
            bool downloadConfigurationHistory)
        {
            Console.WriteLine("Downloading ===> [" + this.Zone + "] " + MetricDay.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

            /** Create Host & URI */
            Host Host = new Host(this.isSSLHostEndpoint, null, null, this.HostEndpoint);
            URI Uri = new URI(Host, Path);


            /*====================================== Hourly ======================================*/
            DateTime StartHourly = new DateTime(MetricDay.Year, MetricDay.Month, MetricDay.Day, 00, 00, 01);
            DateTime EndHourly;
            try
            {
                try
                {
                    EndHourly = new DateTime(MetricDay.Year, MetricDay.Month, MetricDay.Day + 1, 00, 00, 00);
                }
                catch
                {
                    EndHourly = new DateTime(MetricDay.Year, MetricDay.Month + 1, 1, 00, 00, 00);
                }
            }
            catch
            {
                EndHourly = new DateTime(MetricDay.Year + 1, 1, 1, 00, 00, 00);
            }

            var cadenaConexion = ConnectDB.ConnectString(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);

            Response tokenAmountFed = new JWT(Uri.Get()).Login<Response>(Username, Password);
            List<ZoneMetric> amountFed = new EndpointAmountFed(Uri.Get()).Connect(tokenAmountFed.access, StartHourly, EndHourly);

            List<MetricByZone> metricsAmountFedBD = new List<MetricByZone>();
            foreach (ZoneMetric temp in amountFed)
                foreach (Metric metric in temp.metric)
                    metricsAmountFedBD.Add(new MetricByZone
                    {
                        zone = temp.zone,
                        time = DateTime.Parse(metric.time),
                        value = metric.value
                    });


            //ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);
            ConnectDB.BulkCopy<MetricByZone>("AmountFedByZone", metricsAmountFedBD, cadenaConexion);
            //ConnectDB.Close();
            /*========================================================================================*/


            /*==================================== 5m ===================================*/
            DateTime Start5m = new DateTime(MetricDay.Year, MetricDay.Month, MetricDay.Day, 00, 00, 01);
            DateTime End5m = new DateTime(MetricDay.Year, MetricDay.Month, MetricDay.Day, 00, 05, 00);

            while (Start5m.CompareTo(EndHourly) <= 0)
            {

                //Dissolve Oxygen
                Response tokenDissolveOxygen = new JWT(Uri.Get()).Login<Response>(Username, Password);
                List<ZoneMetric> dissolveOxygen = new EndpointDissolveOxygen(Uri.Get()).Connect(tokenDissolveOxygen.access, Start5m, End5m);

                List<MetricByZone> metricsDissolveOxygenBD = new List<MetricByZone>();
                foreach (ZoneMetric zone in dissolveOxygen)
                    foreach (Metric metric in zone.metric)
                        metricsDissolveOxygenBD.Add(new MetricByZone
                        {
                            zone = zone.zone,
                            time = DateTime.Parse(metric.time),
                            value = metric.value
                        });
                //ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);

                ConnectDB.BulkCopy<MetricByZone>("DissolveOxygenByZone", metricsDissolveOxygenBD, cadenaConexion);
                //ConnectDB.Close();


                //Temperature Water
                Response tokenTemperatureWater = new JWT(Uri.Get()).Login<Response>(Username, Password);
                List<ZoneMetric> temperatureWater = new EndpointTemperatureWater(Uri.Get()).Connect(tokenTemperatureWater.access, Start5m, End5m);

                List<MetricByZone> metricsTemperatureWaterBD = new List<MetricByZone>();
                foreach (ZoneMetric zone in temperatureWater)
                    foreach (Metric metric in zone.metric)
                        metricsTemperatureWaterBD.Add(new MetricByZone
                        {
                            zone = zone.zone,
                            time = DateTime.Parse(metric.time),
                            value = metric.value
                        });
                ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);
                ConnectDB.BulkCopy<MetricByZone>("TemperatureWaterByZone", metricsTemperatureWaterBD, cadenaConexion);
                //ConnectDB.Close();


                Start5m = Start5m.AddMinutes(5);
                End5m = End5m.AddMinutes(5);

            }
            /*========================================================================================*/


            if (downloadSetting)
            {

                /*====================================== Daily Limits ======================================*/
                Response tokenDailyLimits = new JWT(Uri.Get()).Login<Response>(Username, Password);
                APIClient.AQ1.Model.DailyLimits dailyLimits = new EndpointDailyLimits(Uri.Get()).Connect(tokenDailyLimits.access, 5000, 0);

                List<APIClient.AQ1.Mapper.DailyLimits> dailyLimitsBD = new List<APIClient.AQ1.Mapper.DailyLimits>();
                foreach (APIClient.AQ1.Model.ResultDailyLimits temp in dailyLimits.results)
                    dailyLimitsBD.Add(new APIClient.AQ1.Mapper.DailyLimits
                    {
                        id = temp.id,
                        timestamp = DateTime.Parse(temp.timestamp),
                        user_id = temp.user.id,
                        username = temp.user.username,
                        user_name = temp.user.name,
                        zone_guid = temp.extra.zone_guid,
                        zone_name = temp.extra.zone_name,
                        limits_all = temp.extra.limits.days.all,
                        limits_monday = temp.extra.limits.days.monday,
                        limits_tuesday = temp.extra.limits.days.tuesday,
                        limits_wednesday = temp.extra.limits.days.wednesday,
                        limits_thursday = temp.extra.limits.days.thursday,
                        limits_friday = temp.extra.limits.days.friday,
                        limits_saturday = temp.extra.limits.days.saturday,
                        limits_sunday = temp.extra.limits.days.sunday,
                        everyday_limit = temp.extra.limits.everyday_limit,
                        daily_limit_start = temp.extra.limits.daily_limit_start
                    });

                List<APIClient.AQ1.Mapper.DailyLimits> finalDailyLimitsBD = new List<APIClient.AQ1.Mapper.DailyLimits>();
                if (!downloadConfigurationHistory)
                {
                    finalDailyLimitsBD = dailyLimitsBD
                        .Where(x =>
                        x.timestamp.Value.Year == MetricDay.Year &&
                        x.timestamp.Value.Month == MetricDay.Month &&
                        x.timestamp.Value.Day == MetricDay.Day
                        ).ToList();
                }
                else
                {
                    finalDailyLimitsBD = dailyLimitsBD;
                }

                //ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);
                ConnectDB.BulkCopy<APIClient.AQ1.Mapper.DailyLimits>("DailyLimits", finalDailyLimitsBD, cadenaConexion);
                //ConnectDB.Close();
                /*========================================================================================*/


                /*====================================== Sonic ======================================*/
                Response tokenSonic = new JWT(Uri.Get()).Login<Response>(Username, Password);
                APIClient.AQ1.Model.Sonic sonic = new EndpointSonic(Uri.Get()).Connect(tokenSonic.access, 5000, 0);

                List<APIClient.AQ1.Mapper.Sonic> sonicBD = new List<APIClient.AQ1.Mapper.Sonic>();
                foreach (APIClient.AQ1.Model.ResultSonic temp in sonic.results)
                    for (int i = 0; i < temp.extra.zone_guids.Count; i++)
                        sonicBD.Add(new APIClient.AQ1.Mapper.Sonic
                        {
                            id = temp.id,
                            timestamp = DateTime.Parse(temp.timestamp),
                            user_id = temp.user.id,
                            username = temp.user.username,
                            user_name = temp.user.name,
                            zone_guid = temp.extra.zone_guids[i],
                            zone_name = temp.extra.zone_names[i],
                            max_feed_spin_secs = temp.extra.sonic_settings.max_feed_spin_secs,
                            min_feed_spin_secs = temp.extra.sonic_settings.min_feed_spin_secs,
                            feed_pause_min_secs = temp.extra.sonic_settings.feed_pause_min_secs,
                            initial_feed_spin_secs = temp.extra.sonic_settings.initial_feed_spin_secs,
                            feed_block_duration_mins = temp.extra.sonic_settings.feed_block_duration_mins
                        });

                List<APIClient.AQ1.Mapper.Sonic> finalSonicBD = new List<APIClient.AQ1.Mapper.Sonic>();
                if (!downloadConfigurationHistory)
                {
                    finalSonicBD = sonicBD
                    .Where(x =>
                    x.timestamp.Value.Year == MetricDay.Year &&
                    x.timestamp.Value.Month == MetricDay.Month &&
                    x.timestamp.Value.Day == MetricDay.Day
                    ).ToList();
                }
                else
                {
                    finalSonicBD = sonicBD;
                }

                //ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);
                ConnectDB.BulkCopy<APIClient.AQ1.Mapper.Sonic>("Sonic", finalSonicBD, cadenaConexion);
                //ConnectDB.Close();
                /*========================================================================================*/


                /*====================================== Sonic Rate ======================================*/
                Response tokenSonicRate = new JWT(Uri.Get()).Login<Response>(Username, Password);
                APIClient.AQ1.Model.SonicRate sonicRate = new EndpointSonicRate(Uri.Get()).Connect(tokenSonicRate.access, 5000, 0);

                List<APIClient.AQ1.Mapper.SonicRate> sonicRateBD = new List<APIClient.AQ1.Mapper.SonicRate>();
                foreach (APIClient.AQ1.Model.ResultSonicRate temp in sonicRate.results)
                    foreach (Rates rate in temp.extra.sonic_rates)
                        sonicRateBD.Add(new APIClient.AQ1.Mapper.SonicRate
                        {
                            id = temp.id,
                            timestamp = DateTime.Parse(temp.timestamp),
                            user_id = temp.user.id,
                            username = temp.user.username,
                            user_name = temp.user.name,
                            zone_guid = temp.extra.zone_guid,
                            zone_name = temp.extra.zone_name,
                            day = rate.day,
                            sonic_rate = rate.sonic_rate,
                            start_time = rate.start_time,
                            end_time = rate.end_time
                        });

                List<APIClient.AQ1.Mapper.SonicRate> finalSonicRateBD = new List<APIClient.AQ1.Mapper.SonicRate>();

                if (!downloadConfigurationHistory)
                {
                    finalSonicRateBD = sonicRateBD
                    .Where(x =>
                    x.timestamp.Value.Year == MetricDay.Year &&
                    x.timestamp.Value.Month == MetricDay.Month &&
                    x.timestamp.Value.Day == MetricDay.Day
                    ).ToList();
                }
                else
                {
                    finalSonicRateBD = sonicRateBD;
                }

               // ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);
                ConnectDB.BulkCopy<APIClient.AQ1.Mapper.SonicRate>("SonicRate", finalSonicRateBD, cadenaConexion);
               // ConnectDB.Close();
                /*========================================================================================*/

            }

          

            Console.WriteLine("[" + this.Zone + "] <=== end");
        }

        public void DownloadedZones(string Path, string Username, string Password, string cadenaConexion)
        {
            /** Create Host & URI */
            Host Host = new Host(this.isSSLHostEndpoint, null, null, this.HostEndpoint);
            URI Uri = new URI(Host, Path);

            /** Login JWT */
            Response TokenZone = new JWT(Uri.Get()).Login<Response>(Username, Password);
            if (TokenZone == null || TokenZone.access == null)
                throw new Exception("Connect to JWT is refused");

            /** Zones */
            List<Zone> Zones = new EndpointZone(Uri.Get()).Connect(TokenZone.access);
            this.zonesDownloaded = true;

            ConnectDB.Connect(this.HostDatabase, this.DatabaseName, UsernameDB, PasswordDB);

            foreach (Zone zone in Zones)
            {
                zone.zone = this.Zone;
            }
            ConnectDB.BulkCopy<Zone>("Zone", Zones, cadenaConexion);
            this.zonesSaved = true;
        }

        public static void EnviarCorreo(string Subject, string Body )
        {
            MailMessage mail = new MailMessage();
            string To = "cristian.delacruz@ipsp-profremar.com";
            /* string subject = "solicitud de empleado";
             string body = "has recibido una solicitud";*/
            string[] subs = To.Split(' ', ';');
            mail.From = new MailAddress("notificaciones-ipsp@ipsp-profremar.com");
            //mail.To.Add(new MailAddress(To));
            foreach (var sub in subs)
            {
                mail.To.Add(new MailAddress(sub));
            }
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = false;
            SmtpClient client = new SmtpClient("mail.ipsp-profremar.com");
            client.Port = 587;
            client.Credentials = new NetworkCredential("notificaciones-ipsp@ipsp-profremar.com", "notificaciones5629");
            client.EnableSsl = true;
            client.Send(mail);
        }

    }
}
