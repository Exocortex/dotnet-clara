using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
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

        public IRestResponse Request(RestRequest req)
        {
            Config config = new Config();
            Config.ConfigInfo configInfo = config.ReadConfig(null);

            var client = new RestClient();

            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(configInfo.username, configInfo.apiToken);

            client.BaseUrl = new Uri(configInfo.host + configInfo.basePath + "/"+this.resource+"/");

            IRestResponse response = client.Execute(req);

            Console.WriteLine("Info: Status:{0}", response.StatusCode);

            return response;
        }

    }
}
