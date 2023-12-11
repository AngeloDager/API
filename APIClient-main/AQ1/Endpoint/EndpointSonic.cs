using ApiClient.AQ1;
using APIClient.AQ1.Model;
using APIClient.CommonUtils.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace APICliente.AQ1.EndPoint
{
    public class EndpointSonic
    {
        private readonly string API_CALL = "settings/sonic";
        private readonly string URI;
        public EndpointSonic(string URI)
        {
            this.URI = URI + API_CALL;
        }
        public Sonic Connect(string JWT, int Limit, int Offset)
        {
            Sonic Sonic = new Sonic();
            try
            {
             ConnectRest.JWT = JWT;
            string PLimit = nameof(Limit).ToLower() + "=" + Limit;
            string POffset = nameof(Offset).ToLower() + "=" + Offset;
             Sonic = JsonConvert.DeserializeObject<Sonic>(ConnectRest.GET<string>(this.URI, 0, PLimit, POffset));
            }
            catch (Exception e)
            {
                Console.WriteLine(this.URI + ": " + e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 "+ this.URI, e.Message + "\n" + e.StackTrace);
            }
            return Sonic;
        }
    }
}
