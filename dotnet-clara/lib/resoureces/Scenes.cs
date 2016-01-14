using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resoureces
{
    public class Scenes
    {
        private string uuid;

        private Method method;

        public Scenes()
        {
            method = new Method("scenes");
        }

        public Scenes(string uuid)
        {
            this.uuid = uuid;
            method = new Method("scenes");
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
            public string setupCommand { get; set; }
            public JObject data { get; set; }
        }

        public class CommandOptions
        {
            public string command { get; set; }
            public JObject data { get; set; }
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

        //Render an image
        public async Task<Stream> Render(string sceneId, string query, string options)
        {
            RenderQuery renderQuery = JsonConvert.DeserializeObject<RenderQuery>(query);
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(options);

            string requestUrl = sceneId + "/render";

            renderQuery.setupCommand = option.command;
            renderQuery.data = option.data;

            var jsonSerializer = new NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(renderQuery);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> getResponse = method.RequestAsync("post", requestUrl, content, true);

            HttpResponseMessage resp = await getResponse;

            return await resp.Content.ReadAsStreamAsync();
        }

        //Run a command
        public async Task<HttpResponseMessage> Command(string sceneId, string commandOptions)
        {
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(commandOptions);
            string requestUrl = sceneId + "/command/" + option.command;

            var jsonSerializer = new NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(option.data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, content, true);

            return await response;
        }

        //Import files
        public async Task<HttpResponseMessage> Import(string sceneId, string[] fileList)
        {
            string requestUrl = sceneId + "/import";

            var content = new MultipartFormDataContent();

            foreach (string file in fileList)
            {
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(file));

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file
                };
                content.Add(fileContent);
            }
            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, content);

            return await response;
        }

        //Export a scene
        public async Task<Stream> Export(string sceneId, string extension)
        {
            string requestUrl = sceneId + "/export/" + extension;
            Task<HttpResponseMessage> getResponse = method.RequestAsync("post", requestUrl, null, true); ;
            HttpResponseMessage resp = await getResponse;

            return await resp.Content.ReadAsStreamAsync();
        }

        //Clone a scene
        public async Task<HttpResponseMessage> Clone(string sceneId)
        {
            string requestUrl = sceneId + "/clone";
            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, null);
            return await response;
        }

        //Delete a scene
        public async Task<HttpResponseMessage> Delete(string sceneId)
        {
            string requestUrl = sceneId;
            Task<HttpResponseMessage> response = method.RequestAsync("delete", requestUrl, null);
            return await response;
        }

        //Create a scene
        public async Task<HttpResponseMessage> Create(string sceneName = null)
        {
            string requestUrl = null;
            StringContent content = new StringContent(sceneName, Encoding.UTF8);
            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, content);
            return await response;
        }
    }
}
