using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnet_clara.lib.resources
{
    public class Jobs
    {
        private Method method;

        public Jobs()
        {
            method = new Method("jobs");
        }
        //Get job data
        public async Task<HttpResponseMessage> Get(string jobId)
        {
            string requestUrl = jobId;

            Task<HttpResponseMessage> response = method.RequestAsync("get", requestUrl, null);
            return await response;
        }
    }
}
