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
    public class Method
    {
        private string resource;
        private HttpClient client;
        private Config config;
        private Config.ConfigInfo configInfo;

        public Method(string resource)
        {
            this.resource = resource;
            this.client = new HttpClient();
            this.config = new Config();
            this.configInfo = config.ReadConfig(null);

            this.client.BaseAddress = new Uri("https://" + configInfo.host + configInfo.basePath + "/" + this.resource + "/");

            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", configInfo.username, configInfo.apiToken))));
        }

        public HttpResponseMessage Request(string method, string requestUrl, HttpContent content, bool reqOutput = false)
        {
            HttpResponseMessage response = null;
   
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));          

            switch (method)
            {
                case "post":
                    response = this.client.PostAsync(requestUrl, content).Result;
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
                    break;
                case "get":
                    response = this.client.GetAsync(requestUrl).Result;
                    break;
                case "delete":
                    response = this.client.DeleteAsync(requestUrl).Result;
                    break;
                case "put":
                    break;
            }
            return response;
        }

        public async Task<HttpResponseMessage> RequestAsync(string method, string requestUrl, HttpContent content, bool reqOutput = false)
        {
            Task<HttpResponseMessage> response = null;

            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            switch (method)
            {
                case "post":
                    response = this.client.PostAsync(requestUrl, content);
                    if (reqOutput)
                    {
                        HttpResponseMessage resp = await response;
                        HttpResponseMessage outputResponse = this.client.GetAsync(resp.Headers.Location).Result;

                        while (outputResponse.Content.Headers.ContentDisposition == null)
                        {
                            Thread.Sleep(2000);
                            outputResponse = this.client.GetAsync(resp.Headers.Location).Result;
                        }

                        return outputResponse;
                    }
                    break;
                case "get":
                    response = this.client.GetAsync(requestUrl);
                    break;
                case "delete":
                    response = this.client.DeleteAsync(requestUrl);
                    break;
                case "put":
                    break;
            }
            return await response;
        }

    }
}
