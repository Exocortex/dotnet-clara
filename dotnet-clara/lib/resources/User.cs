using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resources
{
    public class User
    {
        private Method method;

        public User()
        {
            method = new Method("users");
        }

        public class SceneQuery
        {
            public int page { get; set; }
            public int perPage { get; set; }
            public string query { get; set; }
        }
        public class JobQuery
        {
            public int page { get; set; }
            public int perPage { get; set; }
        }

        public class Profile
        {
            public string name { get; set; }
            public string jobTitle { get; set; }
            public string company { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public string website { get; set; }
        }
        //Get user profile
        public async Task<HttpResponseMessage> GetAsync(string username)
        {
            string requestUrl = username;

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, null);
            return await response;
        }

        public HttpResponseMessage Get(string username)
        {
            string requestUrl = username;

            HttpResponseMessage response = method.Request("get", requestUrl, null);
            return response;
        }

        // Update user profile
        public async Task<HttpResponseMessage> UpdateAsync(string username, string profile)
        {
            string requestUrl = username;

            Profile pro = JsonConvert.DeserializeObject<Profile>(profile);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(pro);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = method.RequestAsync("put", requestUrl, content);
            return await response;
        }

        public HttpResponseMessage Update(string username, string profile)
        {
            string requestUrl = username;

            Profile pro = JsonConvert.DeserializeObject<Profile>(profile);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(pro);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = method.Request("put", requestUrl, content);
            return response;
        }

        //List your scenes
        public async Task<HttpResponseMessage> ListScenesAsync(string username, string query)
        {            
            string requestUrl = username + "/scenes";

            SceneQuery queryObj = JsonConvert.DeserializeObject<SceneQuery>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(queryObj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, content);
            return await response;
        }
        public HttpResponseMessage ListScenes(string username, string query)
        {
            string requestUrl = username + "/scenes";

            SceneQuery queryObj = JsonConvert.DeserializeObject<SceneQuery>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(queryObj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = method.Request("get", requestUrl, content);
            return response;
        }

        //List your jobs
        public async Task<HttpResponseMessage> ListJobsAsync(string username, string query)
        {
            string requestUrl = username + "/jobs";

            JobQuery qry = JsonConvert.DeserializeObject<JobQuery>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(qry);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, content);
            return await response;
        }
        public HttpResponseMessage ListJobs(string username, string query)
        {
            string requestUrl = username + "/jobs";

            JobQuery qry = JsonConvert.DeserializeObject<JobQuery>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(qry);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = method.Request("get", requestUrl, content);
            return response;
        }
    }
}
