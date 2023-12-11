using APIClient.CommonUtils.Resources;

namespace APIClient.CommonUtils.Services
{
    public static class ConnectRest
    {
        #region Attributtes
        public static string JWT;
        #endregion

        #region Methods
        public static T POST<T>(string URI, object request)
        {
            HttpClient httpClient = new HttpClient(JWT, request);
            return (T)httpClient.PostAsync<T>(URI).Result;
        }

        public static T GET<T>(string URI)
        {
            HttpClient httpClient = new HttpClient(JWT);
            return (T)httpClient.GetAsync<T>(URI).Result;
        }

        public static T GET<T>(string URI, int separator, params string[] parameters)
        {
            HttpClient httpClient = new HttpClient(JWT);
            return (T)httpClient.GetAsync<T>(URI, separator, parameters).Result;
        }
        #endregion
    }
}
