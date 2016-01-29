using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using RestSharp;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using dotnet_clara.lib.resources;

namespace dotnet_clara.lib
{
    public class Method
    {
        private string resource;
        private RestClient client;
        private Config config;
        private Config.ConfigInfo configInfo;

        public Method(string resource)
        {
            this.resource = resource;
            this.client = new RestClient();
            this.config = new Config();
            this.configInfo = config.ReadConfig(null);

            this.client.BaseUrl = new Uri("https://" + configInfo.host + configInfo.basePath + "/" + this.resource + "/");
            this.client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(configInfo.username, configInfo.apiToken);
        }

        public class NewtonsoftJsonSerializer : RestSharp.Serializers.ISerializer, RestSharp.Deserializers.IDeserializer
        {
            private Newtonsoft.Json.JsonSerializer jsonSerializer;

            public NewtonsoftJsonSerializer()
            {
                this.jsonSerializer = new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
            }

            public string ContentType
            {
                get { return "application/json"; } // Probably used for Serialization?
                set { }
            }

            public string DateFormat { get; set; }

            public string Namespace { get; set; }

            public string RootElement { get; set; }

            public string Serialize(object obj)
            {
                return JsonConvert.SerializeObject(obj);
            }

            public T Deserialize<T>(RestSharp.IRestResponse response)
            {
                var content = response.Content;

                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return jsonSerializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }
        }

        public IRestResponse Request(string method, string requestUrl, bool reqOutput = false)
        {
            IRestResponse response = null;
            RestRequest request;    

            switch (method)
            {
                /*case "post":
                    request = new RestRequest(RestSharp.Method.POST);
                    response = this.client.Execute(request);
                    if (reqOutput && response.Headers.Location != null)
                    {
                        HttpResponseMessage outputResponse = this.client.GetAsync(response.Headers.Location).Result;

                        while (outputResponse.Content.Headers.ContentDisposition == null)
                        {
                            Thread.Sleep(2000);
                            outputResponse = this.client.GetAsync(response.Headers.Location).Result;
                        }
                        
                        return outputResponse;
                    }
                    break;*/
                case "get":
                    request = new RestRequest(RestSharp.Method.GET);
                    request.AddParameter("Url", requestUrl);
                    response = this.client.Execute(request);
                    break;
                /*case "delete":
                    response = this.client.DeleteAsync(requestUrl).Result;
                    break;
                case "put":
                    response = this.client.PutAsync(requestUrl, content).Result;
                    break;*/
            }
            return response;
        }
    }
}
