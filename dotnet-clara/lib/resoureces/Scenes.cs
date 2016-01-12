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
            public int time { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public string gi { get; set; }
            public string cameraNode { get; set; }
            public string cameraType { get; set; }
            public int fov { get; set; }
            public string quality { get; set; }
            public float gamma { get; set; }
        }

        public class RenderOptions
        {
            public string setupCommand { get; set; }
            public string data { get; set; }
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

            JObject setupData = new JObject();
            setupData.Add("radius", 10);
            setupData.Add("azimuthAngle", 10);
            setupData.Add("polarAngle", 10);

            RenderOptions ro = new RenderOptions();
            request.RequestFormat = DataFormat.Json;
            ro.setupCommand = "presets/polarCameraSetup";
            ro.data = setupData.ToString(Formatting.None);
           
            PropertyInfo[] props = typeof(RenderOptions).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(ro) != null)
                {
                    request.AddParameter(prop.Name, prop.GetValue(ro));
                }
            }

            IRestResponse response = Request(request);
            return response.RawBytes;
        }


        public RestRequest Command(string sceneId, string plugin, string command, string options)
        {
            var request = new RestRequest("{sceneId}/command/{plugin}/{command}", RestSharp.Method.POST);
            request.AddUrlSegment("sceneId", sceneId);
            request.AddUrlSegment("plugin", plugin);
            request.AddUrlSegment("command", command);
            return null;
        }
        public RestRequest Import(string sceneId, string options)
        {
            var request = new RestRequest("{sceneId}/import", RestSharp.Method.POST);
            request.AddUrlSegment("sceneId", sceneId);
            return null;
        }
        public byte[] Export(string sceneId, string extension)
        {
            var request = new RestRequest("{sceneId}/export/{extension}", RestSharp.Method.GET);
            request.AddUrlSegment("sceneId", sceneId);
            request.AddUrlSegment("extension", extension);

            IRestResponse response = Request(request);
            return response.RawBytes;
        }
        public void Clone(string sceneId)
        {
            var request = new RestRequest("{sceneId}/clone", RestSharp.Method.POST);
            request.AddUrlSegment("sceneId", sceneId);

            IRestResponse response = Request(request);
        }
        public void Delete(string sceneId)
        {
            var request = new RestRequest("{sceneId}", RestSharp.Method.DELETE);
            request.AddUrlSegment("sceneId", sceneId);

            IRestResponse response = Request(request);
        }
    }
}
