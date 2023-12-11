using ApiClient.AQ1;
using APIClient.AQ1.Model;
using APIClient.CommonUtils.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace APICliente.AQ1.EndPoint
{
    public class EndpointSonicRate
    {
        private readonly string API_CALL = "settings/sonic-rate";
        private readonly string URI;
        public EndpointSonicRate(string URI)
        {
            this.URI = URI + API_CALL;
        }
        public SonicRate Connect(string JWT, int Limit, int Offset)
        {
            SonicRate SonicRate = new SonicRate();
            try
            {
                ConnectRest.JWT = JWT;
            string PLimit = nameof(Limit).ToLower() + "=" + Limit;
            string POffset = nameof(Offset).ToLower() + "=" + Offset;
             SonicRate = JsonConvert.DeserializeObject<SonicRate>(ConnectRest.GET<string>(this.URI, 0, PLimit, POffset));
            }
            catch (Exception e)
            {
                Console.WriteLine(this.URI + ": " + e.Message + "\n" + e.StackTrace);
                ConnectEndpoints.EnviarCorreo("Notificacion Api Aq1 "+ this.URI, e.Message + "\n" + e.StackTrace);
            }
            return SonicRate;
        }
    }
}
