using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIClient.CommonUtils.Resources;
using APIClient.CommonUtils.Services;
using GuiasEmpacadora.Endpoint;
using GuiasEmpacadora.Model;

namespace GuiasEmpacadora
{
    public class ConnectEndpoints
    {
        private bool isSSLHostEndpoint;
        private string HostEndpoint;
        private string HostDatabase;
        private string DatabaseName;

        private string DatabaseUser;
        private string DatabasePassword;

        public bool infoGuiasDownloaded, infoGuiasSaved = false;
        public ConnectEndpoints(bool isSSLHostEndpoint, string HostEndpoint, string HostDatabase, string DatabaseName)
        {
            this.isSSLHostEndpoint = isSSLHostEndpoint;
            this.HostEndpoint = HostEndpoint;
            this.HostDatabase = HostDatabase;
            this.DatabaseName = DatabaseName;
        }

        public ConnectEndpoints(bool isSSLHostEndpoint, string HostEndpoint, string HostDatabase, string DatabaseName, string DatabaseUser, string DatabasePassword)
        {
            this.isSSLHostEndpoint = isSSLHostEndpoint;
            this.HostEndpoint = HostEndpoint;
            this.HostDatabase = HostDatabase;
            this.DatabaseName = DatabaseName;
            this.DatabaseUser = DatabaseUser;
            this.DatabasePassword = DatabasePassword;
        }

        public void Connect(string Path, string Username, string Password, DateTime ProcessDay)
        {
            DateTime ProcessDayProgram = ProcessDay;

            Console.WriteLine("Connecting host [" + this.HostEndpoint + "]...");
            Console.WriteLine("Start ===============> " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
            if(string.IsNullOrEmpty(this.DatabaseUser))
                ConnectDB.Connect(this.HostDatabase, this.DatabaseName, null, null);
            else
                ConnectDB.Connect(this.HostDatabase, this.DatabaseName, this.DatabaseUser, this.DatabasePassword);

            /** Create Host & URI */
            Host Host = new Host(this.isSSLHostEndpoint, null, null, this.HostEndpoint);
            URI Uri = new URI(Host, Path);

            /** Login JWT */
            ResponseJWT TokenZone = new JWT(Uri.Get() , "auth/loginUsuarioExterno").Login<ResponseJWT>(Username, Password);
            DateTime StartHourly = new DateTime(ProcessDayProgram.Year, ProcessDayProgram.Month, ProcessDayProgram.Day, 00, 00, 01);
            DateTime EndHourly = new DateTime(ProcessDayProgram.Year, ProcessDayProgram.Month, ProcessDayProgram.Day, 23, 59, 59);
            /** Info Guias */
            List<InfoGuia> infoGuias = new EndpointGetInfoGuias(Uri.Get()).Connect(TokenZone.token, StartHourly, EndHourly);
            this.infoGuiasDownloaded = true;
            Console.WriteLine($"Downloading {infoGuias.Count} regisros |  Max Id {infoGuias.Max(x=>x.co_guia)}  | Fecha download " + ProcessDayProgram.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + " ...");
            ConnectDB.BulkCopyWithIdentity<InfoGuia>("InfoGuia", infoGuias);
            this.infoGuiasSaved = true;

            Console.WriteLine("End ===============> " + DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
        }
    }
}
