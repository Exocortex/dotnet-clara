using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Drawing;
using System.IO;
using dotnet_clara.lib.resoureces;

namespace dotnet_clara.lib
{
    class Request
    {
        
        public void Send(string[] args)
        {
            Config config = new Config();
            Config.ConfigInfo configInfo = config.ReadConfig(null);

            Scenes scene = new Scenes();

            var client = new RestClient();
            client.BaseUrl = new Uri(configInfo.host + configInfo.basePath + "/scenes/");
            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(configInfo.username, configInfo.apiToken);

            var request = new RestRequest();
            request.Resource = scene.Render();
            //request.Resource = "c4afda13-1fa8-4179-a1ec-66c13346ba5a/render?width=800$height=600";

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
