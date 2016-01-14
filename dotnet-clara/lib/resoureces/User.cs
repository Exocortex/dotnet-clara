using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resoureces
{
    public class User
    {
        private Method method;

        public User()
        {
            method = new Method("users");
        }

        public class Query
        {
            public int page { get; set; }
            public int perPage { get; set; }
            public string query { get; set; }
        }
        //Get user profile
        public async Task<HttpResponseMessage> Get(string username)
        {
            string requestUrl = "users/" + username;

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, null);
            return await response;
        }

        //Create a scene
        public async Task<HttpResponseMessage> List(string username, string query)
        {            
            string requestUrl = username + "/scenes";

            Query queryObj = JsonConvert.DeserializeObject<Query>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(queryObj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, content);
            return await response;
        }
    }
}
