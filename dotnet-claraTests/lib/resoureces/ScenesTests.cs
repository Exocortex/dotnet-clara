using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_clara.lib.resoureces;
using System;
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
        public void RenderTest()
        {
            scene.Render(sceneId, "{width:1200, height:600}", "{command:\"presets/polarCameraSetup\", data:{radius:100,azimuthAngle:10,polarAngle:20}}");
            Assert.Fail();
        }

        [TestMethod()]
        public void CommandTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            resp = scene.Update(sceneId, "newName").Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void getTest()
        {
            resp = scene.Get(sceneId).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }
    }
}