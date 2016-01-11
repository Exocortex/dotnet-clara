using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resoureces
{
    class Scenes
    {
        private string uuid;


        public Scenes()
        {
            
        }

        public Scenes(string uuid)
        {
            this.uuid = uuid;
        }


        public class RenderQuery
        {
            public string time { get; set; }
            public string width { get; set; }
            public string height { get; set; }
            public string gi { get; set; }
            public string cameraNode { get; set; }
            public string cameraType { get; set; }
            public string fov { get; set; }
            public string quality { get; set; }
            public string gamma { get; set; }
        }

        private IRestResponse Request(RestRequest req)
        {
            Config config = new Config();
            Config.ConfigInfo configInfo = config.ReadConfig(null);

            var client = new RestClient();

            client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(configInfo.username, configInfo.apiToken);

            client.BaseUrl = new Uri(configInfo.host + configInfo.basePath + "/scenes/");

            IRestResponse response = client.Execute(req);

            Console.WriteLine("Info: Status:{0}", response.StatusCode);

            return response;
        }


        public byte[] Render(string sceneId, string query, string options)
        {
            RenderQuery rq = JsonConvert.DeserializeObject<RenderQuery>(query);
            var request = new RestRequest("{sceneId}/render", RestSharp.Method.GET);
            request.AddUrlSegment("sceneId", sceneId);

            PropertyInfo[] properties = typeof(RenderQuery).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(rq) != null)
                {
                    request.AddParameter(property.Name, property.GetValue(rq));
                }
            }
            //request.AddUrlSegment("plugin", "presets");
            //request.AddUrlSegment("command", "polarCameraSetup");
            //string json = "{ \"setupCommand\":\"presets/polarCameraSetup\" \"data\": { \"radius\":50, \"azimuthAngle\": 180, \"polarAngle\": 5} }";

            //request.RequestFormat = DataFormat.Json;
            //request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = Request(request);
            return response.RawBytes;
        }


        public RestRequest Command()
        {
            return null;
        }
        public RestRequest Import()
        {
            return null;
        }
        public RestRequest Export()
        {
            return null;
        }
    }
}
