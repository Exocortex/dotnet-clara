using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_clara.lib.resoureces;
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
    public class UserTests
    {
        User user = new User();
        string username = "godlzr";
        HttpResponseMessage resp;


        [TestMethod()]
        public void GetTest()
        {
            resp = user.Get(username).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void ListScenesTest()
        {
            string scenesQry = "{page:5,perPage:10,query:\"tank\"}";
            resp = user.ListScenes(username, scenesQry).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod()]
        public void ListJobsTest()
        {
            string jobsQry = "{page:5,perPage:10}";
            resp = user.ListScenes(username, jobsQry).Result;
            Assert.AreEqual(resp.StatusCode, HttpStatusCode.OK);
        }
    }
}