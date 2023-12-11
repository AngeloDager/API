
namespace APIClient.CommonUtils.Resources
{
    public class URI
    {
        private readonly string HTTP = "http";
        private readonly string HTTPS = "https";
        private string path;
        public Host host;
        public URI(Host host, string path)
        {
            this.host = host;
            this.path = path;
        }
        public string Get() => Scheme(this.host.IsSSL()) + "://" + host.URL() + "/" + this.path + "/";
        private string Scheme(bool isSSL) => isSSL ? HTTPS : HTTP;
    }
}
