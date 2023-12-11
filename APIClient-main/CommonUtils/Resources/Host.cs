using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace APIClient.CommonUtils.Resources
{
    public class Host
    {
        private bool isSSL;
        private string ipAddress;
        private int? port;
        private string hostname;
        public Host(bool isSSL, string ipAddress, int? port, string hostname)
        {
            this.isSSL = isSSL;
            this.ipAddress = ipAddress;
            this.port = port;
            this.hostname = hostname;

            AcceptCertificate();
        }
        public bool IsSSL() => this.isSSL;
        public string URL() => UseHostname() ? this.hostname : this.ipAddress + ":" + this.port;
        private bool UseHostname() => (this.hostname != null && this.hostname.Trim().Length > 0);
        public void AcceptCertificate() => new SSL().allowAllConnections();
    }

    internal class SSL
    {
        public void allowAllConnections() => ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
    }
}
