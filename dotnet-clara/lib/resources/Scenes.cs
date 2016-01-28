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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resources
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

        public class Query
        {
            public int page { get; set; }
            public int perPage { get; set; }
            public string query { get; set; }
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

        //Render an image
        public async Task<Stream> RenderAsync(string sceneId, string query, string options)
        {
            RenderQuery renderQuery = JsonConvert.DeserializeObject<RenderQuery>(query);
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(options);

            string requestUrl = sceneId + "/render";
            if (option.command == null)
                renderQuery.setupCommand = "";
            else
                renderQuery.setupCommand = option.command;

            renderQuery.data = option.data;
            
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(renderQuery);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> getResponse = method.RequestAsync("post", requestUrl, content, true);

            HttpResponseMessage resp = await getResponse;

            return await resp.Content.ReadAsStreamAsync();
        }
        public Stream Render(string sceneId, string query, string options)
        {
            RenderQuery renderQuery = JsonConvert.DeserializeObject<RenderQuery>(query);
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(options);

            string requestUrl = sceneId + "/render";
            if (option.command == null)
                renderQuery.setupCommand = "";
            else
                renderQuery.setupCommand = option.command;

            renderQuery.data = option.data;

            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(renderQuery);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage resp = method.Request("post", requestUrl, content, true);

            return resp.Content.ReadAsStreamAsync().Result;
        }
        //Return the thumbnail of the scene
        public async Task<Stream> ThumbnailAsync(string sceneId)
        {
            string requestUrl = sceneId + "/thumbnail.jpg";

            Task<HttpResponseMessage> getResponse = method.RequestAsync("get", requestUrl, null, true);

            HttpResponseMessage resp = await getResponse;

            return await resp.Content.ReadAsStreamAsync();
        }
        public Stream Thumbnail(string sceneId)
        {
            string requestUrl = sceneId + "/thumbnail.jpg";

            HttpResponseMessage resp = method.Request("get", requestUrl, null, true);

            return resp.Content.ReadAsStreamAsync().Result;
        }

        //Run a command
        public async Task<HttpResponseMessage> CommandAsync(string sceneId, string commandOptions)
        {
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(commandOptions);
            string requestUrl = sceneId + "/command/" + option.command;

            var data = new { data = option.data };

            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json;
            if (option.data == null)
                json = "{}";
            else
                json = jsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, content);

            return await response;
        }
        public HttpResponseMessage Command(string sceneId, string commandOptions)
        {
            CommandOptions option = JsonConvert.DeserializeObject<CommandOptions>(commandOptions);
            string requestUrl = sceneId + "/command/" + option.command;

            var data = new { data = option.data };

            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json;
            if (option.data == null)
                json = "{}";
            else
                json = jsonSerializer.Serialize(data);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = method.Request("post", requestUrl, content);

            return response;
        }

        //Import files
        public async Task<HttpResponseMessage> ImportAsync(string sceneId, string[] fileList)
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
        public HttpResponseMessage Import(string sceneId, string[] fileList)
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
            HttpResponseMessage response = method.Request("post", requestUrl, content);

            return response;
        }

        //Export a scene
        public async Task<Stream> ExportAsync(string sceneId, string extension)
        {
            string requestUrl = sceneId + "/export/" + extension;
            Task<HttpResponseMessage> getResponse = method.RequestAsync("post", requestUrl, null, true); ;
            HttpResponseMessage resp = await getResponse;

            return await resp.Content.ReadAsStreamAsync();
        }
        public Stream Export(string sceneId, string extension)
        {
            string requestUrl = sceneId + "/export/" + extension;
            HttpResponseMessage resp = method.Request("post", requestUrl, null, true); ;

            return resp.Content.ReadAsStreamAsync().Result;
        }

        //Clone a scene
        public async Task<HttpResponseMessage> CloneAsync(string sceneId)
        {
            string requestUrl = sceneId + "/clone";
            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, null);
            return await response;
        }
        public HttpResponseMessage Clone(string sceneId)
        {
            string requestUrl = sceneId + "/clone";
            HttpResponseMessage response = method.Request("post", requestUrl, null);
            return response;
        }

        //Delete a scene
        public async Task<HttpResponseMessage> DeleteAsync(string sceneId)
        {
            string requestUrl = sceneId;
            Task<HttpResponseMessage> response = method.RequestAsync("delete", requestUrl, null);
            return await response;
        }
        public HttpResponseMessage Delete(string sceneId)
        {
            string requestUrl = sceneId;
            HttpResponseMessage response = method.Request("delete", requestUrl, null);
            return response;
        }

        //Create a scene
        public async Task<HttpResponseMessage> CreateAsync()
        {
            string requestUrl = null;

            Task<HttpResponseMessage> response = method.RequestAsync("post", requestUrl, null);
            return await response;
        }
        public HttpResponseMessage Create()
        {
            string requestUrl = null;

            HttpResponseMessage response = method.Request("post", requestUrl, null);
            return response;
        }

        //Update a scene
        public async Task<HttpResponseMessage> UpdateAsync(string sceneId, string sceneName)
        {
            string requestUrl = sceneId;

            string json = "{\"name\":\"" + sceneName + "\"}";

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = method.RequestAsync("put", requestUrl, content);
            return await response;
        }
        public HttpResponseMessage Update(string sceneId, string sceneName)
        {
            string requestUrl = sceneId;

            string json = "{\"name\":\"" + sceneName + "\"}";

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = method.Request("put", requestUrl, content);
            return response;
        }
        //Create a scene
        public async Task<HttpResponseMessage> GetAsync(string sceneId)
        {
            string requestUrl = sceneId;

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, null);
            return await response;
        }
        public HttpResponseMessage Get(string sceneId)
        {
            string requestUrl = sceneId;

            HttpResponseMessage response = method.Request("get", requestUrl, null);
            return response;
        }

        //List public scenes
        public async Task<HttpResponseMessage> LibraryAsync(string query)
        {
            string requestUrl = null;

            Query queryObj = JsonConvert.DeserializeObject<Query>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(queryObj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, content);
            return await response;
        }
        public HttpResponseMessage Library(string query)
        {
            string requestUrl = null;

            Query queryObj = JsonConvert.DeserializeObject<Query>(query);
            var jsonSerializer = new Method.NewtonsoftJsonSerializer();
            string json = jsonSerializer.Serialize(queryObj);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = method.Request("get", requestUrl, content);
            return response;
        }

    }
}
