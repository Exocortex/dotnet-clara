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
        Scenes scene = new Scenes();

        public void Request(string[] args)
        {
            Config config = new Config();
            Config.ConfigInfo configInfo = config.ReadConfig(null);

            

            var client = new RestClient();

            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(configInfo.username, configInfo.apiToken);
                
            client.BaseUrl = new Uri(configInfo.host + configInfo.basePath + "/scenes/");

            var request = new RestRequest();

            IRestResponse response = client.Execute(request);

            var imagePath = "g:\\aaa.png";
            var bytes = response.RawBytes;
            using (var imageFile = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
        }

    }
}
