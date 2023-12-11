//using APIClient.AQ1.Model;
using APIClient.CommonUtils.Services;
using GuiasEmpacadora.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace GuiasEmpacadora.Endpoint
{

    public class EndpointGetInfoGuias
    {
        private readonly string API_CALL = "guias/getInfoGuias";
        private readonly string URI;
        public EndpointGetInfoGuias(string URI)
        {
            this.URI = URI + API_CALL;
        }

        public List<InfoGuia> Connect(string JWT, DateTime Start, DateTime End)
        {
            ConnectRest.JWT = JWT;
            string PStart =   Start.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string PEnd =     End.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            List<InfoGuia> infoGuiaGeneracion = JsonConvert.DeserializeObject<List<InfoGuia>>(ConnectRest.GET<string>(this.URI, 1, PStart, PEnd));
            return infoGuiaGeneracion;
        }



    }
} 
 
 
