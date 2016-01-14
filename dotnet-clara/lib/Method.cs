using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using System.Drawing;
using System.IO;
using dotnet_clara.lib.resoureces;

namespace dotnet_clara.lib
{
    class Method
    {
        private string resource;

        public Method(string resource)
        {
            this.resource = resource;
        }

        public HttpResponseMessage Request(string method, string requestUrl, HttpContent content, bool reqOutput = false)
        {
            Config config = new Config();
            Config.ConfigInfo configInfo = config.ReadConfig(null);
            HttpResponseMessage response = null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri("https://" + configInfo.host + configInfo.basePath + "/" + this.resource + "/");

            var credentials = Encoding.ASCII.GetBytes(configInfo.username+":"+configInfo.apiToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", configInfo.username, configInfo.apiToken))));

            switch (method)
            {
                case "post":
                    response = client.PostAsync(requestUrl, content).Result;
                    if (reqOutput && response.Headers.Location != null)
                    {
                        HttpResponseMessage outputResponse = client.GetAsync(response.Headers.Location).Result;
                        while (outputResponse.Content.Headers.ContentDisposition == null)
                        {
                            Thread.Sleep(2000);
                            outputResponse = client.GetAsync(response.Headers.Location).Result;
                        }
                        
                        return outputResponse;
                    }
                    break;
                case "get":
                    response = client.GetAsync(requestUrl).Result;
                    break;
                case "delete":
                    response = client.DeleteAsync(requestUrl).Result;
                    break;
                case "put":
                    break;
            }
            return response;
        }

        public bool statusCheck(HttpClient client, string location)
        {
            HttpResponseMessage outputResponse = client.GetAsync(location).Result;
            return true;
        }

    }
}
