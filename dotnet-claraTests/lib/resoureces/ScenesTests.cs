using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_clara.lib.resoureces;
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
            resp = scene.Update(sceneId, "newName").Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void GetTest()
        {
            resp = scene.Get(sceneId).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void LibraryTest()
        {
            string query = "{\"page\":5,\"perPage\":10,\"query\":\"robot\" }";
            resp = scene.Library(query).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void CreateTest()
        {
            resp = scene.Create().Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            string sid = "2a68e13e-b8f2-4f4d-bcc1-556188bf7fb6";
            resp = scene.Delete(sid).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void CloneTest()
        {
            resp = scene.Clone(sceneId).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void ExportTest()
        {
            Stream stream;
            stream = scene.Export(sceneId,"fbx").Result;
            Assert.IsNotNull(stream);
        }

        [TestMethod()]
        public void ImportTest()
        {
            string[] file = new string[1];
            file[0] = "g:\\test.png";
            resp = scene.Import(sceneId, file).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void CommandTest()
        {
            string commandOpt = "{\"setupCommand\":\"vary/sceneSetup\"}";
            resp = scene.Command(sceneId,commandOpt).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void RenderTest()
        {
            Stream stream;
            stream = scene.Render(sceneId, "{}", "{}").Result;
            Assert.IsNotNull(stream);
        }
    }
}