﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resoureces
{
    class Scenes
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
        public Stream Render(string sceneId, string query, string options)
        {
            RenderQuery renderQuery = JsonConvert.DeserializeObject<RenderQuery>(query);
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(options);

            string requestUrl = sceneId + "/render";
            
            renderQuery.setupCommand = option.command;
            renderQuery.data = option.data;

            var jsonSerializer = new NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(renderQuery);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = method.Request("post", requestUrl, content, true);

            return response.Content.ReadAsStreamAsync().Result;

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
        public Stream Export(string sceneId, string extension)
        {
            var request = new RestRequest("{sceneId}/export/{extension}", RestSharp.Method.GET);
            request.AddUrlSegment("sceneId", sceneId);
            request.AddUrlSegment("extension", extension);
            string requestUrl = sceneId + "/render";
            HttpResponseMessage response = method.Request("post", requestUrl, null, true); ;

            return response.Content.ReadAsStreamAsync().Result;
        }
        public void Clone(string sceneId)
        {
            var request = new RestRequest("{sceneId}/clone", RestSharp.Method.POST);
            request.AddUrlSegment("sceneId", sceneId);
            string requestUrl = sceneId + "/render";
            HttpResponseMessage response = method.Request("post", requestUrl, null, true);
        }

        //Delete a scene
        public void Delete(string sceneId)
        {
            string requestUrl = sceneId;
            HttpResponseMessage response = method.Request("delete", requestUrl, null, true);
        }
    }
}
