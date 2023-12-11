using APIClient.CommonUtils.Services;
using Newtonsoft.Json;
using System;

namespace APIClient.CommonUtils.Resources
{
    public class JWT
    {
        private readonly string API_CALL = "token/";
        private readonly string URI;
        public JWT(string URI)
        {
            this.URI = URI + API_CALL;
        }
        public JWT(string URI, string API_CALL)
        {
            this.URI = URI + API_CALL;
        }
        public T Login<T>(string username, string password)
        {
            object response = new object();
            try
            {  
             Request request = new Request() { username = username, password = password };
             response = JsonConvert.DeserializeObject<T>(ConnectRest.POST<String>(this.URI, request));
            }
            catch (Exception e)
            {
                Console.WriteLine(this.URI + e.Message + "LOGIN\n"+ this.URI + e.StackTrace);
            }
            return (T) response;
        }
    }

    public class Request : IRequest
    {
        public string username;
        public string password;
    }
    public class Response : IResponse
    {
        public string refresh;
        public string access;
    }
}
