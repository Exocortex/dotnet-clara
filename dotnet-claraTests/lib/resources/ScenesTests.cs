using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_clara.lib.resources;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotnet_clara.lib.resoureces.Tests
{
    [TestClass()]
    public class ScenesTests
    {
        Scenes scene = new Scenes();
        string sceneId = "c4afda13-1fa8-4179-a1ec-66c13346ba5a";

        HttpResponseMessage resp;

        [TestMethod()]
        public void UpdateTest()
        {
            resp = scene.UpdateAsync(sceneId, "newName").Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void GetTest()
        {
            resp = scene.GetAsync(sceneId).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void LibraryTest()
        {
            string query = "{\"page\":5,\"perPage\":10,\"query\":\"robot\" }";
            resp = scene.LibraryAsync(query).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void CreateTest()
        {
            resp = scene.CreateAsync().Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            string sid = "2a68e13e-b8f2-4f4d-bcc1-556188bf7fb6";
            resp = scene.DeleteAsync(sid).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void CloneTest()
        {
            resp = scene.CloneAsync(sceneId).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void ExportTest()
        {
            Stream stream;
            stream = scene.ExportAsync(sceneId,"fbx").Result;
            Assert.IsNotNull(stream);
        }

        [TestMethod()]
        public void ImportTest()
        {
            string[] file = new string[1];
            file[0] = "g:\\test.png";
            resp = scene.ImportAsync(sceneId, file).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void CommandTest()
        {
            string commandOpt = "{command:\"vary/sceneSetup\"}";
            resp = scene.CommandAsync(sceneId,commandOpt).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void RenderTest()
        {
            Stream stream;
            stream = scene.RenderAsync(sceneId, "{}", "{}").Result;
            Assert.IsNotNull(stream);
        }

        [TestMethod()]
        public void ThumbnailTest()
        {
            Stream stream;
            stream = scene.ThumbnailAsync(sceneId).Result;
            Assert.IsNotNull(stream);
        }
    }
}