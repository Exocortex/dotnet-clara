using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_clara.lib.resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet_clara.lib.resoureces.Tests
{
    [TestClass()]
    public class JobsTests
    {
        Jobs jobs = new Jobs();
        string jobId = "8990e849-d251-4f72-9a35-111b249f83a3";

        HttpResponseMessage resp;

        [TestMethod()]
        public void GetTest()
        {
            resp = jobs.GetAsync(jobId).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }
    }
}