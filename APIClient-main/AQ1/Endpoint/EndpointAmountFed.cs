using ApiClient.AQ1;
using APIClient.AQ1.Model;
using APIClient.CommonUtils.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace APICliente.AQ1.EndPoint
{
    public class EndpointAmountFed
    {
        private readonly string API_CALL = "zone/hourly/amountfed";
        private readonly string URI;
        public EndpointAmountFed(string URI)
        {
            this.URI = URI + API_CALL;
        }
        public List<ZoneMetric> Connect(string JWT, DateTime Start, DateTime End)
        {
            List<ZoneMetric> ZonesMetrics= new List<ZoneMetric>();
            try { 
            ConnectRest.JWT = JWT;
            string PStart = nameof(Start).ToLower() + "=" + Start.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            string PEnd = nameof(End).ToLower() + "=" + End.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            string Response = ConnectRest.GET<string>(this.URI, 0, PStart, PEnd);
            bool contienePalabra = Response.Contains("zone");
            if (contienePalabra) { 
                ZonesMetrics = JsonConvert.DeserializeObject<List<ZoneMetric>>(Response);
            }


            }
            catch (Exception e)
            {
                Console.WriteLine(this.URI+": " + Start.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) +End.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 "+ this.URI, e.Message + "\n" + e.StackTrace);
            }
            return ZonesMetrics;
        }
    }
}
