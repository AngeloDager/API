using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.CommonUtils.Resources
{
    internal class HttpClient
    {
        #region Attributes
        private static readonly string MEDIA_TYPE_APPLICATION_JSON = "application/json";
        private static readonly string HEADERS_BEARER = "Bearer ";

        private System.Net.Http.HttpClient a;
        private System.Net.Http.StringContent b;
        private System.Net.Http.HttpResponseMessage c;
        #endregion


        #region Access
        public void A(System.Net.Http.HttpClient A_0) => this.a = A_0;
        public System.Net.Http.HttpClient B() => this.a;
        public void C(System.Net.Http.StringContent A_0) => this.b = A_0;
        public System.Net.Http.StringContent D() => this.b;
        public void E(System.Net.Http.HttpResponseMessage A_0) => this.c = A_0;
        public System.Net.Http.HttpResponseMessage F() => this.c;
        #endregion


        #region Constructs
        public HttpClient(string A_0, object A_1)
        {
            A(new System.Net.Http.HttpClient());
            B().Timeout = TimeSpan.FromSeconds(500);
            C(new System.Net.Http.StringContent(JsonConvert.SerializeObject(A_1), Encoding.UTF8, HttpClient.MEDIA_TYPE_APPLICATION_JSON));
            D().Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(HttpClient.MEDIA_TYPE_APPLICATION_JSON);
            if (A_0 != null)
                B().DefaultRequestHeaders.Add("Authorization", HttpClient.HEADERS_BEARER + A_0);
        }
        public HttpClient(string A_0)
        {
            A(new System.Net.Http.HttpClient());
            B().Timeout = TimeSpan.FromSeconds(500);
            B().DefaultRequestHeaders.Add("Accept", HttpClient.MEDIA_TYPE_APPLICATION_JSON);
            if (A_0 != null)
                B().DefaultRequestHeaders.Add("Authorization", HttpClient.HEADERS_BEARER + A_0);
        }
        #endregion


        #region Verbose
        public async Task<T> PostAsync<T>(string A_0)
        {
            E(await B().PostAsync(A_0, D()));
            if (F().StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new TimeoutException();
            else if (F().StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new TimeoutException();
            else if (!F().IsSuccessStatusCode)
                PrintErrorMessage();
            return returnResult<T>();
        }
        public async Task<T> GetAsync<T>(string A_0)
        {
            E(await B().GetAsync(A_0));
            if (F().StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new TimeoutException();
            else if (F().StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new TimeoutException();
            else if (!F().IsSuccessStatusCode)
                PrintErrorMessage();
            return returnResult<T>();
        }
        public async Task<T> GetAsync<T>(string A_0, int A_1 = 0, params string[] A_2)
        {
            if (A_1 == 0)
                E(await B().GetAsync(A_0 + "?" + string.Join("&", A_2)));
            else
                E(await B().GetAsync(A_0 + "/" + string.Join("/", A_2)));
            if (F().StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new TimeoutException();
            else if (F().StatusCode == System.Net.HttpStatusCode.BadRequest)
                throw new TimeoutException();
            else if (!F().IsSuccessStatusCode)
                PrintErrorMessage();
            return returnResult<T>();
        }
        #endregion


        #region UTILS
        private void PrintErrorMessage() => Console.WriteLine("=========> Error <=========\n" +
            "* Status Code: " + F().StatusCode +
            "\n* Error Message: " + F().RequestMessage +
            "\n===========================");
        private T returnResult<T>()
        {
            if (typeof(T).Equals(typeof(System.Byte)))
                return (T)Convert.ChangeType(F().Content.ReadAsByteArrayAsync().Result, typeof(T));
            if (typeof(T).Equals(typeof(System.IO.Stream)))
                return (T)Convert.ChangeType(F().Content.ReadAsStreamAsync().Result, typeof(T));
            if (typeof(T).Equals(typeof(System.String)))
                return (T)Convert.ChangeType(F().Content.ReadAsStringAsync().Result, typeof(T));
            return (T)default;
        }
        #endregion
    }
}
