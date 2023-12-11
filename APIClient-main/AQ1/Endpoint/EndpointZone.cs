using ApiClient.AQ1;
using APIClient.AQ1.Model;
using APIClient.CommonUtils.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APICliente.AQ1.EndPoint
{
    public class EndpointZone
    {
        private readonly string API_CALL = "zone/index";
        private readonly string URI;

        public EndpointZone(string URI)
        {
            this.URI = URI + API_CALL;
        }
        public List<Zone> Connect(string JWT)
        {
            List<Zone> Zones = new List<Zone>();
            try
            {
                ConnectRest.JWT = JWT;
               Zones = JsonConvert.DeserializeObject<List<Zone>>(ConnectRest.GET<string>(this.URI));
            }
            catch (Exception e)
            {
                Console.WriteLine(this.URI + ": " + e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 "+ this.URI, e.Message + "\n" + e.StackTrace);
            }
            return Zones;
        }
    }
}
